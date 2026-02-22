using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    string path = "Sprites/map/";
    Item currentItem;

    public Image[] buttons;
    public GameObject[] buttonObject;
    public Image bigPicture;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI description;
    public GameObject infoScreen;
    int begining = 0;

    
    public GameObject helmetObject;
    public GameObject chestPlateObject;
    public GameObject pantsObject;
    public GameObject bootsObject;
    public GameObject weaponObject;
    
    public GameObject equipButton;
    int currentCharacter = 0;
    public TextMeshProUGUI characterName;
    public bool ItemUseMenu;

    public void Select(int id)
    {
        Inventory inventory = Inventory.instance;
        if(id>=inventory.items.Count)
        {
            return;
        }
        infoScreen.SetActive(true);

        currentItem = inventory.items[id].item;

        if(ItemUseMenu && currentItem.type == ItemTypes.Usable)
        {
            equipButton.SetActive(true);
        }
        else if(!ItemUseMenu && currentItem.type == ItemTypes.Gear)
        {
            equipButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(false);
        }
        
        Name.text = currentItem.itemName;
        description.text = currentItem.itemDescription;
        bigPicture.sprite = Resources.Load<Sprite>(path + currentItem.itemSprite);
    }

    public void OpenMenu()
    {
        characterName.text = Inventory.instance.characters[currentCharacter].stats.characterData.name;
        equipButton.SetActive(false);
        infoScreen.SetActive(false);
        begining = 0;
        VisualizeItems();
    }

    void VisualizeItems()
    {
        Inventory inventory = Inventory.instance;

        for(int i = 0; i<buttons.Length;i++)
        {
            if(i + begining>=inventory.items.Count)
            {
                buttonObject[i].SetActive(false);
                continue;
            }

            buttonObject[i].SetActive(true);
            buttons[i + begining].sprite = Resources.Load<Sprite>(path + inventory.items[i + begining].item.itemSprite);
        }
        if(!ItemUseMenu)
        {
            VisualzieGear();
        }
    }

    void VisualzieGear()
    {
        Image helmet = helmetObject.GetComponent<Image>();
        Image chestplate = chestPlateObject.GetComponent<Image>();
        Image pants = pantsObject.GetComponent<Image>();
        Image boots = bootsObject.GetComponent<Image>();
        Image weapon = weaponObject.GetComponent<Image>();

        CharacterGear character = Inventory.instance.characters[currentCharacter];

        PutGear(helmet, helmetObject, character.helmet);
        PutGear(chestplate, chestPlateObject, character.chestplate);
        PutGear(pants, pantsObject, character.pants);
        PutGear(boots, bootsObject, character.boots);
        PutGear(weapon, weaponObject, character.weapon);
    }

    void PutGear(Image image, GameObject gearObject, Item item)
    {
        Inventory inventory = Inventory.instance;
        if(item == null)
        {
            gearObject.SetActive(false);
        }
        else
        {
            gearObject.SetActive(true);
            image.sprite = Resources.Load<Sprite>(path + item.itemSprite);
        }
    }

    public void ChooseItem()
    {
        if(currentItem.type == ItemTypes.Usable)
        {
            BattleManager.Instance.itemSkillId=currentItem.skillID;
            if(currentItem.friendly)
                BattleManager.Instance.friendlyTargetStage=true;
            else
                BattleManager.Instance.enemySelection=true;
            //add option to cancel.
            Inventory.instance.RemoveItem(currentItem);
        
            equipButton.SetActive(false);
            infoScreen.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        Inventory.instance.inventoryMenu = gameObject;
        Inventory.instance.inventoryMenuScript = this;
        if(!ItemUseMenu)gameObject.SetActive(false);
    }

    public void NextPage()
    {
        if(begining + buttons.Length >= Inventory.instance.items.Count)
        {
            return;
        }

        begining += buttons.Length;
    }

    public void PreviousPage()
    {
        if(begining - buttons.Length < 0)
        {
            return;
        }

        begining -= buttons.Length;
    }

    public void EquipGearPiece()
    {
        Inventory.instance.EquipItem(currentItem,currentCharacter);
        VisualizeItems();
        
        equipButton.SetActive(false);
        infoScreen.SetActive(false);
    }
    
    public void UnEquipGearPiece(int type)
    {
        Inventory.instance.UnEquipItem((GearTypes)type, currentCharacter);
        VisualizeItems();
        
        equipButton.SetActive(false);
        infoScreen.SetActive(false);
    }

    public void NextCharacter()
    {
        currentCharacter++;
        if(currentCharacter>=Inventory.instance.characters.Count)
        {
            currentCharacter = 0;
        }
        VisualzieGear();
        characterName.text = Inventory.instance.characters[currentCharacter].stats.characterData.name;
    }

    public void PreviousCharacter()
    {
        currentCharacter--;
        if(currentCharacter<0)
        {
            currentCharacter = Inventory.instance.characters.Count-1;
        }
        VisualzieGear();
        characterName.text = Inventory.instance.characters[currentCharacter].stats.characterData.name;
    }
}
