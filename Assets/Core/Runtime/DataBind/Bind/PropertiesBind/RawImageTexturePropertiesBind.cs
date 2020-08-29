using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class RawImageTextureBindableProperties : BindableProperties<RawImage, Texture>
    {
        protected override Texture GetDelegate () => targetComponents.First ().GetComponent<RawImage> ().texture;

        protected override void SetDelegate (Texture value) => targetComponents.Foreach (x => x.texture = value);
    }
}