using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Menu");
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("Sting");
        SceneManager.LoadSceneAsync(1);
    }

    public void Options()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }

    public void Click()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
