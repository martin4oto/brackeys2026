using UnityEngine;

public class PartyMember : MonoBehaviour
{
    public PartyMember nextPartyMember;
    public GridMovement movement;
    Vector2 lastPosition;

    void Start()
    {
        movement = GetComponent<GridMovement>();
    }

    public void StartChain(Vector3 position)
    {
        lastPosition = transform.position;

        movement.StartMoving(position);

        if(nextPartyMember == null)return;
        nextPartyMember.StartChain(transform.position);
    }

    public void Return()
    {
        movement.Redirect(lastPosition);

        if(nextPartyMember == null)return;
        nextPartyMember.Return();
    }
}
