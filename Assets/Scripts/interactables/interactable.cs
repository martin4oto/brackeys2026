using UnityEngine;
using UnityEngine.InputSystem;

public class interactable : MonoBehaviour
{
    public Interaction firstInteraction;
    public Interaction baseInteraction;
    public bool used;
    public int prefabIndex;
    dialogueBox dialogueManager;

    void Start()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<dialogueBox>();
    }


    public void StartInteraction()
    {
        if(used)
        {
            dialogueManager.StartDialogue(firstInteraction);
            return;
        }
        dialogueManager.StartDialogue(baseInteraction);
        used=true;
    }
}
