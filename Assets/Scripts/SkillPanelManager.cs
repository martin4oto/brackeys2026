using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelManager : MonoBehaviour
{
    public Button skillBtn1;
    public Button skillBtn2;
    public Button skillBtn3;
    private Skill skill1;
    private Skill skill2;
    private Skill skill3;
    private int activeID;
    void Start()
    {
        skillBtn1.onClick.AddListener(() => skill1Clicked());
        skillBtn2.onClick.AddListener(() => skill2Clicked());
        skillBtn3.onClick.AddListener(() => skill3Clicked());
        activeID=BattleManager.Instance.activeId;
        if(GameController.Instance.characters[activeID].characterData.skill1 != null)
        {
            skill1=GameController.Instance.characters[activeID].characterData.skill1;
            skillBtn1.transform.Find("Text").GetComponent<TextMeshProUGUI>().text=skill1.name;
        }
        else
        {
            skill1=null;
            skillBtn1.transform.Find("Text").GetComponent<TextMeshProUGUI>().text="Empty";
        }
        if(GameController.Instance.characters[activeID].characterData.skill2 != null)
        {
            skill2=GameController.Instance.characters[activeID].characterData.skill2;
            skillBtn2.transform.Find("Text").GetComponent<TextMeshProUGUI>().text=skill2.name;
        }
        else
        {
            skill2=null;
            skillBtn2.transform.Find("Text").GetComponent<TextMeshProUGUI>().text="Empty";
        }
        if(GameController.Instance.characters[activeID].characterData.skill3 != null)
        {
            skill3=GameController.Instance.characters[activeID].characterData.skill3;
            skillBtn3.transform.Find("Text").GetComponent<TextMeshProUGUI>().text=skill3.name;
        }
        else
        {
            skill3=null;
            skillBtn3.transform.Find("Text").GetComponent<TextMeshProUGUI>().text="Empty";
        }
    }
    void skill1Clicked()
    {
        if(skill1!=null)
        {
            BattleManager.Instance.enemySelection=true;
            if(skill1.enemyTarget)
            {
                
            }
            else if(skill1.friendlyTarget)
            {
                
            }
            else if(skill1.selfTarget)
            {
                
            }
        }
    }
    void skill2Clicked()
    {
        if(skill2!=null)
        {
            BattleManager.Instance.enemySelection=true;
            if(skill2.enemyTarget)
            {
                
            }
            else if(skill2.friendlyTarget)
            {
                
            }
            else if(skill2.selfTarget)
            {
                
            }
        }
    }
    void skill3Clicked()
    {
        if(skill3!=null)
        {
            BattleManager.Instance.enemySelection=true;
            if(skill3.enemyTarget)
            {
                
            }
            else if(skill3.friendlyTarget)
            {
                
            } 
            else if(skill3.selfTarget)
            {
                
            }
        } 
    }

}
