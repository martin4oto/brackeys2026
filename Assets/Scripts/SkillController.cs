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
    
    public void Attack(int targets, int user) { }
    
    public void Slash(int targets, int user) { }
    
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        skills.Add(Attack);
        skills.Add(Slash);
    }
    public void use(int id, int targets, int user)
    {
        skills[id](targets,user);
    }
}
