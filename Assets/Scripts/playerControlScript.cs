using UnityEngine;
using UnityEngine.InputSystem;

public class playerControlScript : MonoBehaviour
{
    bool isMoving = false;

    public PartyMember playerMember;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    bool StartWalkingTest(Vector2 movementVector)
    {
        if(movementVector.x != 0 || movementVector.y != 0)
        {
            movementVector = (Vector2)transform.position + movementVector;

            playerMember.StartChain(movementVector);
            isMoving = true;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)return;
        Vector2 movementVector = AddToVectorHorizontally();
        
        if(StartWalkingTest(movementVector))
        {
            return;
        }

        movementVector = AddToVectorVertically();
        
        StartWalkingTest(movementVector);
    }

    Vector2 AddToVectorHorizontally()
    {
        Vector2 movementVector = new Vector2(0,0);
        if(Keyboard.current.aKey.isPressed)
        {
            movementVector += Vector2.left;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            movementVector += Vector2.right;
        }
        return movementVector;
    }

    Vector2 AddToVectorVertically()
    {
        Vector2 movementVector = new Vector2(0,0);
        if(Keyboard.current.sKey.isPressed)
        {
            movementVector += Vector2.down;
        }
        if(Keyboard.current.wKey.isPressed)
        {
            movementVector += Vector2.up;
        }
        return movementVector;
    }

    public void Stopped()
    {
        isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "walls")
        {
            playerMember.Return();
        }
    }
}
