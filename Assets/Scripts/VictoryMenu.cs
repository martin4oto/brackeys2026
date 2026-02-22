using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void Back()
    {
        GameController.Instance.FixInventory();
        SceneManager.LoadScene("Level 1");
    }
}
