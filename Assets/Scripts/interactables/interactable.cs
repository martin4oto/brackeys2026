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
    public bool itemReward;

    void Start()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<dialogueBox>();
    }


    public void StartInteraction()
    {
        if(!used)
        {   
            dialogueManager.StartDialogue(firstInteraction);
            used=true;

            if(itemReward)
            {
                Item itemReward = firstInteraction.GetReward();

                dialogueManager.PrintLine(itemReward.itemName);
                Inventory.instance.AddItem(itemReward);
            }

            if(door)
            {
                MapManager.instance.RemoveInteractable(this);
                GameObject.Destroy(gameObject);
            }
            return;
        }
        dialogueManager.StartDialogue(baseInteraction);
    }
}
