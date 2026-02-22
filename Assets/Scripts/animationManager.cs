using UnityEngine;

public class animationManager : MonoBehaviour
{
    Animator animator;

    string currentAnimation = "front";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetUp()
    {
        animator = GetComponent<Animator>();
    }

    void StartAnimationBack()
    {
        animator.SetBool("back", true);
        animator.SetBool("front", false);
        animator.SetBool("side", false);

        currentAnimation = "back";
    }

    void StartAnimationFront()
    {
        animator.SetBool("back", false);
        animator.SetBool("front", true);
        animator.SetBool("side", false);

        currentAnimation = "front";
    }

    void StartAnimationSide()
    {
        animator.SetBool("back", false);
        animator.SetBool("front", false);
        animator.SetBool("side", true);

        currentAnimation = "side";
    }

    public void AnimationChange(Vector2 direction)
    {
        FixRotation(direction.x);
        if(direction.y < 0)
        {
            StartAnimationFront();
        }
        else if(direction.y > 0)
        {
            StartAnimationBack();
        }
        else
        {
            StartAnimationSide();
        }
        animator.speed = 0.5f;
    }

    void FixRotation(float x)
    {
        if(x<0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    public void AnimationStop()
    {
        animator.speed = 0;
    }
}
