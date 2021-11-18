using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChooseAzure()
    {
        SceneManager.LoadScene("LoadingScene");
        

    }

    public void ChooseGlobal()
    {
        SceneManager.LoadScene("LoadingScene");
        
    }

    public void ChooseMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }
}
