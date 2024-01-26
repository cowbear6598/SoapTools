using UnityEditor;
using UnityEngine;

namespace Test.Editor.Database.Editor
{
    public class SpriteOrderAutoDetector : EditorWindow
    {
        private float padding = 0.05f;

        [MenuItem("Soap/設定 Sprite Order")]
        public static void ShowWindow()
        {
            var window = GetWindow<SpriteOrderAutoDetector>();
            window.titleContent = new GUIContent("設定 Sprite Order");
            window.Show();
        }

        private void OnGUI()
        {
            padding = EditorGUILayout.FloatField("間距: ", padding);

            if (GUILayout.Button("更改"))
            {
                Change();
            }
        }

        private void Change()
        {
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                var selectedTrans = Selection.transforms[i];

                int order = (int)(selectedTrans.transform.position.y / padding);
                order *= -1;

                var spriteRenderer = selectedTrans.GetComponent<SpriteRenderer>();

                if (spriteRenderer == null)
                {
                    Debug.LogError($"沒有 SpriteRenderer: {selectedTrans.name}");
                    continue;
                }

                spriteRenderer.sortingOrder = order;
            }
        }
    }
}