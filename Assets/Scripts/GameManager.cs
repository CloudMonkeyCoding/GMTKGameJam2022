using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform startingPosition;
    public Transform player;

    public static bool gamePaused;
    public bool playerLost;

    public GameObject pauseMenuUI;

    public GameObject[] enemies;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }

    private void Start() 
    {
        player = SceneHandler.lastPlayerLocation; 
        if(SceneHandler.restart)
        {
            StartGame();
            SceneHandler.restart = false;
        }
    }

    public void StartGame()
    {
        PlayerStats.hp = PlayerStats.maxHP;
        PlayerStats.sheildDurability = PlayerStats.maxSheildDurability;
        PlayerStats.potionCount = PlayerStats.startPotionCount;

        player = startingPosition;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
