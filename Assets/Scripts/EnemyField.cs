using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int id;
    public bool isClicked = false;
    private Color defaultColor = Color.white;
    private Color clickedColor = Color.red;
    private Color hoverColor = Color.red;
    

    private void OnEnable()
    {
        EnemyFieldClickHandler.OnAnyFieldClicked += ResetClick;
    }

    private void OnDisable()
    {
        EnemyFieldClickHandler.OnAnyFieldClicked -= ResetClick;
    }
    private bool IsInRange()
    {
        int activeId = BattleManager.Instance.activeId;
        if (BattleManager.Instance.combatStage)
        {
            int range = GameController.Instance.characters[activeId].characterData.range;
            return (id == activeId - range) || (id == activeId + range);
        }
        else if(BattleManager.Instance.itemStage)
        {
            //...
            return false;
        }
        else if(BattleManager.Instance.skillStage)
        {
            
            return true;
        }
        return false;
    }

    private bool CanInteract()
    {
        return IsInRange() && 
               GameController.Instance.enemies[id].enemyStats != null && 
               GameController.Instance.enemies[id].alive && 
               BattleManager.Instance.enemySelection;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked && CanInteract())
        {
            gameObject.GetComponent<Image>().color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked && CanInteract())
        {
            gameObject.GetComponent<Image>().color = defaultColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanInteract())
        {
            isClicked = true;
            BattleManager.Instance.enemyClickedId=id;
            gameObject.GetComponent<Image>().color = clickedColor;
            EnemyFieldClickHandler.OnAnyFieldClicked?.Invoke(id);
            if(BattleManager.Instance.skillStage)
            {
                BattleManager.Instance.UseSkill(false);
            }
        }
    }

    public void ResetClick(int clickedFieldId = -1)
    {
        if (clickedFieldId != id)
        {
            isClicked = false;
            gameObject.GetComponent<Image>().color = defaultColor;
        }
    }
}

public static class EnemyFieldClickHandler
{
    public static System.Action<int> OnAnyFieldClicked;
}