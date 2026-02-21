using UnityEngine;
using UnityEngine.InputSystem;

public class interactable : MonoBehaviour
{
    public Interaction firstInteraction;
    public Interaction baseInteraction;
    public bool used;
    public int prefabIndex;
    dialogueBox dialogueManager;
    public bool destroy;
    public bool itemReward;

    void Start()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<dialogueBox>();
    }


    public void StartInteraction()
    {
        if(!used)
        {   
            Time.timeScale = 0;
            dialogueManager.StartDialogue(firstInteraction);
            used=true;

            if(itemReward)
            {
                Item itemReward = firstInteraction.GetReward();

                dialogueManager.PrintLine(itemReward.itemName);
                Inventory.instance.AddItem(itemReward);
            }

            if(destroy)
            {
                MapManager.instance.RemoveInteractable(this);
                GameObject.Destroy(gameObject);
            }
            return;
        }
        dialogueManager.StartDialogue(baseInteraction);
    }
}
