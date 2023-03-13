using UnityEngine;

[DisallowMultipleComponent]
public class Enemy2 : MonoBehaviour
{
    public Emovement Movement;
    public Player player;

    private void Awake()
    {
        Movement = GetComponent<Emovement>();
    }

    private void Start()
    {
        Movement.GoTowardsPlayer(player);
    }
}