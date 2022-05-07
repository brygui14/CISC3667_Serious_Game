using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    const float DELTADISTANCE = 3.5f;
    const float DELTACAMDISTANCE = -7.9f;

    public GameObject arm, hammer, shadow;
    private GameObject[] list = new GameObject[2];

    [SerializeField] float HorizontalSpeed = 20f;
    public Animator anim;
    private Transform _camera;

    void Awake() {
        anim = gameObject.GetComponent<Animator>();
        _camera = Camera.main.transform;
    }

    void Start(){
        list[0] = arm;
        list[1] = hammer;

    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 deltaDist = this.gameObject.transform.position - _camera.position;

        if (deltaDist.x > DELTADISTANCE){
            _camera.SetPositionAndRotation(new Vector3(_camera.position.x + HorizontalSpeed * Time.deltaTime, _camera.position.y, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
        }

        if (Input.GetKey("space")){
            Animator tempAnim;


            foreach (GameObject obj in list){
                tempAnim = obj.GetComponent<Animator>();
                tempAnim.Play("Hammer");
            }
            anim.Play("Hammer");


            
        }

        if (Input.GetAxisRaw("Horizontal") == 1){
            
            walk(true);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1){
            if (deltaDist.x > DELTACAMDISTANCE){
                walk(false);
            }
        }  
    }

    void walk(bool direction){
        if (direction){
            SpriteRenderer temp = GetComponent<SpriteRenderer>();

            Animator tempAnim;

            if (!temp.flipX){
                temp.flipX = true;
                foreach (GameObject obj in list){
                    temp = obj.GetComponent<SpriteRenderer>();
                    temp.flipX = true;
                }

            }

            foreach (GameObject obj in list){
                tempAnim = obj.GetComponent<Animator>();
                tempAnim.Play("Walk");
            }
            anim.Play("Walk");
            


            this.gameObject.transform.SetPositionAndRotation(new Vector3((float)this.gameObject.transform.position.x + HorizontalSpeed * Time.deltaTime, this.gameObject.transform.position.y, 0), Quaternion.Euler(this.gameObject.transform.eulerAngles));
        
        }
        else{
            SpriteRenderer temp = GetComponent<SpriteRenderer>();
            Animator tempAnim;

            if (temp.flipX){
                temp.flipX = false;
                foreach (GameObject obj in list){
                    temp = obj.GetComponent<SpriteRenderer>();
                    temp.flipX = false;
                }
            }
            foreach (GameObject obj in list){
                tempAnim = obj.GetComponent<Animator>();
                tempAnim.Play("Walk");
            }
            anim.Play("Walk");
            this.gameObject.transform.SetPositionAndRotation(new Vector3((float)this.gameObject.transform.position.x - HorizontalSpeed * Time.deltaTime, this.gameObject.transform.position.y, 0), Quaternion.Euler(this.gameObject.transform.eulerAngles));
            
        }
    }
}
