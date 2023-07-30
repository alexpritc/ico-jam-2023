using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    CUTSCENE, SELECTION, CONFIRM, MENU
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Dialogue startDialogue;

    string playerName = "Detective Donut";
    string witnessName = "Ms. Seven";

    public Image bg;
    public Button left;
    public Button right;

    public GameState gameState;

    private DialogueManager dialogueManager;

    private Dialogue[] temp;

    bool hasPlayerUsedArrows = false;

    WaitForSeconds wait = new WaitForSeconds(3f);

    private int eliminateThisSuspect;
    private int currentSuspect;

    public GameObject confirmationPanel;
    public GameObject finalConfirmationPanel;

    public GameObject enterRoomPanel;
    public GameObject enterRoomButton;

    public bool finalThree;

    public GameObject seven;

    public GameObject clueGO;
    public TextMeshProUGUI clue;

    public int lives = 3;

    public GameObject threeBadges;
    public GameObject twoBadges;
    public GameObject oneBadges;

    public GameObject killerFound;
    public GameObject badgesLost;

    public Sprite brokenBadge;

    public Image badgeThree;
    public Image badgeTwo;
    public Image badgeOne;

    public GameObject badgesThreeButton;
    public GameObject badgesTwoButton;
    public GameObject badgesOneButton;

    public GameObject gameOverWonButton;
    public GameObject gameOverLoseButton;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);

        dialogueManager = FindObjectOfType<DialogueManager>();

        clueGO.SetActive(false);

        //bg.enabled = true;
        enterRoomPanel.SetActive(true);
        gameState = GameState.CUTSCENE;
    }

    private void Start()
    {
        dialogueManager.isIntro = true;
        dialogueManager.StartNextDialogue(startDialogue);
        right.onClick.AddListener(FirstClue);
    }

    private void Update()
    {
        if (gameState== GameState.SELECTION) {
            if (!clueGO.activeInHierarchy)
            {
                clueGO.SetActive(true);
                AudioManager.instance.Play("Sting");
            }
        }
        else { clueGO.SetActive(false); }
    }

    public void EndFirstScene()
    {
        enterRoomButton.SetActive(true);
    }

    public void EndBadgesThree()
    {
        badgesThreeButton.SetActive(true);   
    }

    public void EndBadgesTwo()
    {
        badgesTwoButton.SetActive(true);
    }

    public void EndBadgesOne()
    {
        badgesOneButton.SetActive(true);
    }

    public void EndGameOverWin()
    {
        gameOverWonButton.SetActive(true);
    }

    public void EndGameOverLose()
    {
        gameOverLoseButton.SetActive(true);
    }

    public void Main()
    {
        enterRoomPanel.SetActive(false);
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[4];

        temp[0] = NewDialogue(playerName, new string[] { "…Thanks for coming, Mrs N. We know this can’t be easy." });

        temp[1] = NewDialogue(witnessName, new string[] { "Please, it’s Ms. Seven now. And it's my pleasure. It'll feel good to finally finger the bastard that killed my husband." });

        temp[2] = NewDialogue(playerName, new string[] { "All right. Let’s take a look at these sorry shapes…"});

        temp[3] = NewDialogue(" ", new string[] {"[Use the arrow buttons to scroll through the lineup.]" });

        dialogueManager.isUseArrowsClue = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Wait for player to use buttons
    }

    public void EnableFirstButton()
    {
        right.interactable = true;
        StopAllCoroutines();
    }

    void FirstClue()
    {
        if (hasPlayerUsedArrows) {
            return;
        }

        hasPlayerUsedArrows = true;

        FirstClueDialogue();
    }

    void FirstClueDialogue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[3];

        temp[0] = NewDialogue(playerName, new string[] { "Now Ms Seven… I need you to think back to that night and remember.",
            "Is there anything you can tell us about the person that killed your husband?" });

        temp[1] = NewDialogue(witnessName, new string[] { "Well… He was a shape, for starters. So I don’t know what that number is doing there!" });

        temp[2] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 4
        eliminateThisSuspect = 4;

        //clueGO.SetActive(true);
        clue.text = "They were a shape, not a number.";
    }

    public void EnableSelectGameState()
    {
        gameState = GameState.SELECTION;
    }

    Dialogue NewDialogue(string name, string[] dialogue)
    {
        Dialogue temp = new Dialogue();

        temp.name = name;
        temp.sentences = dialogue;

        return temp;
    }

    public void ShowPopUp(int suspectNumber)
    {
        currentSuspect = suspectNumber;
        gameState = GameState.CONFIRM;
        if (finalThree)
        {
            finalConfirmationPanel.SetActive(true);
        }
        else
        {
            confirmationPanel.SetActive(true);
        }
    }

    public void HidePopUp()
    {
        gameState = GameState.SELECTION;
        if (finalThree)
        {
            finalConfirmationPanel.SetActive(false);
        }
        else
        {
            confirmationPanel.SetActive(false);
        }
    }

    public void CheckSuspect()
    {
        if (finalThree)
        {
            HidePopUp();

            if (currentSuspect == 2)
            {
                AccuseTrapezium();
            }
            else if (currentSuspect == -1)
            {
                AccuseSeven();
            }
            else
            {
                ShowBadges();
            }
        }
        else
        {
            if (currentSuspect == eliminateThisSuspect)
            {
                HidePopUp();
                FindObjectOfType<LineUpController>().Remove(currentSuspect);
                // trigger correct dialogue and next conversation

                if (eliminateThisSuspect == 4)
                {
                    SecondClue();
                }
                else if (eliminateThisSuspect == 1)
                {
                    ThirdClue();
                }
                else if (eliminateThisSuspect == 10)
                {
                    FourthClue();
                }
                else if (eliminateThisSuspect == 7)
                {
                    FifthClue();
                }
                else if (eliminateThisSuspect == 3)
                {
                    SixthClue();
                }
                else if (eliminateThisSuspect == 9)
                {
                    SeventhClue();
                }
                else if (eliminateThisSuspect == 5)
                {
                    EighthClue();
                }
            }
            else
            {
                HidePopUp();
                // trigger try again dialogue

                ShowBadges();
                //WrongSuspectMessage();
            }
        }
    }

    void ShowBadges()
    {
        string message;

        if (lives == 3)
        {
            badgeThree.sprite = brokenBadge;
            threeBadges.SetActive(true);
            message = "That's a costly error, Detective. I'll have to take one of your badges for that.";
        }
        else if (lives == 2)
        {
            badgeTwo.sprite = brokenBadge;
            twoBadges.SetActive(true);
            message = "Another mistake, Detective? One more and you're off the force.";
        }
        else
        {
            badgeOne.sprite = brokenBadge;
            oneBadges.SetActive(true);
            message = "That's it. Your career is over. I'm not giving you anymore chances.";
        }

        WrongSuspectMessage(message);

        lives--;
    }

    void WrongSuspectMessage(string message)
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[1];

        temp[0] = NewDialogue("Cpt. Fractal", new string[] { message });

        if (lives == 3)
        {
            dialogueManager.threeBadges = true;
        }
        else if (lives == 2)
        {
            dialogueManager.twoBadges = true;
        }
        else if (lives == 1)
        {
            dialogueManager.oneBadges = true;
        }

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));
    }

    public void BackToGame(GameObject go)
    {
        go.SetActive(false);
        gameState = GameState.SELECTION;
    }

    public void GameOver()
    {
        gameState = GameState.CUTSCENE;

        if (lives <= 0)
        {
            //dialogueManager.gameOverLose = true;
            ShowGameOverScreen(badgesLost);
        }
        else
        {
            dialogueManager.gameOverWon = true;
            ShowGameOverScreen(killerFound);
        }
    }

    void ShowGameOverScreen(GameObject go)
    {
        go.SetActive(true);
    }

    // Circle
    void SecondClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[4];

        temp[0] = NewDialogue("Four", new string[] { "Tee-hee-hee! Hee-hee, hee-hee!" });

        temp[1] = NewDialogue(playerName, new string[] { "Apologies. That guy always gets into these things. He’s a little scamp. Can you tell us anything else?" });

        temp[2] = NewDialogue(witnessName, new string[] { "Well… Something you should know about me… I have some low-level psychic abilities.",
        "And when that awful shape was killing my husband, I just got the overwhelming feeling that the number 3.14159265359 meant *nothing* to them."});

        temp[3] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 1
        eliminateThisSuspect = 1;

        //clueGO.SetActive(true);
        clue.text = "3.14159265359 means nothing to them.";
    }

    
    // Triangle
    void ThirdClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[4];

        temp[0] = NewDialogue("Circle", new string[] { "3.14159265359 means *everything* to me. " });

        temp[1] = NewDialogue(playerName, new string[] { "Excellent Ms Seven, we’ve got it down to just eight suspects. What else can you tell us?" });

        temp[2] = NewDialogue(witnessName, new string[] { "Hmm… Ah! My husband suffered from severe Trigonophobia- but the person who killed him didn’t scare him at all!",
        "That must eliminate someone, right?"});

        temp[3] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 10
        eliminateThisSuspect = 10;

        //clueGO.SetActive(true);
        clue.text = "Trigonophobia is the fear of which shape?";
    }

    // Rhombus
    void FourthClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[6];

        temp[0] = NewDialogue("Triangle", new string[] { "Triangle! T-t-t…Triangle!" });

        temp[1] = NewDialogue(playerName, new string[] { "Damn… That guy was my favourite.", 
            "What about their appearance, Ms Seven? Can you tell us what they looked like?" });

        temp[2] = NewDialogue(witnessName, new string[] { "I’ll try… Hhhh… Nhhhhhh…"});

        temp[3] = NewDialogue(playerName, new string[] { "Um… Are you alright, Ms Seven?" });

        temp[4] = NewDialogue(witnessName, new string[] { "I’m… Thinking… Ah! I remember!", 
            "The shape who killed my husband was vertically symmetrical! Yes!" });

        temp[5] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 7
        eliminateThisSuspect = 7;

        //clueGO.SetActive(true);
        clue.text = "The murderer was vertically symmetrical.";
    }

    // Pentagon
    void FifthClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[4];

        temp[0] = NewDialogue("Rhombus", new string[] { "Oh, I didn’t do it? Cool." });

        temp[1] = NewDialogue(playerName, new string[] { "That’s exactly the kind of information we’re looking for, Ms Seven. Great work."});

        temp[2] = NewDialogue(witnessName, new string[] { "I’m not done yet! They… Were… Also… Even! Yes! They had an even number of sides." });

        temp[3] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 3
        eliminateThisSuspect = 3;

        //clueGO.SetActive(true);
        clue.text = "Any shape with odd sides is not the murderer.";
    }

    
    //SemiCircle
    void SixthClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[7];

        temp[0] = NewDialogue("Pentagon", new string[] { "Typical. Evencells *seething* at oddchads." });

        temp[1] = NewDialogue(witnessName, new string[] { "What a grumpy shape." });

        temp[2] = NewDialogue(playerName, new string[] { "You get a lot of grumpy shapes in here… Comes with the territory." });

        temp[3] = NewDialogue(witnessName, new string[] { "You’re so brave." });

        temp[4] = NewDialogue(playerName, new string[] { "No. I’m just a simple shape doing his job.",
        "You’re the brave one, Ms Seven. Your husband being eaten alive in front of you… I can’t imagine seeing that."});

        temp[5] = NewDialogue(witnessName, new string[] { "I don’t need to imagine. I can remember. Remember so clearly…",
        "That shape standing over him… So many sides… So many… At least more than two…"});

        temp[6] = NewDialogue(" ", new string[] { "[Find who this statement excludes and dismiss them from the lineup.]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 9
        eliminateThisSuspect = 9;

        //clueGO.SetActive(true);
        clue.text = "The murderer had more than two sides.";
    }

    //Hexagon
    void SeventhClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[2];

        temp[0] = NewDialogue("Semicircle", new string[] { "Yee-haw! I’m free! Time to go wrangle some cattle, and other cowboyish activities! " });

        temp[1] = NewDialogue(witnessName, new string[] { "More than that… Four! They had exactly four sides!" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // Player needs to select 5;
        eliminateThisSuspect = 5;

        //clueGO.SetActive(true);
        clue.text = "The murderer had four sides.";
    }

    // Hexagon
    void EighthClue()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[10];

        temp[0] = NewDialogue("Hexagon", new string[] { "Wahhhh! Me go can free! Me finally can get away from scary lady!" });

        temp[1] = NewDialogue(playerName, new string[] { "…Hmmm. Woah! You ok there Ms Seven." });
        
        temp[2] = NewDialogue(witnessName, new string[] { "Y-yes, Detective. I just need a moment. Going back to that place, even mentally… It was just so much." });

        temp[3] = NewDialogue(playerName, new string[] { "I understand, Ms Seven. You’ve done well. So very well. Just three left." });

        temp[4] = NewDialogue(witnessName, new string[] { "My poor husband… He was such a proud number Nine.",
            "So tall, so grand. The best of us, you know? The biggest.", "But when I saw him there… Eaten… All gobbled up… He… He… He…" });

        temp[5] = NewDialogue(playerName, new string[] { "Take your time." });

        temp[6] = NewDialogue(witnessName, new string[] { "He looked like a number Six! A teeny six! They reduced him!",
        "Oh, detective. They reduced my Niney!" });

        temp[7] = NewDialogue(playerName, new string[] { "I understand, Ms Seven. I’ve lost someone too. It’s a wound. A wound that never heals- Not completely.",
            "But it does scar over. It does get better.", "Now- let's catch the monster that did this." });

        temp[8] = NewDialogue(witnessName, new string[] { "Thank you, Detective. So it’s one of these three…", 
            "Gosh, it’s all so fuzzy. It was such a blur… But… In that chaos… I remember noticing…", "That the murderer had exactly one pair of parallel sides." });

        temp[9] = NewDialogue(" ", new string[] { "[It’s time to make your final choice! You’re not excluding this time- Select who you think did the murder!]" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        finalThree = true;

        // Player needs to select the murderer

        //.SetActive(true);
        clue.text = "The murderer had exactly one pair of parallel sides.";

        // enable 7
        EnableSeven();
    }

    // Picked the murderer?
    void AccuseTrapezium()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[3];

        temp[0] = NewDialogue("Kite", new string[] { "¡Dios mío! ¡Soy libre! ¡Estoy tan desesperada por orinar!" });

        temp[1] = NewDialogue("Rectangle", new string[] { "Aw man, it’s over? Same time next week? Guys? Guys?" });

        temp[2] = NewDialogue("Trapezium", new string[] { "Wait, what? No! Officer! Detective! Please! I’m innocent!", 
        "You gotta believe me! I’m being framed! I’m being fraaaaamed!" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // game over?
        GameOver();

    }

    void AccuseSeven()
    {
        StopAllCoroutines();
        gameState = GameState.CUTSCENE;

        temp = new Dialogue[7];

        temp[0] = NewDialogue(witnessName, new string[] { "What! Me! What are you talking about, Detective! I could never." });

        temp[1] = NewDialogue(playerName, new string[] { "I'm sorry, Ms Seven. I truly am. " });

        temp[2] = NewDialogue(witnessName, new string[] { "Sorry! I’ll show you sorry! How dare you! How could you possibly… I… I…",
        "…", "Ok… I confess… You got me. I ate my husband. I ate him alive."});

        temp[3] = NewDialogue("Cpt. Fractal", new string[] { "Take her away boys! Wow… Excellent work, Donut. Didn’t think you had it in you.",
        "But tell me… What gave it away? How did you know?" });

        temp[4] = NewDialogue(playerName, new string[] { "Tale as old as time, boss. Question as old as hell. As soon as I asked it, I knew the answer." });

        temp[5] = NewDialogue("Cpt. Fractal", new string[] { "Don’t mess me around, Detective. What question? What are you talking about?" });

        temp[6] = NewDialogue(playerName, new string[] { "Why was Six… Afraid of Seven?" });

        dialogueManager.canNowSelect = true;
        StartCoroutine(dialogueManager.StartConversation(temp));

        // game over?
        GameOver();
    }

    void EnableSeven()
    {
        seven.GetComponent<Suspect>().enabled = true;
    }

    public void BackToMainMenu()
    {
        gameState = GameState.MENU;
        SceneManager.LoadScene("MainMenu");
    }
}
