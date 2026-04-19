using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool isLocked = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLocked)
            {
                // Move to next level or finish game
                Debug.Log("Entering Portal...");
                SceneManager.LoadScene(3, LoadSceneMode.Single);
            }
            else
            {
                // TRIGGER THE WARNING YOU MADE
                SaturationManager satManager = Object.FindFirstObjectByType<SaturationManager>();
                if (satManager != null)
                {
                    satManager.ShowWarning();
                    Debug.Log("Portal is locked! Warning displayed.");
                }
            }
        }
    }

    public void UnlockPortal()
    {
        isLocked = false;
        // Optional: Change portal color or play a sound here
    }
}