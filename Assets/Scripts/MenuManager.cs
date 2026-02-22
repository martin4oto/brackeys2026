using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button loadGame;
    public Button tutorialButton;
    public Button backButton;
    public GameObject tutorialPanel;
    void Awake()
    {
        startButton.onClick.AddListener(() => StartNewGame());
        loadGame.onClick.AddListener(() => LoadGame());
        tutorialButton.onClick.AddListener(() => OpenTutorial());
        backButton.onClick.AddListener(() => CloseTutorial());
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
    void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }
    void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
