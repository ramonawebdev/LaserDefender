using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delayInSeconds = 3f;
    [SerializeField] int enemy = 0;
    int currentIndex;
    [SerializeField] bool winGame = false;
    

    public void CountEnemies()
    {
        enemy++;
    }

    public void EnemyDown()
    {
        enemy--;
        if (enemy <= 0)
        {
            if (winGame)
            {
                Player player = FindObjectOfType<Player>();
                int health = player.GetHealth();
                FindObjectOfType<GameSession>().AddToScore(health);
                Destroy(player.gameObject, 2f);
            }
           LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(WaitAndLoadNext());
    }

    IEnumerator WaitAndLoadNext()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
