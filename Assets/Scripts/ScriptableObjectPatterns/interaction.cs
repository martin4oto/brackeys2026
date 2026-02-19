using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "Scriptable Objects/Interaction")]
public class Interaction : ScriptableObject
{
    public string mainText;
    public string title;
    public Interaction nextInteraction;

    public Item[] itemRewards;
}
