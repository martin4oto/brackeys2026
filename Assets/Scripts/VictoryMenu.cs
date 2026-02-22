using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class VictoryMenu : MonoBehaviour
{
    string path = Application.dataPath + "/Saves/";

    public void Back()
    {
        string filePath = path +"save0";

        string json = File.ReadAllText(filePath);

        MapManager.instance.SaveGame(json, 1);

        GameController.Instance.gameWon = GameController.Instance.combatWinExpectation;
        GameController.Instance.FixInventory();
        SceneManager.LoadScene("Level 1");
    }
}
