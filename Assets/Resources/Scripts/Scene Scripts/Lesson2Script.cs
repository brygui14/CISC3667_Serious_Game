using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lesson2Script : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space")){
            SceneManager.LoadSceneAsync("Gameplay2", LoadSceneMode.Single);
        }
    }
}
