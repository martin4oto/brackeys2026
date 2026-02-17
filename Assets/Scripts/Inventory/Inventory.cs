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
    Dictionary<string, int> itemIndexes;
    public GameObject inventoryMenu;
    public InventoryMenu inventoryMenuScript;
    string path = Application.dataPath + "/Saves/";

    public void RemoveItem(Item item)
    {
        if(itemIndexes.ContainsKey(item.name))
        {
            int index = itemIndexes[item.name];

            items[index].amount--;

            if(items[index].amount<=0)
            {
                itemIndexes.Remove(item.name);
                items.RemoveAt(index);
            }
        }
    }

    public void AddItem(Item item)
    {
        if(itemIndexes.ContainsKey(item.name))
        {
            int index = itemIndexes[item.name];

            items[index].amount++;
        }
        else
        {
            InventoryStack newstack = new InventoryStack();
            newstack.item  = item;

            itemIndexes.Add(item.name, items.Count);
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
        for(int i = 0; i<items.Count; i++)
        {
            itemIndexes.Add(items[i].item.name,i);
        }
    }
}
