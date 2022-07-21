using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] sides;
    SpriteRenderer sr;

    public bool rolling;
    public int activeSide = 0;
    public int rollTime;

    public CombatManager combatManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    public void StartRoll()
    {
        StartCoroutine(randomRoll());
    }

    IEnumerator randomRoll()
    {

        for (int i = 0; i < rollTime; i++)
        {
            int newSide = Random.Range(0,6);
            while(newSide == activeSide)
                newSide = Random.Range(0,6);
            activeSide = newSide;
            sr.sprite = sides[activeSide];
            yield return new WaitForSeconds(.1f);
        }
        combatManager.Result(activeSide + 1);
    }
}
