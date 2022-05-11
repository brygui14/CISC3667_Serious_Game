using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScript : MonoBehaviour
{

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("space")){
            loadGame();
        }
    }

    void loadGame(){
        SceneManager.LoadSceneAsync("Lesson 1", LoadSceneMode.Single);
    }
}
