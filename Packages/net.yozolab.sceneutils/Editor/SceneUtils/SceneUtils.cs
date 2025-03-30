using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class SceneUtils
{
    static SceneUtils()
    {
        // �G�f�B�^�̍X�V�C�x���g�Ƀn���h����ǉ�
        EditorApplication.update += OnEditorUpdate;
        // �S�ẴG�f�B�^�E�B���h�E�ɑ΂��ăL�[�C�x���g���Ď�����
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnEditorUpdate()
    {
        // �G�f�B�^�̍X�V����
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

        // �X�y�[�X�L�[�������ꂽ���ǂ������`�F�b�N
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
        {
            // �I�𒆂̃I�u�W�F�N�g���擾
            GameObject[] selectedObjects = Selection.gameObjects;

            // �I�𒆂̃I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ�
            foreach (GameObject obj in selectedObjects)
            {
                Undo.RecordObject(obj, "Toggle Active State");
                obj.SetActive(!obj.activeSelf);
            }

            // �C�x���g���g�p�ς݂ɐݒ�
            e.Use();
        }
        
        // Shift+Z�L�[�������ꂽ���ǂ������`�F�b�N
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z && e.shift)
        {
            // �A�N�e�B�u�ȃV�[���r���[���擾
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                // ���݂̃����_�����O���[�h�����̃��[�h�ɐ؂�ւ�
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
                
                // �V�[���r���[���ĕ`��
                sceneView.Repaint();
            }
            
            // �C�x���g���g�p�ς݂ɐݒ�
            e.Use();
        }
    }
}