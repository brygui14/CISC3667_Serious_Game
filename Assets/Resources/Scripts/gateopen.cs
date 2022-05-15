using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateopen : MonoBehaviour
{

  [SerializeField] GameObject wallone;
    [SerializeField] GameObject walltwo;
      [SerializeField] GameObject friend;
       [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        wallone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       private void OnTriggerEnter2D(Collider2D collision)// Check collision with ladder
    {
        if (collision.CompareTag("Ladder"))
        {
           
     wallone.SetActive(true);

          player.transform.position = player.transform.position - new Vector3 (5f, 0f,0f);;
        }


    }
}
