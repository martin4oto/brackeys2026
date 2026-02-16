using UnityEngine;
using UnityEngine.Rendering;

public class cameraController : MonoBehaviour
{
    public float speed;
    public Transform Player;
    public float minimum;
    public float threshhold;
    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movemntVector = Player.position - transform.position;
        float distance = movemntVector.x*movemntVector.x+movemntVector.y*movemntVector.y;

        if(!isMoving && distance>threshhold)
        {
            isMoving = true;
            return;
        }
        else if(distance < minimum&&isMoving)
        {
            transform.position = Player.position + Vector3.back*10;
            isMoving = false;
            return;
        }

        if(isMoving)
        {
            distance+=2f;
            float movemntNumber = distance*distance*speed;

            if(movemntNumber<0.5f)
            {
                movemntNumber = 0.5f;
            }

            movemntVector *= movemntNumber;

            transform.position += (Vector3)movemntVector*Time.deltaTime;
        }
    }
}
