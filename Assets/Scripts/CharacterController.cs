using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float movementSpeed;
    public float regenerationRate;

    public GameManager gameManager;

    private float timeBeforeRegenerate;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();  

        timeBeforeRegenerate = regenerationRate;  
    }

    void Update()
    {
        if(!GameManager.gamePaused)
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

            Vector2 force = new Vector2(horizontal, vertical);

            rb.AddForce(force, ForceMode2D.Force);

            sr.flipX = Input.mousePosition.x >= Screen.width / 2;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Enemy"))
            return;
        
        SceneHandler.lastPlayerLocation = transform;
        SceneManager.LoadScene(collision.gameObject.GetComponent<Enemy>().sceneIndex); 
    }
}