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

    public Animator fadePanel;

    public TextMeshPro GKHP;
    public TextMeshPro GHP;

    private void Start() 
    {
        PlayerTurn();
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
        DeactivatePlayerButtons();
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
    }

    void CheckEnemies()
    {
        int alive = 0;
        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<EnemyActions>().hp <= 0)
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
        fadePanel.SetBool("ending", true);
        SceneHandler.activeEnemies[0] = false;
        SceneManager.LoadScene(1);
    }
}
