using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public Image[] buttons;
    public GameObject[] buttonObject;
    public Image bigPicture;
    public TextMeshPro name;
    public TextMeshPro description;

    public void Select(int id)
    {
        Inventory inventory = Inventory.instance;
        if(id>=inventory.items.Count)
        {
            return;
        }

        name.text = inventory.items[id].item.name;
        bigPicture.sprite = Resources.Load<Sprite>("Sprites/" + inventory.items[id].item.name);
    }

    public void OpenMenu()
    {
        Inventory inventory = Inventory.instance;

        for(int i = 0; i<buttons.Length;i++)
        {
            if(i>=inventory.items.Count)
            {
                buttonObject[i].SetActive(false);
                continue;
            }
            buttonObject[i].SetActive(true);
            buttons[i].sprite = Resources.Load<Sprite>("Sprites/" + inventory.items[i].item.name);
        }
    }

    void Start()
    {
        Inventory.instance.inventoryMenu = gameObject;
        Inventory.instance.inventoryMenuScript = this;
        gameObject.SetActive(false);
    }
}
