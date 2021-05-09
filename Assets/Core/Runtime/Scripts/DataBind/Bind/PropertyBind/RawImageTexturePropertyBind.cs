using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (RawImage))]
    public class RawImageTexturePropertyBind : BindableProperty<RawImage, Texture>
    {
        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = GetComponent<RawImage> ();
        }

        protected override Texture GetDelegate ()
        {
            return targetComponent.texture;
        }

        protected override void SetDelegate (Texture value)
        {
            targetComponent.texture = value;
        }
    }
}