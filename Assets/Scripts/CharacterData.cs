using System;
using UnityEngine;
[Serializable]
public class Data
{
    public CharacterData characterData;
    public float level;
    public float currentHP;
    public float mana;
    public float critChance;
    public bool alive;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    public float maxHP;
    public float maxMana;

    public ScriptableObject skill1;
    public ScriptableObject skill2;
    public ScriptableObject skill3;
    public Sprite combatSprite;
    public Sprite deadSprite;
    public int range;

}
