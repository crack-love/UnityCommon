#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.LowLevel;

namespace UnityCommon
{
    class PlayerLoopViewer : EditorWindow
    {
        // SerializeField is used to ensure the view state is written to the window 
        // layout file. This means that the state survives restarting Unity as long as the window
        // is not closed. If the attribute is omitted then the state is still serialized/deserialized.
        [SerializeField] TreeViewState m_TreeViewState;

        //The TreeView is not serializable, so it should be reconstructed from the tree data.
        PlayerLoopTreeView m_TreeView;

        void OnEnable()
        {
            // Check whether there is already a serialized view state (state 
            // that survived assembly reloading)
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

            m_TreeView = new PlayerLoopTreeView(m_TreeViewState);
        }

        void OnGUI()
        {
            m_TreeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Unity Common/Threading/Player Loop Viewer")]
        static void ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            var window = GetWindow<PlayerLoopViewer>();
            window.titleContent = new GUIContent("PlayerLoopViewer");
            window.Show();
        }
    }

    class PlayerLoopTreeView : TreeView
    {
        public PlayerLoopTreeView(TreeViewState treeViewState) : base(treeViewState)
        {
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            // BuildRoot is called every time Reload is called to ensure that TreeViewItems 
            // are created from data. Here we create a fixed set of items. In a real world example,
            // a data model should be passed into the TreeView and the items created from the model.

            // This section illustrates that IDs should be unique. The root item is required to 
            // have a depth of -1, and the rest of the items increment from that.
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
            
            var id = 0;
            AddSystemReculsive(PlayerLoop.GetCurrentPlayerLoop(), ref id, root);

            // Utility method that initializes the TreeViewItem.children and .parent for all items.
            SetupDepthsFromParentsAndChildren(root);

            // Return root of the tree
            return root;
        }

        void AddSystemReculsive(PlayerLoopSystem system, ref int id, TreeViewItem parent)
        {
            var type = system.type;
            var update = system.updateDelegate;
            var sslist = system.subSystemList;

            TreeViewItem typeItem;
            if (id == 0)
            {
                typeItem = new TreeViewItem() { id = id++, displayName = "PlayerLoop" };
                parent.AddChild(typeItem);
            }
            else
            {
                typeItem = new TreeViewItem() { id = id++, displayName = "(SubSystem) " + type?.ToString() };
                parent.AddChild(typeItem);
            }
            
            if (update != null)
            {
                // var updateTitle = new TreeViewItem() { id = id++, displayName = "Update Delegates" };
                AddDelegateRecursive(update, ref id, typeItem);
                // typeItem.AddChild(updateTitle);
            }

            if (sslist != null)
            {
                // var ssTitle = new TreeViewItem() { id = id++, displayName = "Sub Systems"};
                // typeItem.AddChild(ssTitle);

                foreach (var ss in sslist)
                {
                    AddSystemReculsive(ss, ref id, typeItem);
                }
            }
        }

        void AddDelegateRecursive(Delegate d, ref int id, TreeViewItem parent)
        {
            if (d != null)
            {
                var dItem = new TreeViewItem()
                {
                    id = id++,
                    displayName = string.Format("(Delegate) Target : {0}, Method : {1}", d.Target, d.Method)
                };
                parent.AddChild(dItem);

                var childs = d.GetInvocationList();
                if (childs != null &&
                    childs.Length > 1)
                {
                    foreach(var c in childs)
                    {
                        AddDelegateRecursive(c, ref id, dItem);
                    }
                }
            }
        }
    }
}
#endif