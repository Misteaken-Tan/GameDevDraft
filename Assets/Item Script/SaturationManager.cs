using UnityEngine;
using UnityEngine.UI; // Required for the Slider
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaturationManager : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    [Header("Color Settings")]
    private float targetSaturation = -100f;
    public float increasePerOrb = 25f;

    [Header("Progress Settings")]
    public Slider progressBar; // Drag your UI Slider here
    public int totalOrbsInLevel = 4;
    private int currentOrbs = 0;
    public float maxSaturation = 0f;

    [Header("Portal Settings")]
    public GameObject portal; // Drag your Portal object here
    public GameObject warningText; // Optional: A UI Text object that says "Get more orbs!"
    public Portal portalScript;

    [Header("Rainbow Settings")]
    public Image fillImage;

    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
            colorAdjustments.saturation.value = -100f;

        // Initialize UI
        if (progressBar != null)
        {
            progressBar.maxValue = totalOrbsInLevel;
            progressBar.value = 0;
        }

        // Make sure the portal starts locked
        if (portalScript != null) portalScript.isLocked = true;
        if (warningText != null) warningText.SetActive(false);
    }

    void Update()
    {
        // Smoothly adjust world color
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = Mathf.MoveTowards(
                colorAdjustments.saturation.value, targetSaturation, Time.deltaTime * 50f);
        }
    }

    public void AddColor()
    {
        currentOrbs++;

        // DEBUG LINE: This will show in your Unity Console (bottom left)
        Debug.Log("<color=cyan>Orb Collected!</color> Current Count: " + currentOrbs + " / " + totalOrbsInLevel);

        float progress = (float)currentOrbs / totalOrbsInLevel;
        targetSaturation = Mathf.Lerp(-100f, maxSaturation, progress);


        if (progressBar != null) progressBar.value = currentOrbs;

        SoundManager.instance.PlaySFX(SoundManager.instance.pickupSound);

        // --- RAINBOW LOGIC ---
        if (fillImage != null)
        {
            fillImage.color = Color.HSVToRGB(progress * 0.8f, 0.8f, 1f);
        }

        if (currentOrbs >= totalOrbsInLevel)
        {
            Debug.Log("<color=green>SUCCESS:</color> All orbs collected! Activating Portal...");
            ActivatePortal();
        }
    }

    void ActivatePortal()
    {
        if (portalScript != null)
        {
            portalScript.UnlockPortal();

            // Stop the Level 3 Timer
            LevelObjectiveManager objective = Object.FindFirstObjectByType<LevelObjectiveManager>();
            if (objective != null)
            {
                objective.WinLevel();
            }

            Debug.Log("Portal unlocked and timer stopped!");
        }
    }

    public void ShowWarning()
    {
        if (currentOrbs < totalOrbsInLevel)
        {
            StopAllCoroutines();
            StartCoroutine(FlashWarning());
        }
    }

    private System.Collections.IEnumerator FlashWarning()
    {
        if (warningText != null)
        {
            warningText.SetActive(true);
            yield return new WaitForSeconds(2f);
            warningText.SetActive(false);
        }
    }



}