using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public bool isMoving = false;
    public int prefabIndex;
    public Vector2[] guardAILocations;
    public EnemyData[] stats;
    public int currentLocation = 0;
    GridMovement movement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<GridMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            movement.StartMoving(guardAILocations[currentLocation]);
            isMoving = true;
            currentLocation++;
            if(currentLocation>=guardAILocations.Length)
            {
                currentLocation = 0;
            }
        }
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