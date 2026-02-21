using UnityEngine;
using UnityEngine.UI;

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
        
    }
    void LoadGame()
    {
        
    }
}
