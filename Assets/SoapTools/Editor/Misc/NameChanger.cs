using UnityEditor;
using UnityEngine;

namespace SoapTools.Editor
{
    public class NameChanger : EditorWindow
    {
        private string prefixesName;
        private string suffixesName;
        private int    startOrder;

        [MenuItem("Soap/物件改名")]
        private static void ShowWindow()
        {
            var window = GetWindow<NameChanger>();
            window.titleContent = new GUIContent("Setting");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField($"Selection Count: {Selection.count}");

            prefixesName = EditorGUILayout.TextField("Prefixes Name: ", prefixesName);
            suffixesName = EditorGUILayout.TextField("Suffixes Name: ", suffixesName);
            startOrder   = EditorGUILayout.IntField("Start Order: ", startOrder);

            if (GUILayout.Button("Change"))
            {
                Change();
            }
        }

        private void Change()
        {
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                var selectedObj = Selection.transforms[i];

                selectedObj.name = prefixesName + (startOrder + i) + suffixesName;
            }
        }
    }
}