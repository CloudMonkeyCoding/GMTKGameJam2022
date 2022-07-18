using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public CombatManager.Actions currentAction;
    public CombatManager combatManager;
    public DiceRoller diceRoller;
    public Animator GKAnimator;

    public void RecklessAttack()
    {
        currentAction = CombatManager.Actions.RecklessAttack;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Reckless Attack");
    }

    public void Attack()
    {
        currentAction = CombatManager.Actions.Attack;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Attack");
    }

    public void Block()
    {
        currentAction = CombatManager.Actions.Block;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Block");
    }

    public void Potion()
    {
        currentAction = CombatManager.Actions.Potion;
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Heal");
    }
}