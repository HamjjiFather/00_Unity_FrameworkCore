using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Graphics))]
    public class GraphicColorPropertyBind : BindableProperty<Graphic, Color>
    {
        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = GetComponent<Graphic> ();
        }
        
        protected override Color GetDelegate () => targetComponent.color;

        protected override void SetDelegate (Color value) => targetComponent.color = value;
    }
}