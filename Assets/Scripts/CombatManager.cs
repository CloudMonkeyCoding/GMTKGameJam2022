using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool playersTurn;
    public enum Actions{Attack, RecklessAttack, Block, Potion}
    public PlayerActions playerActions;
    
    public GameObject[] enemies;
    public GameObject chosenEnemy;

    private void Start() 
    {
        Debug.Log(SceneHandler.activeEnemies[0]);
    }

    public void PlayerTurn()
    {

    }

    public void EnemyTurn()
    {

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

        }
    }
}
