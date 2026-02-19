using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;

public class Save
{
    public int[] enemiesAliveIndexes;
    public Vector2[] enemiesAlivePositions;
    public Vector2[][] enemyGuardAILocations;
    public InventoryStack[] playerInventory;
    public CharacterGear[] characters;
    public int[] interactablesIndexes;
    public Vector2[] interactablesPositions;
    public bool[] interactableUsed;

    public Save(int enemyCount, int interactableCount)
    {
        enemiesAliveIndexes = new int[enemyCount];
        enemiesAlivePositions = new Vector2[enemyCount];
        enemyGuardAILocations = new Vector2[enemyCount][];
        
        interactablesIndexes = new int[interactableCount];
        interactablesPositions = new Vector2[interactableCount];
        interactableUsed = new bool[interactableCount];
    }
}

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    GameObject[] enemyVariants;
    GameObject[] interactableVariants;

    List<Enemy> enemiesAlive;

    int interactableCount = 0;
    interactable[][] interactablesMap;
    public List<interactable> allInteractables;

    public int width;
    public int height;
    int screenMiddleHeight;
    int screenMiddleWidth;

    string path = Application.dataPath + "/Saves/";

    public float squareSize;
    public TextAsset[] saves;

    void LoadMapPrefabs()
    {
        enemyVariants = Resources.LoadAll<GameObject>("Prefabs/Enemies");
        interactableVariants = Resources.LoadAll<GameObject>("Prefabs/Interactables");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance != null)
        {
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

        LoadMapPrefabs();

        interactablesMap = new interactable[width][];
        for(int i = 0; i<width; i++)
        {
            interactablesMap[i] = new interactable[height];
        }

        DontDestroyOnLoad(transform);
        LoadFile(1);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void AddInteractable(Vector2 position, int index, bool intereacted)
    {
        GameObject interactable = GameObject.Instantiate(interactableVariants[index], position, Quaternion.identity);

        interactable interactableScript = interactable.GetComponent<interactable>();
        
        int x =(int)position.x;
        int y =(int)position.y;

        interactablesMap[x][y] = interactableScript;

        interactableCount++;
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
            PutEnemy(save.enemiesAlivePositions[i],save.enemiesAliveIndexes[i], save.enemyGuardAILocations[i]);
        }
    }
    void PutEnemiesIntoSave(Save save)
    {
        for(int i = 0; i<enemiesAlive.Count; i++)
        {
            save.enemiesAliveIndexes[i] = enemiesAlive[i].prefabIndex;
            save.enemiesAlivePositions[i] = enemiesAlive[i].transform.position;
            save.enemyGuardAILocations[i] = enemiesAlive[i].guardAILocations;
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

    public void TryToInteract(Vector2 objectLocation)
    {
        int x =(int)objectLocation.x;
        int y =(int)objectLocation.y;

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
        Save currentState = new Save(enemiesAlive.Count, allInteractables.Count);

        PutEnemiesIntoSave(currentState);
        PutInteractablesIntoSave(currentState);

        Inventory inventory = Inventory.instance;
        currentState.playerInventory = inventory.items.ToArray();

        currentState.characters = inventory.characters.ToArray();

        string json = JsonUtility.ToJson(currentState);
        File.WriteAllText(filePath, json);
    }

    public void LoadFile(int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        string json = File.ReadAllText(filePath);

        Save sv = JsonUtility.FromJson<Save>(json);
        LoadEnemiesFromSave(sv);
        LoadInteractablesFromSave(sv);


        Inventory inventory = Inventory.instance;

        if(sv.playerInventory!=null)
        {
            inventory.items = new List<InventoryStack>(sv.playerInventory);
        }
        
        if(sv.characters != null)
        {
            inventory.characters = new List<CharacterGear>(sv.characters);
        }
        else
        {
            inventory.characters = new List<CharacterGear>();
            inventory.characters.Add(new CharacterGear());
        }
        
        inventory.IndexItems();
    }
}