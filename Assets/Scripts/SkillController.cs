using UnityEngine;
using System.Collections.Generic;

public class SkillController : MonoBehaviour
{
    public static SkillController Instance {
        get;
        set;
    }
    public delegate void SkillAction(int targets, int user);
    public List<SkillAction> skills = new List<SkillAction>();
    
    public void Attack(int targets, int user)
    {
        Data caster=GameController.Instance.characters[user];
        int dmg=(int)(caster.characterData.baseAtk*((caster.characterData.maxMana/100)+1));
        GameController.Instance.enemies[targets].hp-=dmg;
    }
    
    public void Heal(int targets, int user)
    {
        Data caster=GameController.Instance.characters[user];
        int hpHeal=(int)caster.characterData.maxMana/4;
        GameController.Instance.characters[targets].currentHP+=hpHeal;
        if(GameController.Instance.characters[targets].currentHP>GameController.Instance.characters[targets].characterData.maxHP)
        {
            GameController.Instance.characters[targets].currentHP=GameController.Instance.characters[targets].characterData.maxHP;
        }
    }
    public void HealPotion30(int targets, int user)
    {
        int hpHeal = 30;
        GameController.Instance.characters[targets].currentHP+=hpHeal;
        if(GameController.Instance.characters[targets].currentHP>GameController.Instance.characters[targets].characterData.maxHP)
        {
            GameController.Instance.characters[targets].currentHP=GameController.Instance.characters[targets].characterData.maxHP;
        }
    }
    public void EnemyTeamHeal(int targets, int user)
    {
        int hpHeal=20;
        for(int i=0;i<GameController.Instance.enemies.Length;i++)
        {
            if(GameController.Instance.enemies[i].enemyStats!=null && GameController.Instance.enemies[i].alive)
            {
                GameController.Instance.enemies[i].hp+=hpHeal;
                if(GameController.Instance.enemies[i].hp>GameController.Instance.enemies[i].enemyStats.maxHp)
                {
                    GameController.Instance.enemies[i].hp=GameController.Instance.enemies[i].enemyStats.maxHp;
                }
            }
        }
    }
    public void EnemyAttack(int targets, int user)
    {
        if(GameController.Instance.characters[targets].characterData!=null && GameController.Instance.characters[targets].alive)
        {
            EnemyData caster= GameController.Instance.enemies[user];
            int dmg=caster.baseDmg;
            GameController.Instance.characters[targets].currentHP-=dmg;
        }
    }
    public void EnemyAttackRanged(int targets, int user)
    {
        
    }
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        skills.Add(Attack);
        skills.Add(Heal);
        skills.Add(HealPotion30);
        skills.Add(EnemyTeamHeal);
        skills.Add(EnemyAttack);
    }
    public void use(int id, int targets, int user)
    {
        skills[id](targets,user);
    }
}
