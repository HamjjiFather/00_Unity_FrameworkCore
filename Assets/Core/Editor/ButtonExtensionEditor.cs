using System.Collections.Generic;
using System.Reflection;
using KKSFramework.UI;
using UnityEditor;
using UnityEditor.UI;

namespace KKSFramework.Editor
{
    /// <summary>
    /// ButtonExtension 클래스 커스텀 에디터.
    /// </summary>
    [CustomEditor (typeof (ButtonExtension), true)]
    [CanEditMultipleObjects]
    public class ButtonExtensionEditor : ButtonEditor
    {
        #region Fields & Property

        private ButtonExtension _target;

        #endregion


        #region UnityMethods

        protected override void OnEnable ()
        {
            _target = target as ButtonExtension;
            base.OnEnable ();
        }


        public override void OnInspectorGUI ()
        {
            EditorGUILayout.BeginVertical ("Box");
            {
                EditorGUILayout.LabelField ($"[Holding Action]", EditorStyles.boldLabel);
                
                EditorGUILayout.PropertyField (
                    serializedObject.FindProperty (nameof (_target.useHoldingAction)));
                serializedObject.ApplyModifiedProperties ();

                if (!_target.useHoldingAction)
                {
                    EditorGUILayout.EndVertical ();
                }
                else
                {
                    EditorGUILayout.PropertyField (
                        serializedObject.FindProperty (nameof (_target.defaultHoldDuration)));
                    EditorGUILayout.EndVertical ();
                }
            }
            
            
            EditorGUILayout.BeginVertical ("Box");
            {
                EditorGUILayout.LabelField ($"[Scale Effect]", EditorStyles.boldLabel);
                
                EditorGUILayout.PropertyField (
                    serializedObject.FindProperty (nameof (_target.useScaleEffect)));
                serializedObject.ApplyModifiedProperties ();

                if (!_target.useScaleEffect)
                {
                    EditorGUILayout.EndVertical ();
                }
                else
                {
                    EditorGUILayout.PropertyField (
                        serializedObject.FindProperty (nameof (_target.scaleEffectDuration)));
                    
                    EditorGUILayout.PropertyField (
                        serializedObject.FindProperty (nameof (_target.toScaleValue)));
                    
                    serializedObject.ApplyModifiedProperties ();
                    EditorGUILayout.EndVertical ();
                }
            }

            
            EditorGUILayout.BeginVertical ("Box");
            {
                EditorGUILayout.LabelField ($"[Button Extension Base]", EditorStyles.boldLabel);
                
                // 색상 변경 옵션.
                EditorGUILayout.PropertyField (serializedObject.FindProperty (nameof (ButtonExtension.soundTypeEnum)));

                // 포함된 텍스트.
                EditorGUILayout.PropertyField (serializedObject.FindProperty (nameof (ButtonExtension.buttonText)));
                serializedObject.ApplyModifiedProperties ();

                EditorGUILayout.EndVertical ();
            }
            
            
            EditorGUILayout.BeginVertical ("Box");
            {
                EditorGUILayout.LabelField ("[Button]", EditorStyles.boldLabel);
                base.OnInspectorGUI ();
                EditorGUILayout.EndVertical ();
            }
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}