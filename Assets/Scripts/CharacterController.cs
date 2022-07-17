using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float movementSpeed;

    public GameManager gameManager;

    public Sprite right;
    public Sprite left;
    public Sprite front;
    public Sprite back;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!GameManager.gamePaused)
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

            Vector2 force = new Vector2(horizontal, vertical);

            rb.AddForce(force, ForceMode2D.Force);

            animator.SetBool("Right", horizontal > 0);
            animator.SetBool("Left", horizontal < 0);
            animator.SetBool("Back", vertical > 0);
            animator.SetBool("Front", vertical < 0);
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
