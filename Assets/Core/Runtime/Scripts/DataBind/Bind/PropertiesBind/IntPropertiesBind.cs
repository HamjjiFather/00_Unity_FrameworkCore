using System.Linq;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public class IntPropertiesBind : BaseValueBindableProperties<Component, int>
    {
        protected override void SetDelegate (int value)
        {
            targetComponents.Select (x => x.GetComponent (targetComponent.GetType ()))
                .Foreach (x => x.GetType ().GetProperty (propertyName)?.SetValue (x, value));
        }
    }
}