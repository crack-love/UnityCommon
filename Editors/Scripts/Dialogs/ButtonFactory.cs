using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 5:48:29, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    public class ButtonFactory
    {
        public Button Create(string text)
        {
            return new Button(text);
        }

        public Button Submit(string text = "Submit")
        {
            var res = Create(text);

            res.IsSubmit = true;

            return res;
        }

        public Button Cancel(string text = "Cancel")
        {
            var res = Create(text);

            res.IsCancel = true;

            return res;
        }
    }
}