using UnityEngine;

public class Orb : MonoBehaviour, IItem
{
    public void Collector()
    {
        // Find the manager and tell it to bump up the saturation
        SaturationManager manager = Object.FindFirstObjectByType<SaturationManager>();

        if (manager != null)
        {
            manager.AddColor();
        }

        // Destroy the orb
        Destroy(gameObject);
    }
}