using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int score = 0;
    int health;
    Player player;



    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int nrGameSession = FindObjectsOfType<GameSession>().Length;
        if (nrGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
