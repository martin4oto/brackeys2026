using System;
using UnityEngine;
[Serializable]
public class Data
{
    public CharacterData characterData;
    public float level;
    public int currentHP;
    public float mana;
    public float critChance;
    public bool alive;
    public int atkBonus;

    public Data Copy()
    {
        Data newData = new Data();

        newData.characterData = characterData;
        newData.level = level;
        newData.currentHP = currentHP;
        newData.mana = mana;
        newData.critChance = critChance;
        newData.alive = alive;
        newData.atkBonus= atkBonus;

        return newData;
    }

}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    public int maxHP;
    public float maxMana;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Sprite combatSprite;
    public Sprite deadSprite;
    public int range;

    public int baseAtk;
}
