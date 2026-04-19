using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Vector3 checkpoint;

    void Start()
    {
        checkpoint = transform.position;
    }

    public void Die()
    {
        // Try to find the Level 3 Manager
        LevelObjectiveManager objective = Object.FindFirstObjectByType<LevelObjectiveManager>();
        SoundManager.instance.PlaySFX(SoundManager.instance.dieSound);
        // Only penalize if the script exists in this scene
        if (objective != null)
        {
            objective.ApplyDeathPenalty();
        }

        // Respawn logic
        transform.position = checkpoint;

        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    public void UpdateCheckpoint(Vector3 newPos)
    {
        checkpoint = newPos;
    }
}