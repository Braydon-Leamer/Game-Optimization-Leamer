using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject loseTab;
    [SerializeField] private GameObject winTab;

    [SerializeField] private TextMeshProUGUI displayTime;
    [SerializeField] private TextMeshProUGUI displayTimeWin;


    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject RestartButtonWin;

    [SerializeField] private TextMeshProUGUI introText;

    public static UIManager instance;



    public int score;
    public int lives;
    public float time;


    public bool isAlive = true;
    public bool firstBallSpawned = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        //Update the text on the screen to be accurate
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";

        if (firstBallSpawned)
        {
            //Keep track of the time after the first ball spawns (unscaled so the timeScale changes don't mess with the recorded time
            time += Time.unscaledDeltaTime;
        }
        
        timeText.text = $"Time: {time.ToString("F2")}";
    }

    public void CheckIfAlive()
    {
        if(lives <= 0)
        {
            //If dead, delete all of the powerups just in case and enable the game over screen
            BallManager.instance.DeleteChildren();
            BallManager.instance.mainBall.gameObject.SetActive(false);
            isAlive = false;
            EnableGameOver();
        }
    }

    public void CheckIfWon()
    {
        //17*8*5 = 680 (all of the blocks)
        if(score >= 680)
        {
            //If won, delete all powerups just in case and enable the win screen
            BallManager.instance.DeleteChildren();
            BallManager.instance.mainBall.gameObject.SetActive(false);
            isAlive = false;
            EnableWinScreen();
        }
    }
    
    private void EnableGameOver()
    {
        //Release the mouse from being hidden/locked to the screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //disable all of the normal UI and activate the lose menu
        loseTab.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);

        displayTime.text = $"You lasted: {time.ToString("F2")} Seconds";

        //Set manual navigation to the buttons
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(RestartButton);
    }    
    private void EnableWinScreen()
    {
        //Release Mouse as stated above
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //disable all of the normal UI and activate the win menu
        winTab.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);

        displayTime.text = $"It took you: {time.ToString("F2")} Seconds";

        //Set manual nav to the buttons
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(RestartButton);
    }



    public void RestartGame()
    {
        //Reload level instead of respawning everything
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        //Quit out of the game
        Application.Quit();
    }

    public void DisableIntro()
    {
        introText.gameObject.SetActive(false);
    }

}
