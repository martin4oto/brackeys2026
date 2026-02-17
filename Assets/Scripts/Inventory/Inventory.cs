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
    public Item helmet;
    public Item chestplate;
    public Item pants;
    public Item boots;
    public Item weapon;

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
            }
        }
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

    void Update()
    {
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
            itemIndexes.Add(items[i].item.itemName,i);
        }
    }

    void RemoveHelmet()
    {
        if(helmet != null)
        {
            AddItem(helmet);
            helmet = null;
        }
    }
    void RemoveChestPlate()
    {
        if(chestplate != null)
        {
            AddItem(chestplate);
            chestplate = null;
        }
    }
    void RemovePants()
    {
        if(pants != null)
        {
            AddItem(pants);
            pants = null;
        }
    }
    void RemoveBoots()
    {
        if(boots != null)
        {
            AddItem(boots);
            boots = null;
        }
    }
    void RemoveWeapon()
    {
        if(weapon != null)
        {
            AddItem(weapon);
            weapon = null;
        }
    }

    public void EquipItem(Item item)
    {
        if(item == null)return;
        
        switch(item.gearType)
        {
            case GearTypes.Helmet:
                RemoveHelmet();
                helmet = item;
                break;
            case GearTypes.Chestplate:
                RemoveChestPlate();
                chestplate = item;
                break;
            case GearTypes.Pants:
                RemovePants();
                pants = item;
                break;
            case GearTypes.Boots:
                RemoveBoots();
                boots = item;
                break;
            case GearTypes.Weapon:
                RemoveWeapon();
                weapon = item;
                break;
        }
        RemoveItem(item);
    }

    public void UnEquipItem(GearTypes itemtype)
    {
        switch(itemtype)
        {
            case GearTypes.Helmet:
                RemoveHelmet();
                break;
            case GearTypes.Chestplate:
                RemoveChestPlate();
                break;
            case GearTypes.Pants:
                RemovePants();
                break;
            case GearTypes.Boots:
                RemoveBoots();
                break;
            case GearTypes.Weapon:
                RemoveWeapon();
                break;
        }
    }
}
