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
        Event e = Event.current;
        if (e == null) return;

        // スペースキーが押されたかどうかをチェック
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space && !e.control)
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
            e.Use();
        }
        
        // Shift+Zキーが押されたかどうかをチェック
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z && e.shift)
        {
            // アクティブなシーンビューを取得
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                // 現在のレンダリングモードを次のモードに切り替え
                switch (sceneView.renderMode)
                {
                    case DrawCameraMode.Textured:
                        sceneView.renderMode = DrawCameraMode.Wireframe;
                        break;
                    case DrawCameraMode.Wireframe:
                        sceneView.renderMode = DrawCameraMode.TexturedWire;
                        break;
                    case DrawCameraMode.TexturedWire:
                    default:
                        sceneView.renderMode = DrawCameraMode.Textured;
                        break;
                }
                
                // シーンビューを再描画
                sceneView.Repaint();
            }
            
            // イベントを使用済みに設定
            e.Use();
        }

        // Ctrl+Spaceキーが押されたかどうかをチェック
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space && e.control)
        {
            // 選択中のオブジェクトを取得
            GameObject[] selectedObjects = Selection.gameObjects;

            // 選択中のオブジェクトの'Tag'を切り替え
            foreach (GameObject obj in selectedObjects)
            {
                Undo.RecordObject(obj, "Toggle EditorOnly Tag");
                if (obj.CompareTag("EditorOnly"))
                {
                    // 'EditorOnly'タグを外す
                    obj.tag = "Untagged";
                }
                else
                {
                    // 'EditorOnly'タグを付ける
                    obj.tag = "EditorOnly";
                }
            }

            // イベントを使用済みに設定
            e.Use();
        }
    }
}