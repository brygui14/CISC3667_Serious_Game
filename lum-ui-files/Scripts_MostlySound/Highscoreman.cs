using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscoreman : MonoBehaviour
{
     private int hsflag = 5;
    private string namekey = "Player";
    private string scorekey = "Score";

    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] Text[] nameTxts;
    [SerializeField] Text[] scoreTxts;
    // Start is called before the first frame update
    void Start()
    {
         playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();

        SaveScore();
        ShowHighScores();
    }

    void SaveScore()
    {
        for (int i = 1; i <= hsflag; i++) {
            string heldname = namekey + i;
            string heldscore = scorekey + i;

            if (PlayerPrefs.HasKey(heldscore)){
                
                int currentScore = PlayerPrefs.GetInt(heldscore);
               
                if (playerScore > currentScore) {
                  
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(heldname);
                    PlayerPrefs.SetString(heldname, playerName);
                    PlayerPrefs.SetInt(heldscore, playerScore);
                    playerName = tempName;
                    playerScore = tempScore;

                    
                       }
                       
                 }
            else
            {
                PlayerPrefs.SetString(heldname, playerName);
                PlayerPrefs.SetInt(heldscore, playerScore);
                return;
            }
        }
    }

        void ShowHighScores()
    {
        for (int i = 0; i <  hsflag; i++)
        {
            nameTxts[i].text = PlayerPrefs.GetString(namekey + (i+1));
            scoreTxts[i].text = PlayerPrefs.GetInt(scorekey + (i+1)).ToString();
        }

    }
}
