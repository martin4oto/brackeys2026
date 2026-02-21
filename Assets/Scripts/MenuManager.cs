using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button loadGame;
    void Awake()
    {
        startButton.onClick.AddListener(() => StartNewGame());
        loadGame.onClick.AddListener(() => LoadGame());
    }
    void StartNewGame()
    {
        TextAsset mytxtData=(TextAsset)Resources.Load("save0");
        string txt=mytxtData.text;

        MapManager.instance.SaveGame(txt, 1);

        LoadGame();
    }
    void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
