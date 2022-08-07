using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 5:48:29, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon.Editors
{
    public class DialogFactory
    {
        public Dialog Create(string title = "Dialog")
        {
            var res = new Dialog();
            res.Title = title;
            return res;
        }

        public Dialog AddConfirmCancel(Dialog src)
        {
            src.AddButton(Button.Factory.Submit("Confirm"));
            src.AddButton(Button.Factory.Cancel());
            return src;
        }

        public Dialog CreateMessage(string message, MessageType type = MessageType.Info)
        {
            Dialog res = Create();
            res.AddField(Field.Factory.HelpBox(message, type, true));
            return res;
        }

        public Dialog CreateMessageConfirm(string message, MessageType type = MessageType.Info)
        {
            Dialog res = CreateMessage(message, type);
            AddConfirmCancel(res);
            return res;
        }
    }
}