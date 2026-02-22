using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public bool finalEnemy;
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
        if(!isMoving && guardAILocations!= null && guardAILocations.Length != 0)
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
        GameController.Instance.combatWinExpectation = finalEnemy;

        MapManager.instance.RemoveEnemy(this);
        MapManager.instance.SaveGame(0);

        SceneManager.LoadScene("BattleScene");
    }
}