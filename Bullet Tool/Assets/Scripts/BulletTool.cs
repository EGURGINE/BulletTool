using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public enum EBulletDir 
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

[System.Serializable]
public struct BulletMGState
{
    [Header("����")]
    public float Lifespan;

    [Header("�߻� �ӵ�")]
    public float ShotSpd;

    [Header("����")]
    public AudioClip Sound;

    [Header("Ʈ����")]
    public Material Trail;
}

[System.Serializable]
public struct BulletState
{
    [Header("�Ŀ�")]
    public float Damage;

    [Header("���ǵ�")]
    public float Spd;

    [Header("�Ѿ� ����")]
    public EBulletDir Dir;

    [Header("Ÿ��")]
    public Transform Target;

    [Header("ȿ����")]
    public AudioClip AudioEffect;
}

[CreateAssetMenu(menuName = "ScriptableObjects/BulletMaker")]
public class BulletValue : ScriptableObject
{
    [Header("�̸�")]
    public string bulletName;

    [Header("�̹���")]
    public Sprite bulletImage;

    [Header("�Ѿ� �Ŵ���")]
    public BulletMGState mgState;

    [Header("�Ѿ� ����")]
    public BulletState state;
}


[CustomEditor(typeof(BulletValue))]
public class BulletTool : Editor
{
    BulletValue value;




    private void OnEnable()
    {
        value = (BulletValue)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space();

        value.bulletImage = (Sprite)EditorGUILayout.ObjectField("�̹���", 
            value.bulletImage, typeof(Sprite), true);

        EditorGUILayout.Space();
        GUILine(4);
        EditorGUILayout.Space();

        base.OnInspectorGUI();


        EditorGUILayout.Space();
        GUILine(4);
        EditorGUILayout.Space();

        if (GUILayout.Button("Create Bullet"))
        {
            GameObject selectedObject = new GameObject(value.bulletName.ToString());
            selectedObject.AddComponent<BulletMG>().StartSet(value.mgState, value.state, value.bulletImage);
            selectedObject.AddComponent<SpriteRenderer>().sprite = value.bulletImage;

            if (selectedObject != null)
            {
                // ���������� ���� ��ο� �̸��� �����մϴ�.
                string prefabPath = "Assets/Prefabs/";
                string prefabName = selectedObject.name + ".prefab";

                // �������� �����մϴ�.
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, prefabPath + prefabName);
                Debug.Log("���� ����");
            }
            else
            {
                Debug.Log("���� ����");
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    void GUILine(int lineHeight = 1)
    {
        EditorGUILayout.Space();
        Rect rect = EditorGUILayout.GetControlRect(false, lineHeight);
        rect.height = lineHeight;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.Space();

    }
}
