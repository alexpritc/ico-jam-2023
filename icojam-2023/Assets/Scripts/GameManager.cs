using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    CUTSCENE, SELECTION
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Dialogue startDialogue;

    const string playerName = "Player";
    const string witnessName = "Number 7";

    public Image bg;
    public Button left;
    public Button right;

    public GameState gameState;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        bg.enabled = true;
        gameState = GameState.CUTSCENE;
    }

    private void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(startDialogue, true);
    }

    public void EndCutScene()
    {
        bg.enabled = false;
        //left.interactable = true;
        right.interactable = true;
    }
}
