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
    [Header("수명")]
    public float Lifespan;

    [Header("발사 속도")]
    public float ShotSpd;

    [Header("사운드")]
    public AudioClip Sound;

    [Header("트레일")]
    public Material Trail;
}

[System.Serializable]
public struct BulletState
{
    [Header("파워")]
    public float Damage;

    [Header("스피드")]
    public float Spd;

    [Header("총알 방향")]
    public EBulletDir Dir;

    [Header("타겟")]
    public Transform Target;

    [Header("효과음")]
    public AudioClip AudioEffect;
}

[CreateAssetMenu(menuName = "ScriptableObjects/BulletMaker")]
public class BulletValue : ScriptableObject
{
    [Header("이름")]
    public string bulletName;

    [Header("이미지")]
    public Sprite bulletImage;

    [Header("총알 매니저")]
    public BulletMGState mgState;

    [Header("총알 스텟")]
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

        value.bulletImage = (Sprite)EditorGUILayout.ObjectField("이미지", 
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
                // 프리팹으로 만들 경로와 이름을 설정합니다.
                string prefabPath = "Assets/Prefabs/";
                string prefabName = selectedObject.name + ".prefab";

                // 프리팹을 생성합니다.
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, prefabPath + prefabName);
                Debug.Log("생성 성공");
            }
            else
            {
                Debug.Log("생성 실패");
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
