using Common;
using CommonEditor;
using UnityEngine;

namespace Test
{
    [ExecuteAlways]
    class Test2 : MonoBehaviour//, IT
    {
        [Common.Button("OnEnable")]
        public string a;
        public bool btn;
        private void OnEnable()
        {
            //var d = Dialog.Factory.Create();
            //d.AddField(Field.Factory.Header("HEADER"));
            //d.AddField(Field.Factory.HelpBox("HB"));
            //d.AddField(Field.Factory.Float(0f));
            //d.AddButton(Button.Factory.Submit());
            //d.AddButton(Button.Factory.Cancel());
            //d.Show();
        }
    }
}