using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Dialogue startDialogue;

    const string playerName = "Player";
    const string witnessName = "Number 7";

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(startDialogue);
    }
}
