using UnityEngine;

public class GridMovement : MonoBehaviour
{
    bool isMoving = false;
    Vector2 movementPosition;
    Vector2 direction;
    public float speed;

    public bool isPlayer;
    public playerControlScript playerControlScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            Vector2 momentum = direction*speed*Time.deltaTime;

            if(IsMovementComplete(momentum))
            {
                return;
            }
            transform.position += (Vector3)momentum;
        }
    }

    public void StartMoving(Vector2 position)
    {
        if(isMoving)return;

        movementPosition = position;
        PositionChange(position);
        isMoving = true;
    }

    public void Redirect(Vector2 position)
    {
        movementPosition = position;
        PositionChange(position);
        isMoving = true;
    }

    //calculating the normal vector of the direection
    void PositionChange(Vector3 position)
    {
        direction = (Vector2)(position - transform.position);
        Vector3.Normalize(direction);
    }

    bool IsMovementComplete(Vector2 momentum)
    {
        //distance squared
        float currentDistance = CalculateCurrentDistance();

        float velocity = momentum.x*momentum.x + momentum.y*momentum.y;

        if(currentDistance<velocity)
        {
            CompleteMovement();
            return true;
        }
        return false;
    }

    float CalculateCurrentDistance()
    {
        Vector2 distance = (Vector2)transform.position - movementPosition;

        return distance.x*distance.x + distance.y*distance.y;
    }

    void CompleteMovement()
    {
        transform.position = movementPosition;
        isMoving = false;

        if(isPlayer)playerControlScript.Stopped();
    }
}
