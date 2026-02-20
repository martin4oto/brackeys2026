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

        Debug.Log(interaction);
        dialogueBoxObject.SetActive(true);
        mainText.text = interaction.mainText;
        titleText.text = interaction.title;

        currentInteraction = interaction;
    }

    void Update()
    {
        if(currentInteraction != null && Keyboard.current.anyKey.isPressed)
        {
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
    }
}
