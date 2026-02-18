using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<PartyMember> partyMembers;
    public GameObject partyMemberPrefab;

    void Start()
    {
        SpawnParty();
    }

    public void SpawnParty()
    {
        List<CharacterGear> characters = Inventory.instance.characters;

        for(int i = 1; i<characters.Count; i++)
        {
            PartyMember member = GameObject.Instantiate(partyMemberPrefab, transform.position, Quaternion.identity).GetComponent<PartyMember>();

            partyMembers.Add(member);
            partyMembers[i-1].nextPartyMember = member;
        }
    }
}
