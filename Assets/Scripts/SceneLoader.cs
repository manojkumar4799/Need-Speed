using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
     
    public void Quit()
    {
        Application.Quit();
    }
}
