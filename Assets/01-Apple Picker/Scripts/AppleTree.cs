using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab;

    //Speed at which AppleTree turns around
    public float speed = 1f;

    //Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    //Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    //Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement

        Vector3 pos = transform.position;

        pos.x += speed * Time.deltaTime;

        transform.position = pos;

        // Changing Direction

        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right

        } else if ( pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move Left
        }
    
    }
    void FixedUpdate()
    {
        //Chaning Direction Randomly
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change direction
        }
    }

}
