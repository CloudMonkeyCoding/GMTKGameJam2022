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
        combatManager.StartTimerForNext();
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Reckless Attack");
        combatManager.chosenEnemy.GetComponent<Animator>().SetTrigger("Hurt");
    }

    public void Attack()
    {
        currentAction = CombatManager.Actions.Attack;
        combatManager.StartTimerForNext();
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Attack");
        combatManager.chosenEnemy.GetComponent<Animator>().SetTrigger("Hurt");
    }

    public void Block()
    {
        currentAction = CombatManager.Actions.Block;
        GKAnimator.SetBool("Block", true);
        PlayerStats.sheildDurability -= 1;
        combatManager.playerButtons[2].interactable = false;
    }

    public void Potion()
    {
        currentAction = CombatManager.Actions.Potion;
        combatManager.StartTimerForNext();
        diceRoller.StartRoll();
        combatManager.DeactivatePlayerButtons();
        GKAnimator.SetTrigger("Heal");
    }
}