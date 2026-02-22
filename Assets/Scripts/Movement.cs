using NUnit.Framework;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public bool isMoving = false;
    public Vector2 movementPosition;
    public Vector2 direction;
    public float speed;

    public bool isPlayer;
    public bool isEnemy;
    public playerControlScript playerControlScript;

    animationManager animationChanger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationChanger = GetComponent<animationManager>();
        animationChanger.SetUp();
        animationChanger.AnimationStop();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            Vector2 momentum = direction*speed*Time.deltaTime;

            if(IsMovementComplete(momentum))
            {
                animationChanger.AnimationStop();
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
        isMoving = true;
        PositionChange(position);
    }

    //calculating the normal vector of the direection
    void PositionChange(Vector3 position)
    {
        direction = (Vector2)(position - transform.position);
        Vector3.Normalize(direction);
        animationChanger.AnimationChange(direction);
    }

    bool IsMovementComplete(Vector2 momentum)
    {
        //distance squared
        float currentDistance = CalculateCurrentDistance();

        float velocity = momentum.x*momentum.x + momentum.y*momentum.y;

        if(currentDistance<velocity||currentDistance<0.01)
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
        else if(isEnemy)
        {
            GetComponent<Enemy>().isMoving = false;
        }
    }
}
