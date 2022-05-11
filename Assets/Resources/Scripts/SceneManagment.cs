using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame(){
        SceneManager.LoadSceneAsync("Instructions", LoadSceneMode.Single);
    }

    public void loadOptions(){
        SceneManager.LoadSceneAsync("Options", LoadSceneMode.Single);
    }

    public void loadScores(){
        SceneManager.LoadSceneAsync("HighScores", LoadSceneMode.Single);
    }

    public void backButton(){
        SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
    }
}
