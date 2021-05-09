using KKSFramework.UI;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class ConvertUGUIToExtensionUIMenu
    {
        #region Static Methods

        [MenuItem ("CONTEXT/Button/Button Component Change", validate = false)]
        public static void ChangeButtonComponent (MenuCommand command)
        {
            ConvertTo<ButtonExtension> (command.context);
        }

        [MenuItem ("CONTEXT/Button/Button Component Change", validate = true)]
        public static bool IsBaseButton (MenuCommand command)
        {
            try
            {
                return !(ButtonExtension) command.context;
            }
            catch
            {
                return true;
            }
        }


        [MenuItem ("CONTEXT/Toggle/Toggle Component Change", validate = false)]
        public static void ChangeToggleComponent (MenuCommand command)
        {
            ConvertTo<ToggleExtension> (command.context);
        }

        [MenuItem ("CONTEXT/Toggle/Toggle Component Change", validate = true)]
        public static bool IsBaseToggle (MenuCommand command)
        {
            try
            {
                return !(ToggleExtension) command.context;
            }
            catch
            {
                return true;
            }
        }


        /// <summary>
        ///     Convert to the specified component.
        /// </summary>
        private static void ConvertTo<T> (Object context) where T : MonoBehaviour
        {
            if (!(context is MonoBehaviour target)) return;
            var so = new SerializedObject (target);
            so.Update ();

            var oldEnable = target.enabled;
            target.enabled = false;

            // Find MonoScript of the specified component.
            foreach (var script in Resources.FindObjectsOfTypeAll<MonoScript> ())
            {
                if (script.GetClass () != typeof (T))
                    continue;

                // Set 'm_Script' to convert.
                so.FindProperty ("m_Script").objectReferenceValue = script;
                so.ApplyModifiedProperties ();
                break;
            }

            ((MonoBehaviour) so.targetObject).enabled = oldEnable;
        }

        #endregion
    }
}