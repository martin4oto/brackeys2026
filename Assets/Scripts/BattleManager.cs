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
    public Sprite background;
    public GameObject party;
    private SpriteRenderer[] partySprites= new SpriteRenderer[4];
    private SpriteRenderer[] enemySprites= new SpriteRenderer[5];
    public GameObject enemyParty;
    public Button attackButton;
    public Button itemButton;
    public Button skillsButton;
    public Button backButton; //go back to action panel
    void Awake()
    {

        attackButton.onClick.AddListener(() => OnAttackButtonClicked());
        itemButton.onClick.AddListener(() => OnItemButtonClicked());
        skillsButton.onClick.AddListener(() => OnSkillsButtonClicked());
        backButton.onClick.AddListener(() => ShowActionPanel());

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
        enemySprites[4]=enemyParty.transform.Find("Enemy5").GetComponent<SpriteRenderer>();
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
    }
    void OnAttackButtonClicked()
    {
        
    }
    void OnItemButtonClicked()
    {
        
    }
    void OnSkillsButtonClicked()
    {
        
    }
    void ShowActionPanel()
    {
        
    }
    private void EndBattle()
    {
        //show victory screen
        //add xp
        //return to default scene
    }
}
