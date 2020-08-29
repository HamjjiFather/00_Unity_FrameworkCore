using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class ImageSpritePropertiesBind : BindableProperties<Image, Sprite>
    {
        protected override Sprite GetDelegate () => targetComponents.First ().GetComponent<Image> ().sprite;

        protected override void SetDelegate (Sprite value) => targetComponents.Foreach (x => x.sprite = value);
    }
}