using UnityEngine;

public class Orb : MonoBehaviour, IItem
{
    public void Collector()
    {
        Destroy(gameObject);
    }
}
