using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaturationManager : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    private float targetSaturation = -100f;
    public float increasePerOrb = 25f; // 4 orbs = full color

    void Start()
    {
        // Get the ColorAdjustments component from the volume profile
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f;
        }
    }

    void Update()
    {
        if (colorAdjustments != null)
        {
            // Smoothly slide toward the target value for a "magical" feel
            colorAdjustments.saturation.value = Mathf.MoveTowards(
                colorAdjustments.saturation.value,
                targetSaturation,
                Time.deltaTime * 50f
            );
        }
    }

    public void AddColor()
    {
        targetSaturation = Mathf.Clamp(targetSaturation + increasePerOrb, -100f, 0f);
    }
}