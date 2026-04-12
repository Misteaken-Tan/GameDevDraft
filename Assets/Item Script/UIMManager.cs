using UnityEngine;

public class UIMManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    GameObject tempCanvas;
    public bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            togglePaused();
        }

    }
    public void togglePaused()
    {
        if(!isPaused)
        {
            Debug.Log("Paused");
            Time.timeScale = 0;
            isPaused = true;
            tempCanvas = Instantiate(pauseCanvas);
        }
        else
        {
            Debug.Log("Resumed");
            Time.timeScale = 1;
            isPaused = false;
            Destroy(tempCanvas);
        }
    }
}
