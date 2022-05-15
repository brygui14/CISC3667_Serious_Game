using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendcheck : MonoBehaviour
{
  
  [SerializeField] Vector3 posti;
  
  [SerializeField] GameObject wallone;
    [SerializeField] GameObject walltwo;
    // Start is called before the first frame update
    void Start()
    {
        posti = new Vector3 (0f, .2f,0f);
        for(int i = 0; i < 10; i++)
        {
    // this.transform.position = this.transform.position + posti;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void OnTriggerEnter2D(Collider2D collision)// Check collision with ladder
    {
        if (collision.CompareTag("Ladder"))
        {
           
     
for(int i = 0; i < 5; i++)
        {
     this.transform.position = this.transform.position + new Vector3 (0f, 1f,0f);
       }
          wallone.SetActive(false);
           walltwo.SetActive(false);
        }

          if (collision.CompareTag("Oil_Ladder"))
        {
           transform.Rotate (Vector3.forward * -90);
     
        }

         

        
        


    }
}
