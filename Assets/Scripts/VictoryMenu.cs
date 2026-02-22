using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void Back()
    {
        GameController.Instance.gameWon = GameController.Instance.combatWinExpectation;
        GameController.Instance.FixInventory();
        SceneManager.LoadScene("Level 1");
    }
}
