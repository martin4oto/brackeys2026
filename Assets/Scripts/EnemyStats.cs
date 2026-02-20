using UnityEngine;
using System;
[Serializable]
public class EnemyData
{
    public EnemyStats enemyStats;
    public int currentId;
    public int hp;
    public int baseDmg;
    public bool alive; 
}
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;

    public int maxHp;
    public Sprite combatSprite;
    public int skill1;
    public int skill2;
    public int skill3;
    public Sprite deadSprite;
    public bool ranged;

}
