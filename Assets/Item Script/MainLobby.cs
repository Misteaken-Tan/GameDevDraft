using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    
    public void Level()
    {
        Debug.Log("Level Scene");
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void Menu()
    {
        Debug.Log("Menu Scene");
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void Collection()
    {
        Debug.Log("Collection Scene");
        SceneManager.LoadScene(4,LoadSceneMode.Single);
    }

    public void StartUI()
    {
        Debug.Log("Start UI");
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void SettingsUI()
    {
        Debug.Log("Settings UI");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Backbtn()
    {
               Debug.Log("Back Button");
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void level1 ()
    {
        Debug.Log("Level 1");
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    public void level2()
    {
        Debug.Log("Level 2");
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

    public void level3()
    {
        Debug.Log("Level 3");
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }



    public void Exit()
    {
        Debug.Log("The game has closed!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
