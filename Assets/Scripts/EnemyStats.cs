using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public float hp;
    public float baseDmg;
    public Sprite combatSprite;
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
}
