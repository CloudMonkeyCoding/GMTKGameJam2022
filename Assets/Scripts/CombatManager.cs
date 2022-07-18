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

    public TextMeshProUGUI GKHP;
    public TextMeshProUGUI GHP;

    private void Start() 
    {
        PlayerTurn();
        fadePanel.GetComponent<Animator>().SetTrigger("FadeStart");
        StartCoroutine(activateFade(false));
        UpdateUI();
    }

    public void PlayerTurn()
    {
        if(PlayerStats.hp < PlayerStats.maxHP)
        {
            PlayerStats.hp = PlayerStats.hp + 1;
        }
        foreach(Button button in playerButtons)
        {
            if(PlayerStats.potionCount < 0)
            {
                playerButtons[2].interactable = true;
            }
            playerButtons[1].interactable = true;
            playerButtons[0].interactable = true;
        }
    }

    public void EnemyTurn()
    {
        
    }

    public void DeactivatePlayerButtons()
    {
        foreach(Button button in playerButtons)
        {
            button.interactable = false;
        }
    }

    IEnumerator timeBeforeNext()
    {
        yield return new WaitForSeconds(5f);
        EnemyTurn();
    }

    public void Result(int n)
    {
        if(playersTurn)
        {
            switch(playerActions.currentAction)
            {
                default:
                case Actions.Attack:            chosenEnemy.GetComponent<EnemyActions>().hp -= n;       break;
                case Actions.RecklessAttack:    chosenEnemy.GetComponent<EnemyActions>().hp -= n * 2;   break;
                case Actions.Potion:            PlayerStats.hp += n;                                    break;
            }
        }
        else
        {
            foreach(GameObject enemy in enemies)
            {
                switch(enemy.GetComponent<EnemyActions>().currentAction)
                {
                    default:
                    case Actions.Attack: PlayerStats.hp -= n;       break;
                }
            }
        }
        CheckEnemies();
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
            if(enemy.GetComponent<EnemyActions>().hp >= 0)
            {
                alive++;
            }
        }
        if(alive <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        fadePanel.GetComponent<Animator>().SetBool("ending", true);
        SceneHandler.activeEnemies[0] = false;
        SceneManager.LoadScene(1);
    }

    IEnumerator activateFade(bool n)
    {
        yield return new WaitForSeconds(1.1f);
        fadePanel.SetActive(n);
    }
}
