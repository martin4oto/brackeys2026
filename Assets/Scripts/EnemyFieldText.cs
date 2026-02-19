using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFieldText : MonoBehaviour
{
    private int id;
    public TextMeshProUGUI name;
    public TextMeshProUGUI hpText;
    public Slider hpSlider;
    void Awake()
    {
        id=gameObject.transform.parent.gameObject.GetComponent<EnemyField>().id;
        name = gameObject.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>();
        hpText = gameObject.transform.Find("HP_text").gameObject.GetComponent<TextMeshProUGUI>();
        hpSlider= gameObject.transform.Find("HP_Slider").gameObject.GetComponent<Slider>();      
    }
    public void UpdateStats()
    {
        EnemyData cd = GameController.Instance.enemies[id];
        name.text=cd.enemyStats.enemyName;
        string hp=cd.hp+"/"+cd.enemyStats.maxHp;
        hpText.text=hp;
        hpSlider.maxValue=cd.enemyStats.maxHp;
        hpSlider.value=cd.hp;
        if(!GameController.Instance.enemies[id].alive)
        {
            gameObject.SetActive(false);
        }
    }

}
