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
    public void ManaPotion30(int targets, int user)
    {
        int manaHeal = 30;
        GameController.Instance.characters[targets].mana+=manaHeal;
        if(GameController.Instance.characters[targets].mana>GameController.Instance.characters[targets].characterData.maxMana)
        {
            GameController.Instance.characters[targets].mana=GameController.Instance.characters[targets].characterData.maxMana;
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
            float dmg=caster.baseDmg;
            dmg=dmg*(100-GameController.Instance.characters[targets].armor);
            int dmg2=(int)(dmg/100);
            if(dmg<0)
                dmg=0;
            GameController.Instance.characters[targets].currentHP-=dmg2;
        }
    }
    public void EnemyAttackRanged(int targets, int user)
    {
        EnemyData caster= GameController.Instance.enemies[user];
        float dmg=caster.baseDmg;
        for(int i=0;i<GameController.Instance.characters.Length;i++)
        {
            if(GameController.Instance.characters[i].characterData!=null && GameController.Instance.characters[i].alive)
            {
                dmg=caster.baseDmg;
                dmg=dmg*(100-GameController.Instance.characters[targets].armor);
                int dmg2=(int)(dmg/100);
                if(dmg2<0)
                    dmg2=0;
                GameController.Instance.characters[targets].currentHP-=dmg2;
            }
        }
    }
    public void CurseOfRa(int targets, int user)
    {
        for(int i=0;i<GameController.Instance.characters.Length;i++)
        {
            if(GameController.Instance.characters[i].characterData!=null && GameController.Instance.characters[i].alive)
            {
                float mana=GameController.Instance.characters[i].mana;
                GameController.Instance.characters[i].mana=(float)GameController.Instance.characters[i].currentHP;
                GameController.Instance.characters[i].currentHP=(int)GameController.Instance.characters[i].mana;
            }
        }
    }
    public void Bite(int targets, int user)
    {
        EnemyData caster= GameController.Instance.enemies[user];
        float dmg=caster.baseDmg;
        if(GameController.Instance.characters[targets].characterData!=null && GameController.Instance.characters[targets].alive)
        {

            dmg=dmg*(100-GameController.Instance.characters[targets].armor);
            int dmg2=(int)(dmg/100);
            if(dmg2<0)
                dmg2=0;
            GameController.Instance.characters[targets].currentHP-=dmg2;
        }
        if(targets!=0 && GameController.Instance.characters[targets-1].characterData!=null && GameController.Instance.characters[targets-1].alive)
        {
            dmg=dmg*(100-GameController.Instance.characters[targets-1].armor);
            int dmg2=(int)(dmg/100);
            if(dmg2<0)
                dmg2=0;
            GameController.Instance.characters[targets-1].currentHP-=dmg2;
        }
        if(targets!=3 && GameController.Instance.characters[targets+1].characterData!=null && GameController.Instance.characters[targets+1].alive)
        {
            dmg=dmg*(100-GameController.Instance.characters[targets+1].armor);
            int dmg2=(int)(dmg/100);
            if(dmg2<0)
                dmg2=0;
            GameController.Instance.characters[targets+1].currentHP-=dmg2;
        }
    }
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        names=new List<string>{"Attack","Heal","Heal Potion for 30 HP","Team Heal", "Attack","Attack All", "Curse of Ra", "Mana potion for 30 Mana","Bite"};
        skills.Add(Attack); //0
        skills.Add(Heal); //1
        skills.Add(HealPotion30); //2
        skills.Add(EnemyTeamHeal); //3
        skills.Add(EnemyAttack); //4
        skills.Add(EnemyAttackRanged); //5
        skills.Add(CurseOfRa); //6
        skills.Add(ManaPotion30); //7
        skills.Add(Bite); //8
    }
    public void use(int id, int targets, int user)
    {
        skills[id](targets,user);
    }
}
