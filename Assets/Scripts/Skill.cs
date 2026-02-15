using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    public string name;
    public float manaCost;
    public Sprite logo;
    public Object script;
}
