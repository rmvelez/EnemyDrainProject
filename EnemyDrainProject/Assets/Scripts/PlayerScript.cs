using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    public float speed;
    private Rigidbody2D myRB;

    public GameObject groundCheck = null;
    public bool grounded;
    public float jumpHeight;
    private float jump;

    public GameObject swordSpawn; // empty game object that is used to generate the sword
    public GameObject playerSword; // the player's sword attack
    private float timeBtwStabs; // these last two vars are used to control how often the player can attack
    public float startTimeBtwStabs;

    public bool facingRight; // checks to see which direction the player is facing

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); // gets the rigidbody component on the player
        facingRight = true; // the player is facing right
    }

    // Update is called once per frame
    void Update()
    {
        // lets the player attack with the sword once he is able to
        if(Input.GetKey(KeyCode.O) && timeBtwStabs <= 0)
        {
            Instantiate(playerSword, swordSpawn.transform.position, playerSword.transform.rotation);
            timeBtwStabs = startTimeBtwStabs;
        }
        // otherwise, he won't be able to attack
        else
        {
            timeBtwStabs -= Time.deltaTime;
        }
    }

    // calls on the movement function every other frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");

        PlayerMove();
    }

    // tis function allows the player to move and jump
    // it also flips the player and indicates which direction he or she is facing
    void PlayerMove()
    {
        float currentYVal = myRB.velocity.y; // shortcut for the y value of the velocity for the player
        Vector2 playerScale = transform.localScale; // new var that tracks which direction the player is facing

        if (horizontal == 1)
        {
            myRB.velocity = new Vector2(horizontal * speed, currentYVal);
            playerScale.x = 1;
            facingRight = true;
        }
        else if (horizontal == -1)
        {
            myRB.velocity = new Vector2(horizontal * speed, currentYVal);
            playerScale.x = -1;
            facingRight = false;
        }
        else
        {
            myRB.velocity = new Vector2(0, currentYVal);
        }

        transform.localScale = playerScale; // sets the direction to the new scale

        if(Physics2D.Linecast(transform.position, groundCheck.transform.position))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded == true)
        {
            myRB.AddForce(new Vector2(0, jump * jumpHeight));
        }
    }

    // parts if the enemy drain mechanic on the player's end can be found here
    void OnCollisionEnter2D(Collision2D other)
    {
        // the player gets attacked when the enemy touches him or her
        if (other.gameObject.tag == "Enemy")
        {
            if (facingRight == true)
            {
                transform.position = new Vector2(transform.position.x - 3, transform.position.y);
            }
            else if (facingRight == false)
            {
                transform.position = new Vector2(transform.position.x + 3, transform.position.y);
            }
        }

        // when the player grabs the enemy's soul
        else if (other.gameObject.tag == "Soul")
        {
            Destroy(other.gameObject); // remove it from the scene
        }
    }
}
