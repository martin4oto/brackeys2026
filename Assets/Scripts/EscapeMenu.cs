using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menu;

    // Update is called once per frame
    bool buttonHeld = false;
    bool isVisible = false;


    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed && !buttonHeld)
        {
            isVisible = !isVisible;
            menu.SetActive(isVisible);
            buttonHeld = true;
        }
        else if(!Keyboard.current.escapeKey.isPressed)
        {
            buttonHeld = false;
        }
    }

    public void SaveGame()
    {
        MapManager.instance.SaveGame(1);
    }
}
