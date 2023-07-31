using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.instance.Play("Sting");
        SceneManager.LoadSceneAsync(1);
    }

    public void Options()
    {
        AudioManager.instance.Play("Click");
    }

    public void Credits()
    {
        AudioManager.instance.Play("Click");
    }

    public void Exit()
    {
#if UNITY_STANDALONE_WIN
        AudioManager.instance.Play("Click");
        Application.Quit();
#endif
    }

    public void Click()
    {
        AudioManager.instance.Play("Click");
    }
}
