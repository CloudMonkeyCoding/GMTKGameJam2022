using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public CombatManager.Actions currentAction;
    public DiceRoller diceRoller;

    public void RecklessAttack()
    {
        currentAction = CombatManager.Actions.RecklessAttack;
        diceRoller.StartRoll();
    }

    public void Attack()
    {
        currentAction = CombatManager.Actions.Attack;
        diceRoller.StartRoll();
    }

    public void Block()
    {
        currentAction = CombatManager.Actions.Block;
        diceRoller.StartRoll();
    }

    public void Potion()
    {
        currentAction = CombatManager.Actions.Potion;
        diceRoller.StartRoll();
    }
}