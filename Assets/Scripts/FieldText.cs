using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldText : MonoBehaviour
{
    private int id;
    public TextMeshProUGUI name;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI manaText;
    public Slider hpSlider;
    public Slider manaSlider;
    void Awake()
    {
        id=gameObject.transform.parent.gameObject.GetComponent<Field>().id;
        name = gameObject.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>();
        hpText = gameObject.transform.Find("HP_text").gameObject.GetComponent<TextMeshProUGUI>();
        manaText = gameObject.transform.Find("Mana_text").gameObject.GetComponent<TextMeshProUGUI>();
        hpSlider= gameObject.transform.Find("HP_Slider").gameObject.GetComponent<Slider>();   
        manaSlider= gameObject.transform.Find("Mana_Slider").gameObject.GetComponent<Slider>();    
    }
    public void UpdateStats()
    {
        Data cd = GameController.Instance.characters[id];
        name.text=cd.characterData.characterName;
        string hp=cd.currentHP+"/"+cd.characterData.maxHP;
        hpText.text=hp;
        hpSlider.maxValue=cd.characterData.maxHP;
        hpSlider.value=cd.currentHP;
        string mana=cd.mana+"/"+cd.characterData.maxMana;
        manaText.text=mana;
        manaSlider.maxValue=cd.characterData.maxMana;
        manaSlider.value=cd.mana;
    }
}
