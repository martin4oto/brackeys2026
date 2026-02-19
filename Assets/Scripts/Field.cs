using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int id;
    public bool isClicked = false;
    private Color defaultColor = Color.white;
    private Color clickedColor = Color.red;
    private Color hoverColor = Color.red;

    private void OnEnable()
    {
        FieldClickHandler.OnAnyFieldClicked += ResetClick;
    }

    private void OnDisable()
    {
        FieldClickHandler.OnAnyFieldClicked -= ResetClick;
    }
    private bool IsAdjacentToActive()
    {
        int activeId = BattleManager.Instance.activeId;
        if(BattleManager.Instance.moveStage)
            return ((id == activeId - 1) || (id == activeId + 1));
        else if(BattleManager.Instance.skillStage
        && GameController.Instance.characters[id].characterData!= null 
        && GameController.Instance.characters[id].alive)
        {
            return true;
        }

        return false;
    }

    private bool CanInteract()
    {
        return IsAdjacentToActive() && 
               id != BattleManager.Instance.activeId &&
               BattleManager.Instance.friendlyTargetStage;
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
            BattleManager.Instance.clickedId=id;
            gameObject.GetComponent<Image>().color = clickedColor;
            FieldClickHandler.OnAnyFieldClicked?.Invoke(id);
            if(BattleManager.Instance.skillStage)
            {
                BattleManager.Instance.UseSkill(true);
            }
        }
    }

    public void ResetClick(int clickedFieldId = -1)
    {
        if (clickedFieldId != id && id!=BattleManager.Instance.activeId)
        {
            isClicked = false;
            gameObject.GetComponent<Image>().color = defaultColor;
        }
    }
}

public static class FieldClickHandler
{
    public static System.Action<int> OnAnyFieldClicked;
}