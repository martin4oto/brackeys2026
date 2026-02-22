using UnityEngine;
using System.Collections.Generic;
using System;

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
    public List<String> names;
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
            dmg=dmg*(100-GameController.Instance.characters[targets].armor);
            dmg=(int)(dmg/100);
            if(dmg<0)
                dmg=0;
            GameController.Instance.characters[targets].currentHP-=dmg;
        }
    }
    public void EnemyAttackRanged(int targets, int user)
    {
        for(int i=0;i<GameController.Instance.characters.Length;i++)
        {
            if(GameController.Instance.characters[i].characterData!=null && GameController.Instance.characters[i].alive)
            {
                
            }
        }
    }
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        names=new List<string>{"Attack","Heal","Heal Potion for 30 HP","Team Heal", "Attack"};
        skills.Add(Attack); //0
        skills.Add(Heal); //1
        skills.Add(HealPotion30); //2
        skills.Add(EnemyTeamHeal); //3
        skills.Add(EnemyAttack); //4
    }
    public void use(int id, int targets, int user)
    {
        skills[id](targets,user);
    }
}
