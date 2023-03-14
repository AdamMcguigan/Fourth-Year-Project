using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCube : MonoBehaviour
{
    public GameObject smallCube; // the prefab for the small cube that the large cube will break into
    public int numSmallCubes = 15; // the number of small cubes to create when the large cube is broken
    public float explosionForce = 20; // the force with which to explode the small cubes
    public float explosionRadius = 1f; // the radius of the explosion

    // A variable to track the current health of the large cube
    private int health;

    void Start()
    {
        // Set the initial health of the large cube
        health = 100;
    }

    void Update()
    {
        // Check if the large cube's health has reached 0
        if (health <= 0)
        {
            // Create the small cubes and apply the explosion force to them
            for (int i = 0; i < numSmallCubes; i++)
            {
                GameObject small = Instantiate(smallCube, transform.position, Quaternion.identity);
                small.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Destroy the large cube
            Destroy(gameObject);
        }
    }

    // A function to reduce the large cube's health when it takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("cube") || collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }
    }
}
