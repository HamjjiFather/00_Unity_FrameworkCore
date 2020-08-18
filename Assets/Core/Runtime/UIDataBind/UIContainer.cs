using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (IBinder))]
    public class UIContainer : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// 컨테이너에 포함된 컴포넌트 딕셔너리.
        /// </summary>
        public Dictionary<string, UIBehaviour> BindableComponents { get; } = new Dictionary<string, UIBehaviour> ();


        #region UnityMethods

        private void Awake ()
        {
            var classes = GetComponents<IBinder> ();
            classes.Foreach (monoBehaviour =>
            {
                var fields = monoBehaviour
                    .GetType ()
                    .GetFields (BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where (x => x.HasAttribute<ResolveUIAttribute> ());
                fields.Foreach (field =>
                {
                    var attribute = field.GetCustomAttribute<ResolveUIAttribute> (true);
                    field.SetValue (monoBehaviour, Resolve<UIBehaviour> (attribute.Key));
                });
            });
        }


        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        public void AddComponent (string key, UIBehaviour uiBehaviour)
        {
            BindableComponents.Add (key, uiBehaviour);
        }


        public T Resolve<T> (string key) where T : UIBehaviour
        {
            if (Vaildate (key))
            {
                return BindableComponents[key] as T;
            }

            Debug.Log ($"No UIBehaviour component named {key}.");
            return default;
        }


        public bool Vaildate (string key)
        {
            return BindableComponents.ContainsKey (key);
        }


        public void Dispose ()
        {
            BindableComponents.Clear ();
            GC.Collect ();
        }
    }
}