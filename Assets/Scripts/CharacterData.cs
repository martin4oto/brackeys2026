using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float level;
    public float maxHP;
    public float currentHP;
    public float mana;
    public float critChance;
    public float dodgeChance;
    public ScriptableObject skill1;
    public ScriptableObject skill2;
    public ScriptableObject skill3;
    public Sprite combatSprite;
    public Sprite deadSprite;
    public int range;
    public bool alive;
}
