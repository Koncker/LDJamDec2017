using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerSpeed = 100;
    private bool facingRight = false;
    public int playerJumpForce = 1000;
    private float moveX;
    public bool grounded;

    public float PlayerHealth = 100;
    public float PlayerFat = 0;
    bool damaged;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Image damageImage;
    public Slider healthSlider;

    private Animator anim;
    private Rigidbody2D playerrb;
    private bool isJumping = false;

    public Transform shotSpawn;
    public float fireRate;
    public GameObject projectile;
    private float shot_damage;
    private float nextFire;
    public AudioSource jump;
    public AudioSource hurt;
    public AudioSource pickup;

    public GameObject gameOver;

    public Text scoreText;
    private int score;

    private int Fat;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerrb = GetComponent<Rigidbody2D>();
        healthSlider.value = PlayerHealth;
        score = 0;
        UpdateScore();
    }

    void Update()
    {
        PlayerMove();

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        { 
            nextFire = Time.time + fireRate;
            Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
        }
        CheckFat();

        if (PlayerHealth <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }

 /** /       if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

         damaged = false;
 /**/
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckFat()
    {
        if (PlayerFat < 50)
        {
            anim.SetInteger("FatValue", 0);
            playerSpeed = 100;
        }
        else if (PlayerFat >= 50 && PlayerFat <100)
        {
            anim.SetInteger("FatValue", 1);
            playerSpeed = 80;
        }
        else if (PlayerFat >= 100 && PlayerFat <150)
        {
            anim.SetInteger("FatValue", 2);
            playerSpeed = 60;
        }
        else if (PlayerFat >= 150 && PlayerFat <200)
        {
            anim.SetInteger("FatValue", 3);
            playerSpeed = 40;
        }
        else if (PlayerFat >= 200 && PlayerFat <300)
        {
            anim.SetInteger("FatValue", 4);
            playerSpeed = 20;
        }
        else if (PlayerFat >= 250)
        {
            anim.SetBool("isDead", true);
            anim.SetInteger("FatValue", 5);
            gameOver.SetActive(true);
            fireRate = 10000;
            playerSpeed = 0;
            
        }
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis ("Horizontal");
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            Jump();
            isJumping = true;
            Debug.Log("Is Jumping");
        }
        //ANIMATIONS
        if (Mathf.Abs(moveX) > 0.0f)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        if (Mathf.Abs(playerrb.velocity.y) > 0.1f)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //PHYSICS
        playerrb.velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //JUMPING CODE
        playerrb.AddForce(Vector2.up * playerJumpForce);
        jump.Play();
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player has collided with " + col);
        if (col.gameObject.tag == "Floor")
        {
            isJumping = false;
        }

        if (col.gameObject.tag == "Burger")
        {
            if (PlayerHealth < 100)
            {
                PlayerHealth += 50f;
            }
            PlayerFat += 50f;
            Object.Destroy(col.gameObject);
            isJumping = false;
            pickup.Play();
            healthSlider.value = PlayerHealth;
        }

        if (col.gameObject.tag == "Sausage")
        {
            if (PlayerHealth < 100)
            {
                PlayerHealth += 30f;
            }
            PlayerFat += 50f;
            Object.Destroy(col.gameObject);
            isJumping = false;
            pickup.Play();
            healthSlider.value = PlayerHealth;
        }

        if (col.gameObject.tag == "Hangry")
        {
            PlayerHealth -= 20f;
            PlayerFat -= 5f;
            isJumping = false;
            hurt.Play();
            damaged = true;
            healthSlider.value = PlayerHealth;
        }
        
        if (col.gameObject.tag == "Creepy")
        {
            PlayerHealth -= 25f;
            PlayerFat -= 10f;
            isJumping = false;
            hurt.Play();
            damaged = true;
            healthSlider.value = PlayerHealth;
        }
    }
}
