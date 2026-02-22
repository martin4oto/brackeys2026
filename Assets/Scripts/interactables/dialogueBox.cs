using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class dialogueBox : MonoBehaviour
{
    public GameObject dialogueBoxObject;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI titleText;

    Interaction currentInteraction;
    PartyManager partyManager;

    void Start()
    {
        partyManager = GameObject.Find("Player").GetComponent<PartyManager>();
    }

    public void StartDialogue(Interaction interaction)
    {
        if(currentInteraction != null)
        {
            return;
        }

        StartNext(interaction);
    }

    public void PrintLine(string line)
    {
        mainText.text += line;
    }

    public bool held = false;

    void StartNext(Interaction interaction)
    {
        dialogueBoxObject.SetActive(true);

        mainText.text = "";
        for(int i = 0; i<interaction.mainText.Length; i++)
        {
            mainText.text += interaction.mainText[i] +"\n";
        }
        titleText.text = interaction.title;

        currentInteraction = interaction;
    }

    void Update()
    {
        if(currentInteraction != null && Keyboard.current.anyKey.isPressed && !Keyboard.current.eKey.isPressed && !held)
        {
            held = true;

            if(currentInteraction.characterReward.characterData != null)
            {
                Debug.Log("aaaa");
                Inventory.instance.AddCharacter(currentInteraction.characterReward);
                partyManager.SpawnParty();
            }

            if(currentInteraction.nextInteraction !=null)
            {
                StartNext(currentInteraction.nextInteraction);
            }
            else
            {
                Time.timeScale = 1;
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
