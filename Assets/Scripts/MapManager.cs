using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Save
{
    public int[] enemiesAliveIndexes;
    public Vector2[] enemiesAlivePositions;

    public Save(int enemyCount)
    {
        enemiesAliveIndexes = new int[enemyCount];
        enemiesAlivePositions = new Vector2[enemyCount];
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
        LoadFile(0);

        DontDestroyOnLoad(transform);
        instance = this;
    }

    void Start()
    {
        SaveGame(0);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void PutEnemy(Vector2 position, int index)
    {
        GameObject enemy = GameObject.Instantiate(enemyVariants[index], position, Quaternion.identity);

        enemiesAlive.Add(enemy.GetComponent<Enemy>());
        Debug.Log(enemiesAlive[0]);
    }

    void LoadEnemiesFromSave(Save save)
    {
        for(int i = 0; i<save.enemiesAliveIndexes.Length; i++)
        {
            PutEnemy(save.enemiesAlivePositions[i],save.enemiesAliveIndexes[i]);
        }
    }
    void PutEnemiesIntoSave(Save save)
    {
        for(int i = 0; i<enemiesAlive.Count; i++)
        {
            Debug.Log(enemiesAlive[i]);
            save.enemiesAliveIndexes[i] = enemiesAlive[i].prefabIndex;
            save.enemiesAlivePositions[i] = enemiesAlive[i].transform.position;
        }
    }

    void SaveGame(int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        if(!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        Save currentState = new Save(enemiesAlive.Count);

        PutEnemiesIntoSave(currentState);

        string json = JsonUtility.ToJson(currentState);
        File.WriteAllText(filePath, json);
    }

    void LoadFile(int fileIndex)
    {
        string filePath = path +"save"+ fileIndex;

        string json = File.ReadAllText(filePath);

        Save sv = JsonUtility.FromJson<Save>(json);
        LoadEnemiesFromSave(sv);
    }
}