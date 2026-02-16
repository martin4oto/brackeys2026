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
    public EnemyStats[] enemies;
    public float locationID;
    public void EnterCombat() {

        //TODO add pre combat saving + logic
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
    }

}
