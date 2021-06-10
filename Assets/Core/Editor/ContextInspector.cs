using KKSFramework.DataBind;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    [CustomEditor(typeof(Context))]
    public class ContextInspector : UnityEditor.Editor
    {
        #region Fields & Property
        
        private Context _target;

        private bool _onValidateCheck;
        
        #endregion


        #region UnityMethods
        
        
        private void OnEnable ()
        {
            _target = target as Context;
        }


        private void OnDisable ()
        {
            _target = null;
        }
        
        
        public override void OnInspectorGUI ()
        {
            var targetClass = _target.GetComponent<IResolveTarget> ();
            
            if (targetClass == null)
            {
                GUI.color = Color.red;
                EditorGUILayout.LabelField ("No classes inherited IResolver interface.");
                GUI.color = Color.white;
                return;
            }
            
            base.OnInspectorGUI ();

            var checkBindable = GUILayout.Button("Validate check");

            if (_onValidateCheck || !checkBindable) return;
            _onValidateCheck = true;
            var vaildateCheckResult = _target.CheckResolve ();
                
            Debug.Log (vaildateCheckResult);
            _onValidateCheck = false;
        }

        #endregion
    }
}