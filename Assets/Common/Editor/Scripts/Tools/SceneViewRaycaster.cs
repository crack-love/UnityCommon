using System;
using UnityEditor;
using UnityEngine;

namespace CommonEditor
{
    public static class SceneViewRaycaster
    {
        /// <summary>
        /// SceneView.beforeSceneGui
        /// </summary>
        public static event Action<SceneView> BeforeSceneGUI
        {
            add => SceneView.beforeSceneGui += value;
            remove => SceneView.beforeSceneGui -= value;
        }

        /// <summary>
        /// SceneView.duringSceneGui
        /// </summary>
        public static event Action<SceneView> DuringSceneGUI
        {
            add => SceneView.duringSceneGui += value;
            remove => SceneView.duringSceneGui -= value;
        }

        public static bool MousePointRaycast<T>(SceneView view, out RaycastHit hit, out T hitComponent, int layermask = ~0) where T : class
        {
            hitComponent = null;

            if (MousePointRaycast(view, out hit, layermask))
            {
                hitComponent = hit.transform?.GetComponent<T>();
            }

            return hitComponent != null;
        }

        public static bool MousePointRaycast(SceneView view, out RaycastHit hit, int layermask = ~0)
        {
            Event e = Event.current;
            if (e == null || !e.isMouse)
            {
                hit = default;
                return false;
            }

            // get mouse position
            var mousePos = MousePointToWorld(view);

            // raycast
            Ray ray = view.camera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, 10000f, layermask))
            {
                return true;
            }

            return false;
        }

        public static Vector3 MousePointToWorld(SceneView view)
        {
            float ppp = EditorGUIUtility.pixelsPerPoint;
            Vector2 mousePos = Event.current.mousePosition * ppp;
            mousePos.y = view.camera.pixelHeight - mousePos.y; // reverse y

            Vector3 res = Vector3.zero;
            try
            {
                res = view.camera.ScreenToWorldPoint(mousePos);
            }
            catch (Exception e)
            {
                // ignore out of view frustrum exception
                if (e.Message.Contains("frustrum"))
                {

                }
                else
                {
                    throw e;
                }
            }

            return res;
        }
    }
}
