using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float foeSpeed;
    public GameObject Player; // reference to the player
    private Transform target; // used to find the player

    public GameObject soulSpawn; // used to spwan the enemy's soul upon death
    public GameObject foeSoul; // the form of the enemy that the player can collect

    // Start is called before the first frame update
    void Start()
    {
        // sets the target to the player so the enemy knows who to track
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // the enemy will move towards the player
        transform.position = Vector2.MoveTowards(transform.position, target.position, foeSpeed * Time.deltaTime);
    }

    // the parts of the enemy drain mechanic on the enemy's side can be found here
    void OnCollisionEnter2D(Collision2D other)
    {
        // if the enemy is hit by the sword
        if (other.gameObject.tag == "Sword")
        {
            this.gameObject.SetActive(false); // remove the enemy from the scene
            // and add his soul in his place
            Instantiate(foeSoul, soulSpawn.transform.position, foeSoul.transform.rotation);
        }
    }
}
