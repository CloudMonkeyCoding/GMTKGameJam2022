using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public CombatManager.Actions currentAction;
    public CombatManager combatManager;
    public DiceRoller diceRoller;

    public void RecklessAttack()
    {
        currentAction = CombatManager.Actions.RecklessAttack;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
    }

    public void Attack()
    {
        currentAction = CombatManager.Actions.Attack;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
    }

    public void Block()
    {
        currentAction = CombatManager.Actions.Block;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
    }

    public void Potion()
    {
        currentAction = CombatManager.Actions.Potion;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
    }
}