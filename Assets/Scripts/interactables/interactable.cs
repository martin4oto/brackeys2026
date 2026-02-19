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
    GameObject roof;

    void Start()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<dialogueBox>();

        if(door)
        {
            roof = transform.GetChild(0).gameObject;
        }
    }


    public void StartInteraction()
    {
        if(used)
        {
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
