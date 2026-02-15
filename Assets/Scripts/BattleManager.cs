using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class BattleManager : MonoBehaviour
{
    public GameObject panel;
    public Sprite background;
    public GameObject party;
    private GameObject[] characters = new GameObject[4];
    public GameObject enemyParty;
    void Awake()
    {
        string file = "Sprites/battleBackground" + GameController.Instance.locationID;
        background= Resources.Load<Sprite>(file);
        panel.GetComponent<Image>().sprite=background;
        //TODO add music file

        if (GameController.Instance.characters[0] != null)
        {
            party.transform.Find("Character1").GetComponent<SpriteRenderer>().sprite=GameController.Instance.characters[0].combatSprite;
        }
        if (GameController.Instance.characters[1] != null)
        {
            party.transform.Find("Character2").GetComponent<SpriteRenderer>().sprite=GameController.Instance.characters[0].combatSprite;
        }
    }
}
