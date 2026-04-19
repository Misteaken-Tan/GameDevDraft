using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that touched the square is the Player
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Die();
        }
    }
}