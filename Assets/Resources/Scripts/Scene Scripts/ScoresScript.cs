using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresScript : MonoBehaviour
{

    const string NAME_KEY = "Player";
    const string SCORE_KEY = "Score";
    [SerializeField] TextMeshProUGUI[] players;
    [SerializeField] TextMeshProUGUI[] scores;

    

    void Start() {
        ShowHighScores();
    }
    public void backButton(){
        SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);        
    }

    void ShowHighScores()
    {
        for (int i = 0; i < 5; i++)
        {
            players[i].SetText(PlayerPrefs.GetString(NAME_KEY + (i+1)));
            scores[i].SetText(PlayerPrefs.GetInt(SCORE_KEY + (i+1)).ToString());
        }

    }

    
}
