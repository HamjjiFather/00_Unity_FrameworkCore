﻿using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class ImageSpritePropertiesBind : BindableProperties<Image, Sprite[]>
    {
        protected override Sprite[] GetDelegate ()
        {
            return targetComponents.Select (x => x.GetComponent<Image> ().sprite).ToArray ();
        }

        protected override void SetDelegate (Sprite[] values)
        {
            targetComponents.ZipForEach (values, (comp, value) => comp.GetComponent<Image> ().sprite = value);
        }
    }
}