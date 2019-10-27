using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

[CreateAssetMenu]
public class CharacterStatusTable : ScriptableObject
{
    [SerializeField]
    private string characterName;
    public string CharacterName { get => characterName; }

    [SerializeField]
    private int hp;
    public int HP { get => hp; }

    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; }

    [SerializeField]
    private int score;
    public int Score { get => score; }

    [SerializeField]
    private bool doAttack;
    public bool DoAttack { get => doAttack; }

    [SerializeField]
    private float bulletInterval;
    public float BulletInterval { get => bulletInterval; }

    [SerializeField]
    private float bulletSpeed;
    public float BulletSpeed { get => bulletSpeed; }

    [SerializeField]
    private GameObject dieEffect;
    public GameObject DieEffect { get => dieEffect; }

#if UNITY_EDITOR

    [CustomEditor(typeof(CharacterStatusTable))]
    private class CharacterStatusTableEx : Editor
    {
        private CharacterStatusTable Target { get => target as CharacterStatusTable; }

        public override void OnInspectorGUI()
        {
            Target.characterName = EditorGUILayout.TextField("Name", Target.characterName);
            Target.hp = EditorGUILayout.IntSlider("HP", Target.hp, 1, 100);
            Target.movementSpeed = EditorGUILayout.FloatField("MovementSpeed", Target.movementSpeed);
            Target.score = EditorGUILayout.IntField("Score", Target.score);
            Target.doAttack = EditorGUILayout.Toggle("DoAttack", Target.doAttack);
            if(Target.doAttack)
            {
                EditorGUI.indentLevel++;

                Target.bulletInterval = EditorGUILayout.FloatField("BulletInterval", Target.bulletInterval);
                Target.bulletSpeed = EditorGUILayout.FloatField("BulletSpeed", Target.bulletSpeed);

                EditorGUI.indentLevel--;
            }
            Target.dieEffect = EditorGUILayout.ObjectField("DieEffect", Target.dieEffect, typeof(GameObject), allowSceneObjects: false) as GameObject;

            EditorUtility.SetDirty(Target);

            if(GUILayout.Button("Save"))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }

#endif
}
