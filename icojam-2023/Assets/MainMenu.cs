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
        SceneManager.LoadSceneAsync(1);
    }
}
