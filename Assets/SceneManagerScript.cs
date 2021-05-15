using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public Animator transition = null;
    public float transitionTime = 1f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exitgame();
        }
        
    }
    /*public void LoadMainScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }*/

    public void Exitgame()
    {
        Application.Quit();
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void StartGame()
      {
        SceneManager.LoadScene("Letstryagain");
      }

    /*IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }*/
}
