using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_health;
    float m_maxHealth = 100;
    [SerializeField]
    private ProgressBar healthBar;
    //public GameObject healthP;

    // Start is called before the first frame update
    void Start()
    {
        m_health = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTakeDamage(int t_damage)
    {
        m_health -= t_damage;
        healthBar.SetProgress(m_health / m_maxHealth, 3);

        if(m_health <= 0)
        {
            //Instantiate(healthP, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(healthBar.gameObject);

           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            onTakeDamage(20);
        }
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        healthBar.transform.SetParent(canvas.transform);
        if(healthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }
}
