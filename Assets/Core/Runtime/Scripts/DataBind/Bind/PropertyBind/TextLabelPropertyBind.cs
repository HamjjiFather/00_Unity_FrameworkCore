using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Text))]
    public class TextLabelPropertyBind : BindableProperty<Text, string>
    {
        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = GetComponent<Text> ();
        }

        protected override string GetDelegate ()
        {
            return targetComponent.text;
        }

        protected override void SetDelegate (string value)
        {
            targetComponent.text = value;
        }
    }
}