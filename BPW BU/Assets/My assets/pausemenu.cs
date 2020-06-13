using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
  public static bool paused = false;
  public GameObject pauseMenuUI;

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Escape)){
         if (paused){
             Resume();
         } else{
             Pause();
         }

     }   
    }
    void Pause () {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        }
    
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

    }

    public void QuitGame() {
        Debug.Log("quit");
        Application.Quit();

    }
}
