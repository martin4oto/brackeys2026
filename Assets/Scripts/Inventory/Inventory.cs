using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.IO;
using System;

[Serializable]
public class InventoryStack
{
    public int amount;
    [SerializeField]
    public Item item;
}

[Serializable]
public class CharacterGear
{
    public Item helmet;
    public Item chestplate;
    public Item pants;
    public Item boots;
    public Item weapon;
    public Data stats;
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    bool isActive = false;
    void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform);
        instance = this;
        
        items = new List<InventoryStack>();
        itemIndexes = new Dictionary<string, int>();
    }

    [SerializeField]
    public List<InventoryStack> items;
    [SerializeField]
    public List<CharacterGear> characters;

    Dictionary<string, int> itemIndexes;
    public GameObject inventoryMenu;
    public InventoryMenu inventoryMenuScript;
    string path = Application.dataPath + "/Saves/";

    public void RemoveItem(Item item)
    {
        if(itemIndexes.ContainsKey(item.itemName))
        {
            int index = itemIndexes[item.itemName];

            items[index].amount--;

            if(items[index].amount<=0)
            {
                itemIndexes.Remove(item.itemName);
                items.RemoveAt(index);
                IndexItems();
            }
        }
    }

    public void AddCharacter(Data characer)
    {
        CharacterGear cg = new CharacterGear();

        cg.stats = characer;

        characters.Add(cg);
    }

    public void AddItem(Item item)
    {
        if(itemIndexes.ContainsKey(item.itemName))
        {
            int index = itemIndexes[item.itemName];

            items[index].amount++;
        }
        else
        {
            InventoryStack newstack = new InventoryStack();
            newstack.item  = item;
            newstack.amount = 1;

            itemIndexes.Add(item.itemName, items.Count);
            items.Add(newstack);
        }
    }

    void MenuPop()
    {
        if(isActive)
        {
            inventoryMenu.SetActive(true);
            inventoryMenuScript.OpenMenu();
        }
        else
        {
            inventoryMenu.SetActive(false);
        }
    }

    bool buttonHeld = false;
    public bool inventoryEnabled = true;

    void Update()
    {
        if(!inventoryEnabled)return;
        if(Keyboard.current.iKey.isPressed && !buttonHeld)
        {
            buttonHeld = true;
            isActive = !isActive;
            MenuPop();
        }
        else if(!Keyboard.current.iKey.isPressed)
        {
            buttonHeld = false;
        }
    }

    public void IndexItems()
    {
        itemIndexes = new Dictionary<string, int>();
        if(items == null)
        {
            items = new List<InventoryStack>();
        }

        for(int i = 0; i<items.Count; i++)
        {
            if(itemIndexes.ContainsKey(items[i].item.itemName))
            {
                itemIndexes[items[i].item.itemName] = i;
                continue;
            }

            itemIndexes.Add(items[i].item.itemName,i);
        }
    }

    void RemoveHelmet(int character)
    {
        if(characters[character].helmet != null)
        {
            AddItem(characters[character].helmet);
            characters[character].helmet = null;
        }
    }
    void RemoveChestPlate(int character)
    {
        if(characters[character].chestplate != null)
        {
            AddItem(characters[character].chestplate);
            characters[character].chestplate = null;
        }
    }
    void RemovePants(int character)
    {
        if(characters[character].pants != null)
        {
            AddItem(characters[character].pants);
            characters[character].pants = null;
        }
    }
    void RemoveBoots(int character)
    {
        if(characters[character].boots != null)
        {
            AddItem(characters[character].boots);
            characters[character].boots = null;
        }
    }
    void RemoveWeapon(int character)
    {
        if(characters[character].weapon != null)
        {
            AddItem(characters[character].weapon);
            characters[character].weapon = null;
        }
    }

    public void EquipItem(Item item, int character)
    {
        if(item == null)return;
        
        switch(item.gearType)
        {
            case GearTypes.Helmet:
                RemoveHelmet(character);
                characters[character].helmet = item;
                break;
            case GearTypes.Chestplate:
                RemoveChestPlate(character);
                characters[character].chestplate = item;
                break;
            case GearTypes.Pants:
                RemovePants(character);
                characters[character].pants = item;
                break;
            case GearTypes.Boots:
                RemoveBoots(character);
                characters[character].boots = item;
                break;
            case GearTypes.Weapon:
                RemoveWeapon(character);
                characters[character].weapon = item;
                break;
        }
        RemoveItem(item);
    }

    public void UnEquipItem(GearTypes itemtype, int character)
    {
        switch(itemtype)
        {
            case GearTypes.Helmet:
                RemoveHelmet(character);
                break;
            case GearTypes.Chestplate:
                RemoveChestPlate(character);
                break;
            case GearTypes.Pants:
                RemovePants(character);
                break;
            case GearTypes.Boots:
                RemoveBoots(character);
                break;
            case GearTypes.Weapon:
                RemoveWeapon(character);
                break;
        }
    }

    public Data[] GetCompleteData()
    {
        Data[] newData = new Data[4];

        for(int i = 0; i<characters.Count; i++)
        {
            Data data = newData[i] = characters[i].stats;

            newData[i] = data.Copy();
            newData[i].alive = true;
            newData[i].mana = data.characterData.maxMana;
            newData[i].currentHP=data.characterData.maxHP;
        }

        return newData;
    }
}
