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
    
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        skills.Add(Attack);
        skills.Add(Heal);
    }
    public void use(int id, int targets, int user)
    {
        skills[id](targets,user);
    }
}
