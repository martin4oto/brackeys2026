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
    public void Setup()
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
        Debug.Log(skill1.manaCost + " " + GameController.Instance.characters[activeID].mana);
        if(skill1!=null && skill1.manaCost<=GameController.Instance.characters[activeID].mana)
        {
            Debug.Log(skill1.manaCost);
            BattleManager.Instance.manaCost=(int)skill1.manaCost;
            BattleManager.Instance.activeSkillId=skill1.id;
            if(skill1.enemyTarget)
            {
                BattleManager.Instance.enemySelection=true;
                BattleManager.Instance.friendlyTargetStage=false;
            }
            else if(skill1.friendlyTarget)
            {
                BattleManager.Instance.friendlyTargetStage=true;
                BattleManager.Instance.enemySelection=false;
            }
        }
    }
    void skill2Clicked()
    {
        if(skill2!=null && skill2.manaCost<=GameController.Instance.characters[activeID].mana)
        {
            BattleManager.Instance.activeSkillId=skill2.id;
            BattleManager.Instance.manaCost=(int)skill2.manaCost;
            if(skill2.enemyTarget)
            {
                BattleManager.Instance.enemySelection=true;
                BattleManager.Instance.friendlyTargetStage=false;
            }
            else if(skill2.friendlyTarget)
            {
                BattleManager.Instance.friendlyTargetStage=true;
                BattleManager.Instance.enemySelection=false;
            }
        }
    }
    void skill3Clicked()
    {
        if(skill3!=null && skill3.manaCost<=GameController.Instance.characters[activeID].mana)
        {
            BattleManager.Instance.activeSkillId=skill3.id;
            BattleManager.Instance.manaCost=(int)skill3.manaCost;
            if(skill3.enemyTarget)
            {
                BattleManager.Instance.enemySelection=true;
                BattleManager.Instance.friendlyTargetStage=false; 
            }
            else if(skill3.friendlyTarget)
            {
                BattleManager.Instance.friendlyTargetStage=true;
                BattleManager.Instance.enemySelection=false;
            } 
        } 
    }

}
