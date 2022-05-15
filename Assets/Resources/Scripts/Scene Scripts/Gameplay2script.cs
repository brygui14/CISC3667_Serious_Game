using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameplay2script : MonoBehaviour
{
    public TextMeshProUGUI score;
    private float actScore = PersistentData.Instance.GetScore();

    bool levelCcmplete = false;
    
    void loadNextLevel(){
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
    }

    void Update() {
        PersistentData.Instance.SetScore((int)Mathf.Round(actScore));

        if (!levelCcmplete){
            actScore -= 1 * Time.fixedDeltaTime;
        }

        score.SetText("Score: " + Mathf.Round(actScore));

    }   

    void decreaseScore(int value){
        print(actScore);
        actScore -= value;
        print(actScore);
    }

    void won(){
        levelCcmplete = true;
    }
}
