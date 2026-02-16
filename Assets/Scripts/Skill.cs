using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    public string name;
    public float manaCost;
    public Sprite logo;
    public int id;
}
