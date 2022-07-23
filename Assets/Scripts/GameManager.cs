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

    public GameObject fadePanel;

    public AudioManager audioManager;

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
        player.position = SceneHandler.lastPlayerLocation; 
        if(SceneHandler.restart)
        {
            StartGame();
            SceneHandler.restart = false;
        }
        for (int i = 0; i < 4; i++)
        {
            enemies[i].SetActive(SceneHandler.activeEnemies[i]);
        }
        StartCoroutine(ActivateFade());
    }
         

    public void StartGame()
    {
        PlayerStats.hp = PlayerStats.maxHP;
        PlayerStats.sheildDurability = PlayerStats.maxSheildDurability;
        PlayerStats.potionCount = PlayerStats.startPotionCount;

        SceneHandler.activeEnemies[0] = true;
        SceneHandler.activeEnemies[1] = true;
        SceneHandler.activeEnemies[2] = true;
        SceneHandler.activeEnemies[3] = true;

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
        SceneHandler.restart = true;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
        
    IEnumerator ActivateFade()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Animator>().SetTrigger("FadeStart");
        yield return new WaitForSeconds(1.1f);
        fadePanel.SetActive(false);
    }
}
