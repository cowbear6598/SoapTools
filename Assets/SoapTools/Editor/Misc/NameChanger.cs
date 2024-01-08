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
            window.titleContent = new GUIContent("物件改名");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField($"選擇物件數量: {Selection.count}");

            prefixesName = EditorGUILayout.TextField("前墜名稱: ", prefixesName);
            suffixesName = EditorGUILayout.TextField("後墜名稱: ", suffixesName);
            startOrder   = EditorGUILayout.IntField("起始編號: ", startOrder);

            if (GUILayout.Button("更改"))
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