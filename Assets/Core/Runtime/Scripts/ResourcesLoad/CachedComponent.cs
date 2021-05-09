using System;
using System.Collections.Generic;
using UnityEngine;

namespace KKSFramework
{
    /// <summary>
    ///     캐시 컴포넌트 클래스.
    ///     2018.06.03. 작성.
    /// </summary>
    public class CachedComponent : MonoBehaviour
    {
        #region Fields & Property

        /// <summary>
        ///     이 오브젝트에 캐시된 컴포넌트 딕셔너리.
        /// </summary>
        private Dictionary<Type, object> _cachedComponentDict;

        public Dictionary<Type, object> CachedComponentDict
        {
            get
            {
                if (_cachedComponentDict == null) _cachedComponentDict = new Dictionary<Type, object> ();

                return _cachedComponentDict;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        ///     캐시된 컴포넌트 리턴.
        /// </summary>
        public Component GetCachedComponent (Type type)
        {
            if (!CachedComponentDict.ContainsKey (type)) CachedComponentDict.Add (type, GetComponent (type));

            return CachedComponentDict[type] as Component;
        }


        /// <summary>
        ///     캐시된 컴포넌트 리턴.
        /// </summary>
        public Component[] GetCachedComponents (Type type)
        {
            if (!CachedComponentDict.ContainsKey (type)) CachedComponentDict.Add (type, GetComponents (type));

            return CachedComponentDict[type] as Component[];
        }


        /// <summary>
        ///     컴포넌트를 추가하고 캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        public Component AddCachedComponent (Type type)
        {
            var tempComponent = gameObject.AddComponent (type);
            CachedComponentDict.Add (type, tempComponent);
            return GetCachedComponent (type);
        }


        /// <summary>
        ///     캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        public T GetCachedComponent<T> ()
        {
            return GetCachedComponent (typeof (T)).GetComponent<T> ();
        }


        /// <summary>
        ///     캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        public T[] GetCachedComponents<T> ()
        {
            return Array.ConvertAll (GetCachedComponents (typeof (T)), input => input.GetComponent<T> ());
        }


        /// <summary>
        ///     컴포넌트를 추가하고 캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        public T AddCachedComponent<T> () where T : Component
        {
            return (T) AddCachedComponent (typeof (T));
        }

        #endregion
    }
}