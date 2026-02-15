using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    
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
        SceneManager.LoadScene("SampleScene");
    }
    public CharacterData[] characters = new CharacterData[4];
    private EnemyStats[] enemies;
    public float locationID;
    public void EnterCombat() {


        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
    }

}
