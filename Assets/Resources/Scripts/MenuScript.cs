using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public GameObject inputField;
    public void loadInstructions(){
        string name = inputField.GetComponent<TMP_InputField>().text;

        if (name.Equals("")){
            name = "Player";
        }

        PersistentData.Instance.SetName(name);

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
