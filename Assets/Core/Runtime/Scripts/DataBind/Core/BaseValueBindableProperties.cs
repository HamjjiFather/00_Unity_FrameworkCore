using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class BaseValueBindableProperties<T, TV> : BindableProperties<T, TV>
        where T : Component where TV : IEnumerable
    {
        [HideInInspector]
        public string propertyName;

        /// <summary>
        ///     기준이 되는 타입.
        /// </summary>
        [HideInInspector]
        public Component targetComponent;

        private PropertyInfo[] _propertyInfo;

        public IEnumerable<Component> GetComponents =>
            targetComponents.Select (x => x.GetComponent (targetComponent.GetType ())).ToArray ();

        public IEnumerable<PropertyInfo> PropertyInfo
        {
            get
            {
                if (_propertyInfo == null || !_propertyInfo.Any ())
                    _propertyInfo = GetComponents.Select (x => x.GetType ().GetProperty (propertyName)).ToArray ();

                return _propertyInfo;
            }
        }

        protected override TV GetDelegate ()
        {
            return (TV) PropertyInfo.Select (info => info.GetValue (propertyName));
        }
    }
}