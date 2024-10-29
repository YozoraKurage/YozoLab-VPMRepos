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
        // �X�y�[�X�L�[�������ꂽ���ǂ������`�F�b�N
        if (Event.current != null && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
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
            Event.current.Use();
        }
    }
}
