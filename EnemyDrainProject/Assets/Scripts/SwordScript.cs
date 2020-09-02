using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private float spawnTime = .2f; // controls how long the sword last for

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;

        // the sword attack dissapears once the time runs out
        if (spawnTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
