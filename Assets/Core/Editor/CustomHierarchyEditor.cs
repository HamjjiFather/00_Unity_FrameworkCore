using System.Collections;
using KKSFramework.DataBind;
using UnityEditor;
using UnityEngine;

namespace KKSFramework
{
    [InitializeOnLoad]
    public class CustomHierarchyEditor
    {
        public static Color ActiveNameTextColor = new Color (0.88f, 0.29f, .05f);

        public static Color InactiveNameTextColor = new Color (0.42f, 0.42f, .42f);

        public static Color BackGroundColor = new Color (.2196f, .2196f, .2196f);

        static CustomHierarchyEditor ()
        {
            var activeColor = BaseExtension.ToColor (EditorPrefs.GetString (nameof(ActiveNameTextColor), ActiveNameTextColor.ToRGBHex ()));
            ActiveNameTextColor = activeColor;
            var inActiveColor = BaseExtension.ToColor (EditorPrefs.GetString (nameof(InactiveNameTextColor), InactiveNameTextColor.ToRGBHex ()));
            InactiveNameTextColor = inActiveColor;
            var bgColor = BaseExtension.ToColor (EditorPrefs.GetString (nameof(BackGroundColor), BackGroundColor.ToRGBHex ()));
            BackGroundColor = bgColor; 
            
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI (int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject (instanceID);
            if (obj == null) return;

            if (obj is GameObject toGameObj)
            {
                if (!toGameObj.GetComponent<Bindable> ()) return;
                var binableComp = toGameObj.GetComponent<Bindable> ();
                var isPrefab = PrefabUtility.GetPrefabAssetType (obj) == PrefabAssetType.Regular;
                var isSelection = ((IList) Selection.instanceIDs).Contains (instanceID);
                var isActive = toGameObj.activeInHierarchy;
                var targetContext = binableComp.TargetContext;

                var fontColor = isActive ? ActiveNameTextColor : InactiveNameTextColor;
                var bgColor = isSelection ? new Color (0.24f, 0.48f, 0.90f) : BackGroundColor;
                var offsetRect = new Rect (selectionRect.position + new Vector2 (18, 0),
                    selectionRect.size - (isPrefab ? new Vector2 (18, 0) : Vector2.zero));
                
                EditorGUI.DrawRect (offsetRect, bgColor);
                EditorGUI.LabelField (offsetRect,
                    $"{obj.name} (Bind as {toGameObj.GetComponent<Bindable> ().ContainerPath} to {(targetContext ? targetContext.name : null)})",
                    new GUIStyle
                    {
                        normal = new GUIStyleState {textColor = fontColor},
                        fontStyle = FontStyle.Italic
                    }
                );
            }
        }
    }
}