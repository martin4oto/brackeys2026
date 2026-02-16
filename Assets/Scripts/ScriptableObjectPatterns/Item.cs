using UnityEngine;

enum ItemTypes
{
    Gear,
    Usable,
    Skill
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    ItemTypes type;
    public string itemName;
    public Skill[] skillsOfItem;
}
