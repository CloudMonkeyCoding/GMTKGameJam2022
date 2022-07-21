using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public bool playersTurn;
    public enum Actions{Attack, RecklessAttack, Block, Potion}
    public PlayerActions playerActions;
    
    public GameObject[] enemies;
    public GameObject chosenEnemy;

    public bool playerWon;

    public Button[] playerButtons; //attack, reckless attack, potion

    public GameObject fadePanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI GKHP;
    public TextMeshProUGUI GHP;

    public Animator GKAnimator;

    public DiceRoller diceRoller;

    public float timePerTurn;

    public bool boss;

    private void Start() 
    {
        PlayerTurn();
        fadePanel.GetComponent<Animator>().SetTrigger("FadeStart");
        StartCoroutine(ActivateFade());
        UpdateUI();
    }

    public void PlayerTurn()
    {
        playersTurn = true;
        
        if(PlayerStats.hp < PlayerStats.maxHP)
        {
            PlayerStats.hp = PlayerStats.hp + 1;
        }
        foreach(Button button in playerButtons)
        {
            playerButtons[0].interactable = true;
            playerButtons[1].interactable = true;
            
            playerButtons[3].interactable = PlayerStats.potionCount > 0;
        }
    }

    public void EnemyTurn()
    {
        playerButtons[2].interactable = PlayerStats.sheildDurability > 0;
        playersTurn = false;
        diceRoller.StartRoll();
        enemies[0].GetComponent<Animator>().SetTrigger("Attack");
        GKAnimator.SetTrigger("Hurt");
        StartTimerForNext();
    }

    public void DeactivatePlayerButtons()
    {
        playerButtons[0].interactable = false;
        playerButtons[1].interactable = false;
        playerButtons[3].interactable = false;
    }

    public void StartTimerForNext()
    {
        StartCoroutine(timeBeforeNext());
    }

    IEnumerator timeBeforeNext()
    {
        yield return new WaitForSeconds(timePerTurn);
        if(playersTurn)
        {
            EnemyTurn();
        }
        else
        {
            PlayerTurn();
        }
    }

    public void Result(int n)
    {
        if(playersTurn)
        {
            switch(playerActions.currentAction)
            {
                default:
                case Actions.Attack:            
                    chosenEnemy.GetComponent<EnemyActions>().hp -= n;       
                    break;
                case Actions.RecklessAttack:    
                    chosenEnemy.GetComponent<EnemyActions>().hp -= n * 2;   
                    break;
                case Actions.Potion:            
                    PlayerStats.hp += n;
                    if(PlayerStats.hp > PlayerStats.maxHP)
                        PlayerStats.hp = PlayerStats.maxHP;
                    PlayerStats.potionCount -= 1;                                     
                    break;
            }
            CheckEnemies();
        }
        else
        {
            if(boss)
                n *= 2;
            switch(playerActions.currentAction)
            {
                default:
                    PlayerStats.hp -= n; 
                    break;
                case Actions.Block:
                    break;
                case Actions.RecklessAttack:
                    PlayerStats.hp -= n * 2;
                    break;
            }
            if(PlayerStats.hp <= 0)
            {
                PlayerStats.hp = 0;
                gameOverPanel.SetActive(true);
            }
            GKAnimator.SetBool("Block", false);
            
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        GKHP.text = "HP: " + PlayerStats.hp;
        GHP.text = "HP: " + chosenEnemy.GetComponent<EnemyActions>().hp;
    }

    void CheckEnemies()
    {
        int alive = 0;
        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<EnemyActions>().hp > 0)
            {
                alive++;
            }
        }
        if(alive <= 0)
        {
            StartCoroutine(EndGame());
            
        }
    }

    IEnumerator EndGame()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Animator>().SetTrigger("FadeEnd");
        SceneHandler.activeEnemies[SceneHandler.currentEnemy] = false;
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1);
    }

    IEnumerator ActivateFade()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Animator>().SetTrigger("FadeStart");
        yield return new WaitForSeconds(1.1f);
        fadePanel.SetActive(false);
    }

    public void Menu()
    {
        SceneHandler.restart = true;
        SceneManager.LoadScene(0);
    }
}