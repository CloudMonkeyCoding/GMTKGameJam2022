using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] sides;
    public SpriteRenderer sr;

    public bool rolling;
    public int activeSide = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        sr.sprite = sides[activeSide];

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartRoll();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            StopRoll();
        }
    }

    public void StartRoll()
    {
        StartCoroutine(randomRoll());
    }

    void StopRoll()
    {
        StopAllCoroutines();

    }

    IEnumerator randomRoll()
    {
        while(true)
        {
            int newSide = Random.Range(0,6);
            while(newSide == activeSide)
                newSide = Random.Range(0,6);
            activeSide = newSide;
            yield return new WaitForSeconds(.1f);
        }
    }
}
