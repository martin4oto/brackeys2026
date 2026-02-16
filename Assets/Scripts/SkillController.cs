using UnityEngine;
using System.Collections.Generic;

public class SkillController : MonoBehaviour
{
    public static SkillController Instance {
        get;
        set;
    }
    public delegate void SkillAction(GameObject[] targets, CharacterData user);
    public List<SkillAction> skills = new List<SkillAction>();
    
    public void Attack(GameObject[] targets, CharacterData user) { }
    
    public void Slash(GameObject[] targets, CharacterData user) { }
    
    void Awake()
    {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
        skills.Add(Attack);
        skills.Add(Slash);
    }
    public void use(int id, GameObject[] targets, CharacterData user)
    {
        skills[id](targets,user);
    }
}
