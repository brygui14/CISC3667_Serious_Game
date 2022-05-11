using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    const int NUM_HIGH_SCORES = 5;
    const string NAME_KEY = "Player";
    const string SCORE_KEY = "Score";
    public TextMeshProUGUI score;

    string playerName;
    int playerScore;

    void Start()
    {
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();
        score.SetText(PersistentData.Instance.GetName() + "'s Score: " + PersistentData.Instance.GetScore());
        SaveScore();
    }

    void Update(){
        if (Input.GetKeyDown("space")){
            SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
        }
    }

    void SaveScore()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                if (playerScore > currentScore)
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);

                    playerName = tempName;
                    playerScore = tempScore;
                }

            }
            else
            {
                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                return;
            }
        }
    }
}
