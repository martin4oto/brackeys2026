using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public bool toBattle;
    public static GameController Instance {
        get;
        set;
    }

    void Awake () {
        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
    }

    void Start() {
        if(toBattle)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
    [SerializeField]
    public Data[] characters = new Data[4];
    public EnemyData[] enemies = new EnemyData[4];
    public float locationID;
    public void EnterCombat() {

        //TODO add pre combat saving + logic
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
    }

}
