using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    bool isMoving = false;
    public int prefabIndex;
    public Vector2[] guardAILocations;
    int currentLocation;
    public EnemyData[] stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //put the correct sprite
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            StartCombat();
        }
    }

    void StartCombat()
    {
        Inventory.instance.inventoryEnabled = false;
        GameController.Instance.characters = Inventory.instance.GetCompleteData();
        GameController.Instance.enemies= stats;
        SceneManager.LoadScene("BattleScene");
    }
}