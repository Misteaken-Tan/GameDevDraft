using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasScript : MonoBehaviour
{


    public void Resume()
    {
       GameObject MainUI = GameObject.FindGameObjectWithTag("MainUI");
       UIMManager manager = MainUI.GetComponent<UIMManager>();
       manager.togglePaused();
    }

    public void quit()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
