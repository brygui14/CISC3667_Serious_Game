using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lesson1Script : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space")){
            SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Single);
        }
    }
}
