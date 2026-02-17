using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Save
{
    public int[] enemiesAliveIndexes;
    public Vector2[] enemiesAlivePositions;
    public Vector2[][] enemyGuardAILocations;
    public InventoryStack[] playerInventory;
    public CharacterGear[] characters;

    public Save(int enemyCount)
    {
        enemiesAliveIndexes = new int[enemyCount];
        enemiesAlivePositions = new Vector2[enemyCount];
        enemyGuardAILocations = new Vector2[enemyCount][];
    }
}

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    GameObject[] enemyVariants;

    List<Enemy> enemiesAlive;
    string path = Application.dataPath + "/Saves/";

    public float squareSize;
    public TextAsset[] saves;

    void LoadMapPrefabs()
    {
        enemyVariants = Resources.LoadAll<GameObject>("Prefabs/Enemies");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }

        enemiesAlive = new List<Enemy>();

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        LoadMapPrefabs();

        DontDestroyOnLoad(transform);
        instance = this;
    }

    void Start()
    {
        LoadFile(1);
    }

    // Update is called once per frame
    void Update()
    {
        

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

    public void SaveGame(int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        if(!File.Exists(filePath))
        {
            using(File.Create(filePath));
        }
        Save currentState = new Save(enemiesAlive.Count);

        PutEnemiesIntoSave(currentState);

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