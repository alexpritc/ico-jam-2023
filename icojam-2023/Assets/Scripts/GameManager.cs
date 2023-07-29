using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
