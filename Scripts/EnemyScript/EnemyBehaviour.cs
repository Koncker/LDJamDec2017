using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float myHealth;
    public int monsterSpeed;
    private bool facingright;
    private float move;
	private Vector2 moveDirection;
    public PlayerController Player;
    private bool facingRight = false;
    public float moveX;
    public int scoreValue;

    private PlayerController playerController;

    void Start()
	{
		moveDirection = Vector2.left;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision1)
    {
        Debug.Log("Player has collided with " + collision1);
        if (collision1.gameObject.tag == "Projectile")
        {
            myHealth -= 50f;
            Object.Destroy(collision1.gameObject);
        }
		else if (collision1.gameObject.tag == "Border")
		{
			// If enemy collides with any "border" reverse its direction
			moveDirection = -moveDirection;
		}
    }

    private void Update()
    {
       if (myHealth <= 0)
        {
            Player.AddScore(scoreValue);
            Destroy(gameObject);
        }



        Move(monsterSpeed);
    }

    void FlipEnemy()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Move(int speed)
    {
        Vector2 currentPos = transform.position;
        Vector2 moveDirection = Player.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        Vector2 newPos = currentPos + (moveDirection * speed * Time.deltaTime);
        moveX = -moveDirection.x;

        transform.position = newPos;

        //ENEMY DIRECTION
        if (moveX < 0.0f && facingRight == false)
        {
            FlipEnemy();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipEnemy();
        }
    }
}
