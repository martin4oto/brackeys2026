using UnityEngine;

public enum ItemTypes
{
    Gear,
    Usable,
    Skill
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public ItemTypes type;
    public string itemName;
    public string itemSprite;
    public Skill[] skillsOfItem;
}
