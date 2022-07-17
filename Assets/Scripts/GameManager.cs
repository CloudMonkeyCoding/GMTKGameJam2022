using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class GameManager : MonoBehaviour
{
    public Transform startingPosition;
    public Transform player;

    public static bool gamePaused;
    public bool playerLost;

    public GameObject pauseMenuUI;

    public int maxHP;
    public int maxSheildDurability;
    public int startPotionCount;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneHandler.activeEnemies[0] = true;
        }
        Debug.Log(SceneHandler.activeEnemies[0]);
    }

    public void StartGame()
    {
        PlayerStats.hp = maxHP;
        PlayerStats.sheildDurability = maxSheildDurability;
        PlayerStats.potionCount = startPotionCount;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
