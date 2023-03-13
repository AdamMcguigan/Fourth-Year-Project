using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class skeletonScript : MonoBehaviour
{
    private GameObject Player;
    Rigidbody rb;
    private float withinRange = 2;
    private float movementSpeed = 3.0f;

    bool moveTowards = false;
    bool inRadius = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the player is inside of a certain radius to the skeleton.
        if (Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) < withinRange && inRadius == false)
        {
            Debug.Log("Player is within the range to do something");
            moveTowards = true;
        }

        if(moveTowards == true)
        {
            transform.LookAt(Player.transform.position);
            Vector3 velocity = (Player.transform.position - transform.position).normalized * movementSpeed;
            rb.velocity = velocity;
        }
        //This will cause some stuttering as we are still inside the radius - should be able to resolve this when i use the b Tree.
        if (Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) < 3.0f)
        {
            moveTowards = false;
            inRadius = true;
            rb.velocity = Vector3.zero;
            Debug.Log("ATTACKING PLAYER BRUH");


        }
        else if(Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) > withinRange)
        {
            inRadius = false;
        }

    }

    private void HeadTowardsPlayer()
    {
        //transform.LookAt(Player.transform.position);
        //Vector3 velocity = (Player.transform.position - transform.position).normalized * movementSpeed;
        //rb.velocity = velocity;
        //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) < 2.0f)
        {
            rb.velocity = Vector3.zero;
            Debug.Log("ATTACKING PLAYER BRUH");
            

        }
    }

    //Drawing gizmo so i can see the range more easily inside scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, withinRange);
    }
}