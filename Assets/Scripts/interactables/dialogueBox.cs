using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class dialogueBox : MonoBehaviour
{
    public GameObject dialogueBoxObject;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI titleText;

    Interaction currentInteraction;

    public void StartDialogue(Interaction interaction)
    {
        if(currentInteraction != null)
        {
            return;
        }

        dialogueBoxObject.SetActive(true);

        mainText.text = "";
        for(int i = 0; i<interaction.mainText.Length; i++)
        {
            mainText.text += interaction.mainText[i] +"\n";
        }
        titleText.text = interaction.title;

        currentInteraction = interaction;
    }

    public void PrintLine(string line)
    {
        mainText.text += line;
    }

    bool held = false;

    void Update()
    {
        if(currentInteraction != null && Keyboard.current.anyKey.isPressed && !Keyboard.current.eKey.isPressed && !held)
        {
            held = true;
            if(currentInteraction.nextInteraction !=null)
            {
                StartDialogue(currentInteraction.nextInteraction);
            }
            else
            {
                currentInteraction = null;
                dialogueBoxObject.SetActive(false);
            }
        }

        if(!Keyboard.current.anyKey.isPressed)
        {
            held = false;
        }
    }
}
