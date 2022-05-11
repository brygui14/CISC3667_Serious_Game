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
        SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Single);
    }
}
