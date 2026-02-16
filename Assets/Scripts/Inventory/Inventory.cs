using System.Collections.Generic;
using UnityEngine;

public class InventoryStack
{
    int amount;
    Item item;
}

public class Inventory : MonoBehaviour
{
    List<InventoryStack> items;
    Dictionary<string, int> itemIndexes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
