using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class SceneUtils
{
    static SceneUtils()
    {
        // エディタの更新イベントにハンドラを追加
        EditorApplication.update += OnEditorUpdate;
        // 全てのエディタウィンドウに対してキーイベントを監視する
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnEditorUpdate()
    {
        // エディタの更新処理
    }

    private static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
    {
        CheckKeyPress();
    }

    private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
    {
        CheckKeyPress();
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        CheckKeyPress();
    }

    private static void CheckKeyPress()
    {
        // スペースキーが押されたかどうかをチェック
        if (Event.current != null && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
        {
            // 選択中のオブジェクトを取得
            GameObject[] selectedObjects = Selection.gameObjects;

            // 選択中のオブジェクトのアクティブ状態を切り替え
            foreach (GameObject obj in selectedObjects)
            {
                Undo.RecordObject(obj, "Toggle Active State");
                obj.SetActive(!obj.activeSelf);
            }

            // イベントを使用済みに設定
            Event.current.Use();
        }
    }
}
