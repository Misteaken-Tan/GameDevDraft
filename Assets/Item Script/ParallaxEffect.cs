using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;

    [Tooltip("0 = Stays with Cam, 1 = Stays Still, >1 = Moves Opposite")]
    public float parallaxFactor;

    void Start()
    {
        startPos = transform.position.x;

        // This calculates the width of your sprite for infinite looping
        if (GetComponent<SpriteRenderer>() != null)
        {
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Change 'void Update' to 'void LateUpdate'
    void LateUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxFactor));
        float dist = (cam.transform.position.x * parallaxFactor);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}