using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }

        DontDestroyOnLoad(transform);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
