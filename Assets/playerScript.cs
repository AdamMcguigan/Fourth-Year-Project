using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class playerScript : MonoBehaviour
{
    private CharacterController Controller;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float HistoricalPositionDuration = 1f;
    [SerializeField]
    [Range(0.001f, 1f)]
    private float HistoricalPositionInterval = 0.1f;

    public float m_health;
    float m_maxHealth = 100;
    [SerializeField]
    private ProgressBar healthBar;

    [Header("Death Related Vars")]
    public GameObject panel;
    public TextMeshProUGUI deathText;
    public GameObject deathBox;
    private bool playerDead = false;
    private float targetTime = 5.0f;

    [Header("Win Vars")]
    public bool keyCollected = false;
    public GameObject panel2;
    public TextMeshProUGUI winText;
    public GameObject Bus;
    private float winTime = 10.0f;


    public Vector3 AverageVelocity
    {
        get
        {
            Vector3 average = Vector3.zero;
            foreach (Vector3 velocity in HistoricalVelocities)
            {
                average += velocity;
            }
            average.y = 0;

            return average / HistoricalVelocities.Count;
        }
    }

    private Queue<Vector3> HistoricalVelocities;
    private float LastPositionTime;
    private int MaxQueueSize;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        MaxQueueSize = Mathf.CeilToInt(1f / HistoricalPositionInterval * HistoricalPositionDuration);
        HistoricalVelocities = new Queue<Vector3>(MaxQueueSize);
    }

    private void Update()
    {
        if (LastPositionTime + HistoricalPositionInterval <= Time.time)
        {
            if (HistoricalVelocities.Count == MaxQueueSize)
            {
                HistoricalVelocities.Dequeue();
            }

            HistoricalVelocities.Enqueue(Controller.velocity);
            LastPositionTime = Time.time;
        }

        if(playerDead == true)
        {
            gameObject.transform.position = deathBox.gameObject.transform.position;
            panel.gameObject.SetActive(true);
            deathText.gameObject.SetActive(true);
            targetTime -= Time.deltaTime;
            if(targetTime <= 0.0f)
            {
                SceneManager.LoadScene("Test Scene");
            }
        }

        if(keyCollected == true)
        {
            gameObject.transform.position = Bus.gameObject.transform.position;
            panel2.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            winTime -= Time.deltaTime;

            if (winTime <= 0.0f)
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

 

    // Start is called before the first frame update
    void Start()
    {
        m_health = m_maxHealth;
        panel.gameObject.SetActive(false);
        deathText.gameObject.SetActive(false);
        playerDead = false;
    }

    // Update is called once per frame

    public void onTakeDamage(int t_damage)
    {
        m_health -= t_damage;
        healthBar.SetProgress(m_health / m_maxHealth, 3);

        if (m_health <= 0)
        {
            playerDead = true;
            Destroy(healthBar.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onTakeDamage(10);
        }

        if(collision.gameObject.CompareTag("Health"))
        {
            Debug.Log("Colliding");
            m_health += 10;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Bus")
        {
            Debug.Log("player on bus");
            keyCollected = true;
        }

        if (collision.gameObject.tag == "Keys")
        {
            Debug.Log("player picked up keys");
        }
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        healthBar.transform.SetParent(canvas.transform);
        if (healthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }
}
