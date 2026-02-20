using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "Scriptable Objects/Interaction")]
public class Interaction : ScriptableObject
{
    public string mainText;
    public string title;
    public Interaction nextInteraction;

    public Item[] itemRewards;

    public Item GetReward()
    {
        int index = Random.Range(0, itemRewards.Length-1);

        return itemRewards[index];
    }
}
