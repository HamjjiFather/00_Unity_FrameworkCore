using UnityEditor;

namespace KKSFramework.Editor
{
    public class CustomHierarchyColorWindow : EditorWindow
    {
        private void OnGUI ()
        {
            CustomHierarchyEditor.ActiveNameTextColor =
                EditorGUILayout.ColorField ("Activated, NonPrefab Font Color",
                    CustomHierarchyEditor.ActiveNameTextColor);
            CustomHierarchyEditor.InactiveNameTextColor =
                EditorGUILayout.ColorField ("Inactivated, NonPrefab Font Color",
                    CustomHierarchyEditor.InactiveNameTextColor);
            CustomHierarchyEditor.BackGroundColor =
                EditorGUILayout.ColorField ("Background Color", CustomHierarchyEditor.BackGroundColor);

            EditorPrefs.SetString (nameof (CustomHierarchyEditor.ActiveNameTextColor),
                CustomHierarchyEditor.ActiveNameTextColor.ToRGBHex ());
            EditorPrefs.SetString (nameof (CustomHierarchyEditor.InactiveNameTextColor),
                CustomHierarchyEditor.InactiveNameTextColor.ToRGBHex ());
            EditorPrefs.SetString (nameof (CustomHierarchyEditor.BackGroundColor),
                CustomHierarchyEditor.BackGroundColor.ToRGBHex ());
        }

        [MenuItem ("Framework/Custom Hierarchy Color Editor")]
        public static void ShowWindow ()
        {
            GetWindow<CustomHierarchyColorWindow> ("HierarchyEditor");
        }
    }
}