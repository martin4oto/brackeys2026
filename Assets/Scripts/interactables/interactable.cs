using UnityEngine;
using UnityEngine.InputSystem;

public class interactable : MonoBehaviour
{
    public Interaction firstInteraction;
    public Interaction baseInteraction;
    public bool used;
    public int prefabIndex;
    dialogueBox dialogueManager;
    public bool door;

    void Start()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<dialogueBox>();
    }


    public void StartInteraction()
    {
        if(!used)
        {
            Debug.Log(firstInteraction);
            dialogueManager.StartDialogue(firstInteraction);
            if(door)
            {
                GameObject.Destroy(gameObject);
            }
            return;
        }
        dialogueManager.StartDialogue(baseInteraction);
        used=true;
    }
}
