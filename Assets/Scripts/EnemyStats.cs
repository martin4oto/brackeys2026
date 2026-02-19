using UnityEngine;
using System;
[Serializable]
public class EnemyData
{
    public EnemyStats enemyStats;
    public int hp;
    public float baseDmg;
    public bool alive; 
}
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;

    public int maxHp;
    public Sprite combatSprite;
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Sprite deadSprite;

}
