using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;
using System;

[Serializable]
public class positionList
{
    [SerializeField]
    public Vector2[] positions;
}
public class Save
{
    public int[] enemiesAliveIndexes;
    public Vector2[] enemiesAlivePositions;
    [SerializeField]
    public positionList[] enemyGuardAILocations;

    public int[] itemAmount;
    public int[] itemID;
    
    public CharacterGear[] characters;
    public int[] characterDataIDs;

    public int[] interactablesIndexes;
    public Vector2[] interactablesPositions;
    public bool[] interactableUsed;

    public Vector3 playerPosition;

    public Save(int enemyCount, int interactableCount, int itemCount)
    {
        enemiesAliveIndexes = new int[enemyCount];
        enemiesAlivePositions = new Vector2[enemyCount];
        enemyGuardAILocations = new positionList[enemyCount];
        
        interactablesIndexes = new int[interactableCount];
        interactablesPositions = new Vector2[interactableCount];
        interactableUsed = new bool[interactableCount];

        itemID = new int[itemCount];
        itemAmount = new int[itemCount];
    }
}

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject[] enemyVariants;
    public GameObject[] interactableVariants;
    public List<Enemy> enemiesAlive;

    int interactableCount = 0;
    interactable[][] interactablesMap;
    public List<interactable> allInteractables;

    public int width;
    public int height;
    int screenMiddleHeight;
    int screenMiddleWidth;

    string path = Application.dataPath + "/Saves/";
    string itemsPath = "/Items/";

    public float squareSize;
    public TextAsset[] saves;

    public Item[] allItems;
    public CharacterData[] allData;

    GameObject player;
    GameObject mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance != null)
        {
            instance.SetUp();
            instance.LoadFile(1);
            GameObject.Destroy(gameObject);
            return;
        }
        instance = this;

        enemiesAlive = new List<Enemy>();
        allInteractables = new List<interactable>();

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        interactablesMap = new interactable[width][];
        for(int i = 0; i<width; i++)
        {
            interactablesMap[i] = new interactable[height];
        }

        DontDestroyOnLoad(transform);
        screenMiddleHeight = height/2;
        screenMiddleWidth = width/2;
        SetUp();
        instance.LoadFile(1);
    }

    void SetUp()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void RemoveInteractable(interactable interactable)
    {
        Vector2 position = interactable.transform.position;

        int x =(int)position.x + screenMiddleWidth;
        int y =(int)position.y + screenMiddleHeight;
        
        interactablesMap[x][y] = null;
        allInteractables.Remove(interactable);
    }

    public void AddInteractable(Vector2 position, int index, bool intereacted)
    {
        GameObject interactable = GameObject.Instantiate(interactableVariants[index], position, Quaternion.identity);

        interactable interactableScript = interactable.GetComponent<interactable>();
        
        int x =(int)position.x + screenMiddleWidth;
        int y =(int)position.y + screenMiddleHeight;

        interactablesMap[x][y] = interactableScript;

        interactableScript.used = intereacted;
        allInteractables.Add(interactableScript);
    }

    public void PutEnemy(Vector2 position, int index, Vector2[] locations)
    {
        GameObject enemy = GameObject.Instantiate(enemyVariants[index], position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.guardAILocations = locations;
        enemiesAlive.Add(enemyScript);
    }

    void LoadEnemiesFromSave(Save save)
    {
        for(int i = 0; i<save.enemiesAliveIndexes.Length; i++)
        {
            PutEnemy(save.enemiesAlivePositions[i],save.enemiesAliveIndexes[i], save.enemyGuardAILocations[i].positions);
        }
    }
    void PutEnemiesIntoSave(Save save)
    {
        for(int i = 0; i<enemiesAlive.Count; i++)
        {
            save.enemiesAliveIndexes[i] = enemiesAlive[i].prefabIndex;
            save.enemiesAlivePositions[i] = enemiesAlive[i].transform.position;

            save.enemyGuardAILocations[i] = new positionList();

            save.enemyGuardAILocations[i].positions = enemiesAlive[i].guardAILocations;
        }
    }

    void LoadInteractablesFromSave(Save save)
    {
        for(int i = 0; i<save.interactablesPositions.Length; i++)
        {
            AddInteractable(save.interactablesPositions[i], save.interactablesIndexes[i], save.interactableUsed[i]);
        }
    }
    void PutInteractablesIntoSave(Save save)
    {
        for(int i = 0; i<allInteractables.Count; i++)
        {
            save.interactablesPositions[i] = allInteractables[i].transform.position;
            save.interactableUsed[i] = allInteractables[i].used;
            save.interactablesIndexes[i] = allInteractables[i].prefabIndex;
        }
    }
    
    void LoadItemFromSave(Save save)
    {
        for(int i = 0; i<save.itemAmount.Length; i++)
        {
            for(int j = 0; j<save.itemAmount[i]; j++)
            {
                Inventory.instance.AddItem(allItems[save.itemID[i]]);
            }
        }
    }

    void PutItemIntoSave(Save save)
    {
        Inventory inventory = Inventory.instance;

        for(int i = 0; i<inventory.items.Count; i++)
        {
            save.itemAmount[i] = inventory.items[i].amount;
            save.itemID[i] = inventory.items[i].item.itemID;
        }
    }
    
    void LoadCharactersFromSave(Save save)
    {
        Inventory inventory = Inventory.instance;

        for(int i = 0; i<save.characterDataIDs.Length; i++)
        {
            inventory.characters[i].stats.characterData = allData[save.characterDataIDs[i]];
        }
    }

    void PutCharactersIntoSave(Save save)
    {
        save.characterDataIDs = new int[save.characters.Length];

        for(int i = 0; i<save.characterDataIDs.Length; i++)
        {
            save.characterDataIDs[i] = save.characters[i].stats.characterData.id;
        }
    }

    public void TryToInteract(Vector2 objectLocation)
    {
        int x =(int)objectLocation.x + screenMiddleHeight;
        int y =(int)objectLocation.y + screenMiddleWidth;

        if(interactablesMap[x][y] != null)
        {
            interactablesMap[x][y].StartInteraction();
        }
    }

    public void SaveGame(int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        if(!File.Exists(filePath))
        {
            using(File.Create(filePath));
        }
        Save currentState = new Save(enemiesAlive.Count, allInteractables.Count, Inventory.instance.items.Count);

        PutEnemiesIntoSave(currentState);
        PutInteractablesIntoSave(currentState);
        PutItemIntoSave(currentState);

        Inventory inventory = Inventory.instance;

        currentState.characters = inventory.characters.ToArray();
        PutCharactersIntoSave(currentState);

        currentState.playerPosition = player.transform.position;

        string json = JsonUtility.ToJson(currentState);
        File.WriteAllText(filePath, json);
    }

    public void SaveGame(string data, int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        File.WriteAllText(filePath, data);
    }

    public void LoadFile(int fileIndex)
    {
        if(player == null)return;
        string filePath = path +"save"+ fileIndex;

        string json = File.ReadAllText(filePath);

        Save sv = JsonUtility.FromJson<Save>(json);
        LoadEnemiesFromSave(sv);
        LoadInteractablesFromSave(sv);
        LoadItemFromSave(sv);
        player.transform.position = sv.playerPosition;
        mainCamera.transform.position = sv.playerPosition + Vector3.back*10;


        Inventory inventory = Inventory.instance;
        
        if(sv.characters != null)
        {
            inventory.characters = new List<CharacterGear>(sv.characters);
            LoadCharactersFromSave(sv);
        }
        else
        {
            inventory.characters = new List<CharacterGear>();
            inventory.characters.Add(new CharacterGear());
        }
        
        inventory.IndexItems();
    }
}