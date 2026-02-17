using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public GameObject combatPanel;
    public GameObject panel;
    public GameObject attackPanel;
    public Sprite background;
    public GameObject party;
    private SpriteRenderer[] partySprites= new SpriteRenderer[4];
    private SpriteRenderer[] enemySprites= new SpriteRenderer[4];
    public GameObject enemyParty;
    public Button attackButton;
    public Button itemButton;
    public Button skillsButton;
    public Button skipButton; //go back to action panel
    public Button cancelButton;
    public Button strikeButton;

    public Button moveButton;
    public GameObject[] friendlyFields;
    public GameObject[] enemyFields;

    public GameObject movePanel;
    public GameObject actionPanel;
    public bool moveStage;
    public bool actionStage;
    private bool enemyMoveStage;
    private bool enemyActionStage;
    public int turnCounter;
    public int activeId =0;
    public int clickedId;
    public int enemyClickedId;
    public bool enemySelection;
    public bool combatStage;
    public bool itemStage;
    public bool skillStage;
    public static BattleManager Instance {
        get;
        set;
    }
    void Awake()
    {

        if(Instance!=null)
            GameObject.Destroy(gameObject);
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;

        attackButton.onClick.AddListener(() => OnAttackButtonClicked());
        itemButton.onClick.AddListener(() => OnItemButtonClicked());
        skillsButton.onClick.AddListener(() => OnSkillsButtonClicked());
        skipButton.onClick.AddListener(() => ShowActionPanel());
        moveButton.onClick.AddListener(() => OnMoveButtonClicked());
        cancelButton.onClick.AddListener(() => ShowActionPanel());
        strikeButton.onClick.AddListener(() => Attack());

        string file = "Sprites/battleBackground" + GameController.Instance.locationID;
        background= Resources.Load<Sprite>(file);
        panel.GetComponent<Image>().sprite=background;
        //TODO add music file
        partySprites[0]=party.transform.Find("Character1").GetComponent<SpriteRenderer>();
        partySprites[1]=party.transform.Find("Character2").GetComponent<SpriteRenderer>();
        partySprites[2]=party.transform.Find("Character3").GetComponent<SpriteRenderer>();
        partySprites[3]=party.transform.Find("Character4").GetComponent<SpriteRenderer>();
        enemySprites[0]=enemyParty.transform.Find("Enemy1").GetComponent<SpriteRenderer>();
        enemySprites[1]=enemyParty.transform.Find("Enemy2").GetComponent<SpriteRenderer>();
        enemySprites[2]=enemyParty.transform.Find("Enemy3").GetComponent<SpriteRenderer>();
        enemySprites[3]=enemyParty.transform.Find("Enemy4").GetComponent<SpriteRenderer>();
        for(int i=0;i<4;i++)
        {
            if (GameController.Instance.characters[i] != null)
            {
              partySprites[i].sprite=GameController.Instance.characters[i].combatSprite;
            }
        }
        for(int i=0;i<GameController.Instance.enemies.Length;i++)
        {
            if(GameController.Instance.enemies[i] != null)
                enemySprites[i].sprite=GameController.Instance.enemies[i].combatSprite;
        }

        moveStage=false;
        actionStage=false;
        enemyActionStage=false;
        enemyMoveStage=false;
        enemySelection=false;
        combatStage=false;
        itemStage=false;
        skillStage=false;
        movePanel.SetActive(false);
        actionPanel.SetActive(false);
        turnCounter=1;
        friendlyTurn(0);
    }

    public void friendlyTurn(int counter)
    {
        if (GameController.Instance.characters[counter] != null && GameController.Instance.characters[counter].alive)
        {
            friendlyFields[counter].GetComponent<Image>().color = Color.green;
            activeId=counter;
            clickedId=-1;
            enemyClickedId = -1;
            moveStage=true;
            movePanel.SetActive(true);
        }
        /*
        else if(counter<3)
        {
            counter++;
            friendlyTurn(counter);
        }
        else if(counter==3)
        {
            enemyTurn(0);
        }
        */
    }
    public void enemyTurn(int counter)
    {
        
    }
    void Attack()
    {
        if(combatStage && enemyClickedId!=-1)
        {
            

            attackPanel.SetActive(false);
            combatStage=false;
            enemySelection=false;
            enemyFields[enemyClickedId].GetComponent<EnemyField>().isClicked = false;
            friendlyFields[activeId].GetComponent<Image>().color = Color.white;
            enemyFields[enemyClickedId].GetComponent<Image>().color = Color.white;
            enemyClickedId=-1;
            actionStage=false;
        }
    }
    void OnMoveButtonClicked()
    {
        if(moveStage && clickedId!=-1)
        {
            Sprite sprite = partySprites[activeId].sprite;
            partySprites[activeId].sprite = partySprites[clickedId].sprite;
            partySprites[clickedId].sprite= sprite;

            CharacterData cd = GameController.Instance.characters[activeId];
            GameController.Instance.characters[activeId] = GameController.Instance.characters[clickedId];
            GameController.Instance.characters[clickedId] = cd;
//            GameObject field = friendlyFields[activeId];
            friendlyFields[clickedId].GetComponent<Image>().color = Color.green;
            friendlyFields[clickedId].GetComponent<Field>().isClicked = false;
            friendlyFields[activeId].GetComponent<Image>().color = Color.white;

            activeId=clickedId;
            moveStage=false;
            actionStage=true;
            clickedId=-1;
            movePanel.SetActive(false);
            actionPanel.SetActive(true);
        }
    }
    void OnAttackButtonClicked()
    {
        actionPanel.SetActive(false);
        attackPanel.SetActive(true);
        enemySelection=true;
        combatStage=true;
    }
    void OnItemButtonClicked()
    {
        //... pannels
        enemySelection=true;
        itemStage=true;
    }
    void OnSkillsButtonClicked()
    {
        //... pannels
        enemySelection=true;
        skillStage=true;
    }
    void ShowActionPanel()
    {
        if(clickedId!=-1)
        {
            friendlyFields[clickedId].GetComponent<Image>().color = Color.white;
            friendlyFields[clickedId].GetComponent<Field>().isClicked = false;
            clickedId=-1;
        }
        if(enemyClickedId!=-1)
        {
            enemyFields[enemyClickedId].GetComponent<Image>().color = Color.white;
            enemyFields[enemyClickedId].GetComponent<EnemyField>().isClicked = false;

            enemyClickedId=-1;
        }
        moveStage=false;
        actionStage=true;
        enemySelection=false;
        combatStage=false;
        itemStage=false;
        skillStage=false;
        movePanel.SetActive(false);
        attackPanel.SetActive(false);
        actionPanel.SetActive(true);
        //more pannels..
    }
    private void EndBattle()
    {
        //show victory screen
        //add xp
        //return to default scene
    }
}
