using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayScript : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI tip;
    private float actScore = PersistentData.Instance.GetScore();

    bool levelCcmplete = false;
    
    void loadNextLevel(){
        SceneManager.LoadSceneAsync("Lesson 2", LoadSceneMode.Single);
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

    void increaseScore(int value){
        print(actScore);
        actScore += value;
        print(actScore);
    }

    void displayTip(){
        tip.gameObject.SetActive(true);
    }

    void hideTip(){
        tip.gameObject.SetActive(false);
    }
    void won(){
        levelCcmplete = true;
    }
}
