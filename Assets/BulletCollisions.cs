using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "bus")
        {
            Player.GetComponent<playerScript>().keyCollected = true;
        }

    }
}
