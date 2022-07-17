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

    public Sprite right;
    public Sprite left;
    public Sprite front;
    public Sprite back;

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

            if(horizontal > 0)
            {
                sr.sprite = right;
            }
            else if(horizontal < 0)
            {
                sr.sprite = left;
            }
            else if(vertical > 0)
            {
                sr.sprite = back;
            }
            else
            {
                sr.sprite = front;
            }
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
