using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    public GameObject ItemMenu;
    public GameObject skillPanel;
    public GameObject combatPanel;
    public GameObject panel;
    public GameObject attackPanel;
    public GameObject victoryPanel;
    public Sprite background;
    public GameObject party;
    private SpriteRenderer[] partySprites= new SpriteRenderer[4];
    private SpriteRenderer[] enemySprites= new SpriteRenderer[4];
    public GameObject enemyParty;
    public Button attackButton;
    public Button itemButton;
    public Button skillsButton;
    public Button skipButton; //go back to action panel
    public Button cancelButton; //go back to action panel
    public Button cancelButton1;
    public Button cancelButton2;
    public Button strikeButton;

    public Button moveButton;
    public GameObject[] friendlyFields;
    public GameObject[] enemyFields;

    public GameObject movePanel;
    public GameObject actionPanel;
    public int itemSkillId;
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
    public bool friendlyTargetStage;
    public int activeSkillId;
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
        cancelButton1.onClick.AddListener(() => ShowActionPanel());
        cancelButton2.onClick.AddListener(() => ShowActionPanel());

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
            if (GameController.Instance.characters[i].characterData != null)
            {
              partySprites[i].sprite=GameController.Instance.characters[i].characterData.combatSprite;
            }
        }
        for(int i=0;i<GameController.Instance.enemies.Length;i++)
        {
            if(GameController.Instance.enemies[i].enemyStats != null)
                enemySprites[i].sprite=GameController.Instance.enemies[i].enemyStats.combatSprite;
        }

        moveStage=false;
        actionStage=false;
        enemyActionStage=false;
        enemyMoveStage=false;
        enemySelection=false;
        combatStage=false;
        itemStage=false;
        skillStage=false;
        friendlyTargetStage=false;
        movePanel.SetActive(false);
        actionPanel.SetActive(false);
        ItemMenu.SetActive(false);
        turnCounter=1;
        RefreshFieldText();
        friendlyTurn(0);
    }

    public void friendlyTurn(int counter)
    {
        if (GameController.Instance.characters[counter].characterData != null && GameController.Instance.characters[counter].alive)
        {
            friendlyFields[counter].GetComponent<Image>().color = Color.green;
            activeId=counter;
            clickedId=-1;
            enemyClickedId = -1;
            moveStage=true;
            friendlyTargetStage=true;
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
    public bool CheckEnemiesAlive()
    {
        int countAll=0;
        int countDead=0;
        for(int i=0;i<GameController.Instance.enemies.Length;i++)
        {
            if(GameController.Instance.enemies[i].enemyStats!=null)
            {
                countAll++;
                if(!GameController.Instance.enemies[i].alive)
                {
                    countDead++;
                }
            }
        }
        return(countAll!=countDead);
    }
    void CheckEnemyAlive()
    {
        if(GameController.Instance.enemies[enemyClickedId].hp<=0)
        {
            Debug.Log("enemy dead");
            GameController.Instance.enemies[enemyClickedId].hp=0;
            GameController.Instance.enemies[enemyClickedId].alive=false;
            enemyFields[enemyClickedId].GetComponent<Image>().color=Color.white;
            enemySprites[enemyClickedId].sprite=GameController.Instance.enemies[enemyClickedId].enemyStats.deadSprite;
            enemyFields[enemyClickedId].transform.Find("Text").gameObject.SetActive(false);
                
            if(!CheckEnemiesAlive())
            {
                EndBattleWin();
            }
                //check if all are dead...
        }
    }
    void Attack()
    {
        if(combatStage && enemyClickedId!=-1)
        {
            //atack logiccc
            enemyFields[enemyClickedId].GetComponent<Image>().color = Color.white;
            int dmg=GameController.Instance.characters[activeId].atkBonus+GameController.Instance.characters[activeId].characterData.baseAtk;
            float num=Random.Range(0.0f,100.0f);
            if(num<=GameController.Instance.characters[activeId].critChance)
            {
                dmg*=2;
                Debug.Log("You critted");  
            }
            GameController.Instance.enemies[enemyClickedId].hp-=dmg;

            CheckEnemyAlive();
            RefreshFieldText();
            attackPanel.SetActive(false);
            combatStage=false;
            enemySelection=false;
            enemyFields[enemyClickedId].GetComponent<EnemyField>().isClicked = false;
            friendlyFields[activeId].GetComponent<Image>().color = Color.white;

            enemyClickedId=-1;
            actionStage=false;
            enemySelection=false;



        }
    }
    public void UseSkill(bool friendly)
    {
        if(friendly)
            SkillController.Instance.use(activeSkillId,clickedId,activeId);
        else
        {
            SkillController.Instance.use(activeSkillId,enemyClickedId,activeId);
            CheckEnemyAlive();
        }
        RefreshFieldText();

        skillPanel.SetActive(false);
        if(enemyClickedId>=0)
        {
            enemyFields[enemyClickedId].GetComponent<Image>().color = Color.white;
            enemyFields[enemyClickedId].GetComponent<EnemyField>().isClicked = false;
        }
        if(clickedId>=0)
        {
            friendlyFields[clickedId].GetComponent<Image>().color = Color.white;
            friendlyFields[clickedId].GetComponent<Field>().isClicked = false;
        }
        friendlyFields[activeId].GetComponent<Image>().color = Color.white;
        skillStage=false;
        actionStage=false;
        enemyClickedId=-1;
        clickedId=-1;
        enemySelection=false;
        friendlyTargetStage=false;


    }
    void RefreshFieldText()
    {
        for(int i=0;i<friendlyFields.Length;i++)
        {
            if(GameController.Instance.characters[i].characterData != null)
            {
                friendlyFields[i].transform.Find("Text").gameObject.SetActive(true);
                friendlyFields[i].transform.Find("Text").gameObject.GetComponent<FieldText>().UpdateStats();
            }
            else
            {
                friendlyFields[i].transform.Find("Text").gameObject.SetActive(false);
            }
            
        }
        for(int i=0;i<enemyFields.Length;i++)
        {
            if(GameController.Instance.enemies[i].enemyStats != null)
            {
                enemyFields[i].transform.Find("Text").gameObject.SetActive(true);
                enemyFields[i].transform.Find("Text").gameObject.GetComponent<EnemyFieldText>().UpdateStats();
            }
            else
            {
                enemyFields[i].transform.Find("Text").gameObject.SetActive(false);
            }
        }
    }
    void OnMoveButtonClicked()
    {
        if(moveStage && clickedId!=-1)
        {
            Sprite sprite = partySprites[activeId].sprite;
            partySprites[activeId].sprite = partySprites[clickedId].sprite;
            partySprites[clickedId].sprite= sprite;

            Data cd = GameController.Instance.characters[activeId];
            GameController.Instance.characters[activeId] = GameController.Instance.characters[clickedId];
            GameController.Instance.characters[clickedId] = cd;
//            GameObject field = friendlyFields[activeId];
            friendlyFields[clickedId].GetComponent<Image>().color = Color.green;
            friendlyFields[clickedId].GetComponent<Field>().isClicked = false;
            friendlyFields[activeId].GetComponent<Image>().color = Color.white;

            activeId=clickedId;
            moveStage=false;
            friendlyTargetStage=false;
            actionStage=true;
            clickedId=-1;
            movePanel.SetActive(false);
            actionPanel.SetActive(true);
            RefreshFieldText();
        }
    }
    public void UseItem(bool friendly)
    {
        if(friendly)
            SkillController.Instance.use(activeSkillId,clickedId,activeId);
        else
        {
            SkillController.Instance.use(activeSkillId,enemyClickedId,activeId);
            CheckEnemyAlive();
        }
        RefreshFieldText();

        ItemMenu.SetActive(false);
        if(enemyClickedId>=0)
        {
            enemyFields[enemyClickedId].GetComponent<Image>().color = Color.white;
            enemyFields[enemyClickedId].GetComponent<EnemyField>().isClicked = false;
        }
        if(clickedId>=0)
        {
            friendlyFields[clickedId].GetComponent<Image>().color = Color.white;
            friendlyFields[clickedId].GetComponent<Field>().isClicked = false;
        }
        friendlyFields[activeId].GetComponent<Image>().color = Color.white;
        itemStage=false;
        actionStage=false;
        enemyClickedId=-1;
        clickedId=-1;
        enemySelection=false;
        friendlyTargetStage=false;
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
        itemSkillId=-1;
        ItemMenu.SetActive(true);
        actionPanel.SetActive(false);

        ItemMenu.GetComponent<InventoryMenu>().OpenMenu();
        Debug.Log("test");


        itemStage=true;
    }
    void OnSkillsButtonClicked()
    {
        actionPanel.SetActive(false);
        skillPanel.SetActive(true);
        enemySelection=false;
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
        itemSkillId=-1;
        activeSkillId=-1;
        moveStage=false;
        actionStage=true;
        enemySelection=false;
        friendlyTargetStage=false;
        combatStage=false;
        itemStage=false;
        skillStage=false;
        ItemMenu.SetActive(false);
        skillPanel.SetActive(false);
        movePanel.SetActive(false);
        attackPanel.SetActive(false);
        actionPanel.SetActive(true);
        //more pannels..
    }
    private void EndBattleWin()
    {
        Debug.Log("Win");
        victoryPanel.SetActive(true);
        //show victory screen
        //add xp
        //return to default scene
    }
}
