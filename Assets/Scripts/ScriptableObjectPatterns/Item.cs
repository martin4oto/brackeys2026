using UnityEngine;

public enum GearTypes
{
    None,
    Helmet,
    Chestplate,
    Pants,
    Boots,
    Weapon
}

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
    public string itemDescription;
    public Skill[] skillsOfItem;
    public GearTypes gearType;
}
