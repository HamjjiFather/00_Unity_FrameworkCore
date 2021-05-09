using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Image))]
    public class ImageSpritePropertyBind : BindableProperty<Image, Sprite>
    {
        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = GetComponent<Image> ();
        }

        protected override Sprite GetDelegate ()
        {
            return targetComponent.sprite;
        }

        protected override void SetDelegate (Sprite value)
        {
            targetComponent.sprite = value;
        }
    }
}