using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{

    const float DELTADISTANCEX = 3.5f;
    const float DELTADISTANCEY = .5f;
    const float DELTACAMDISTANCE = -7.0f;

    public GameObject arm, hammer, shadow;
    private GameObject[] list = new GameObject[2];

    float HorizontalSpeed = 750f;
    float verticalSpeed = 10000f;
    float scrollSpeed = 3f;
    public Animator anim;
    private Transform _camera;

    Rigidbody2D body;

    bool isPaused = false;

    public GameObject pausescreen, canvas;

    Vector2 move = new Vector2(0,0);
    Vector3 origin;
    Vector3 camOrigin;
    private float speed = 1.2f;
    private float maxSpeed;

    bool flip = false;

    bool canClimb = false;
    bool canDie = false;
    bool canSwing = false;


    public AudioClip audioSource;
    
    [SerializeField] bool isHolding = false;
    [SerializeField] bool isLadder = false;
    [SerializeField] bool wantHolding;
    [SerializeField] Vector3 posti = new Vector3 (0f, 3.27f,0f) ;
    [SerializeField] GameObject hldobj;
    [SerializeField] GameObject tempobj;
    [SerializeField] Image uiobj;
    [SerializeField] SpriteRenderer uispr;
    [SerializeField] SpriteRenderer hldspr;


    



    void Awake() {
        
        _camera = Camera.main.transform;
    }

    void Start(){
        list[0] = arm;
        list[1] = hammer;
        maxSpeed = speed * 2.5f;
        body = GetComponent<Rigidbody2D>();
        origin = transform.position;
        camOrigin = _camera.position;
        anim = gameObject.GetComponent<Animator>();

        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = audioSource;

    }

    void FixedUpdate() {
        
        walk(flip);
        climb();
    }

    // Update is called once per frame
    void Update()
    {   
        print(body.gravityScale);
        camMovement();
        inputMovement();
        // print(body.gravityScale);
        // Vector3 deltaDist = this.gameObject.transform.position - _camera.position;

        


        pauseScreen();
    }

    void inputMovement(){

        Vector3 deltaDist = this.gameObject.transform.position - _camera.position;

        if (Input.GetKey("space") && canSwing){
            Animator tempAnim;


            foreach (GameObject obj in list){
                tempAnim = obj.GetComponent<Animator>();
                tempAnim.Play("Hammer");
            }
            anim.Play("Hammer");

            
        }
        
        if (Input.GetAxisRaw("Horizontal") == -1){
            if (deltaDist.x > DELTACAMDISTANCE){
                move.x = -1;
                flip = false;
            }
        } 
        else{
            move.x = Input.GetAxisRaw("Horizontal");
            flip = true;
        }

        move.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Pause")){
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Resume")){
            isPaused = false;
            Time.timeScale = 1; 
        }

        if (move.y != 0 && canDie){
            death();
        }

        if (SceneManager.GetActiveScene().buildIndex == 6){
            wantHolding = Input.GetKeyDown("q");
            if( hldobj != null){
                hldobj.transform.position = new Vector3(this.transform.position.x + posti.x, -.4f, 0);
                // this.transform.position + posti;
                uiobj.color = new Color(uiobj.color.r, uiobj.color.g, uiobj.color.b, 100f);
            }
            else{
                uiobj.color = new Color(uiobj.color.r, uiobj.color.g, uiobj.color.b, 0f);
            }

            if(isHolding){
                if (wantHolding){
                    hldobj.SetActive(true);
                    hldobj = null;
                    isHolding =false;
                    uiobj.sprite = null;
                }
            }
            else{
                if (wantHolding && isLadder){
                    if( tempobj!= null){
                        hldobj = tempobj;
                        hldobj.SetActive(false);
                        isHolding = true;
                        //uispr = uiobj.GetComponent<SpriteRenderer>();

                        hldspr = hldobj.GetComponent<SpriteRenderer>();

                        uiobj.sprite = hldspr.sprite;
   
                    }
                }        
            }
        }

        


    }

    void camMovement(){
        Vector3 deltaDist = this.gameObject.transform.position - _camera.position;

        if (deltaDist.x >= DELTADISTANCEX){
            _camera.SetPositionAndRotation(new Vector3(_camera.position.x + scrollSpeed * Time.deltaTime, _camera.position.y, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
        }
        else if (deltaDist.y > DELTADISTANCEY){
            _camera.SetPositionAndRotation(new Vector3(_camera.position.x, _camera.position.y + scrollSpeed * Time.deltaTime, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
        }
        else if (deltaDist.y < DELTADISTANCEY && _camera.position.y >= 0){
            _camera.SetPositionAndRotation(new Vector3(_camera.position.x, _camera.position.y - scrollSpeed * Time.deltaTime, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
        }

        // _camera.SetPositionAndRotation(new Vector3(, _camera.position.y, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
        
        if (deltaDist.x < DELTACAMDISTANCE){
            _camera.SetPositionAndRotation(new Vector3(_camera.position.x - scrollSpeed * Time.deltaTime, _camera.position.y, _camera.position.z), Quaternion.Euler(_camera.eulerAngles));
            // body.velocity = new Vector2(0,0);
            // transform.position = new Vector3(_camera.position.x + DELTACAMDISTANCE, transform.position.y, transform.position.z);
            // body.position = new Vector2(DELTACAMDISTANCE, transform.position.y);

        }
        else if(deltaDist.x > -DELTACAMDISTANCE){
            body.velocity = new Vector2(0,0);
            body.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    void climb(){
        if (canClimb){
            Animator tempAnim;
            // body.velocity = new Vector2(0,0);
            move.x = 0;
            if(body.velocity.magnitude > maxSpeed){
                body.velocity = body.velocity.normalized * maxSpeed;
            }

            if(move.y != 0){
                foreach (GameObject obj in list){
                    tempAnim = obj.GetComponent<Animator>();
                    tempAnim.Play("Climb");
            }
            anim.Play("Climb");
            }

            body.AddForce(move * verticalSpeed * Time.fixedDeltaTime);

        }
    }

    void pauseScreen(){
        if (isPaused){
            pausescreen.SetActive(true);
            if (Input.GetButtonDown("Quit")){
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
            }
        }
        else {
            pausescreen.SetActive(false);
             
        }
    }

    void walk(bool direction){
        if (move.x == 0){
            return;
        }

        if (!canClimb){
            move.y = 0;
        }


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
            
            if(body.velocity.magnitude > maxSpeed){
                body.velocity = body.velocity.normalized * maxSpeed;
            }
            body.AddForce(move * HorizontalSpeed * Time.fixedDeltaTime);
        
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

            if(body.velocity.magnitude > maxSpeed){
                body.velocity = body.velocity.normalized * maxSpeed;
            }
            body.AddForce(move * HorizontalSpeed * Time.fixedDeltaTime);
            
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ladder"){
            canClimb = true;
            isLadder = true;
            tempobj = other.gameObject;
        }
        else if (other.gameObject.tag == "Oil_Ladder"){
            canDie = true;
            isLadder = true;
            tempobj = other.gameObject;

        }
        else if(other.gameObject.tag == "Gravity"){
            body.gravityScale = 1;
        }
        else if (other.gameObject.tag == "Screwdriver"){
            canvas.SendMessage("increaseScore", 100);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "tip"){
            canvas.SendMessage("hideTip");
        }
        else if (other.gameObject.tag == "death"){
            canDie = true;
            death();
        }
    }



    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Ladder"){
            canClimb = false;   
            isLadder = false;
            
        }
        else if (other.gameObject.tag == "Oil_Ladder"){
            canDie = false;
            isLadder = false;
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ceiling"){
            body.gravityScale = .5f;
        }
    }

    void death(){
        if (canDie){
            canvas.SendMessage("decreaseScore", 20);
            canvas.SendMessage("displayTip");
            canDie = false;
            _camera.SetPositionAndRotation(camOrigin, Quaternion.identity);
            GetComponent<AudioSource>().Play(0);
            Instantiate(this.gameObject, origin, Quaternion.identity);
            this.gameObject.transform.SetPositionAndRotation(origin, Quaternion.identity);
            SpriteRenderer[] AllRend = GetComponentsInChildren<SpriteRenderer>();
            Animator[] AllAnim = GetComponentsInChildren<Animator>();

            foreach (SpriteRenderer sp in AllRend){
                sp.enabled = false;
            }
            foreach (Animator sp in AllAnim){
                sp.enabled = false;
            }


            // GetComponentInChildren<SpriteRenderer>().enabled = false;
            // GetComponentInChildren<Animator>().enabled = false;
            Destroy(gameObject, audioSource.length);
        }
    }
}
