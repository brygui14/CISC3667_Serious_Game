using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    [SerializeField] float movementh;// read input for left and right
     [SerializeField] float movementv;// read the imput for up
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int speed;// speed for left and right movement
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] bool isGrounded = true;
    

     [SerializeField] bool isLadder = false;// Flag to see if the player is touching a ladder

[SerializeField] bool isBadLadder = false;// Flag to see if the player is touching a Bad Ladder
[SerializeField] bool isFalling = true;// Check if player fails on a bad ladder

   [SerializeField] bool isClimbing = false;// Checks if the player is on a ladder
   [SerializeField] int climbspeed;// Speed of moving up a ladder

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        speed = 15;
        climbspeed =8;
    }

    // Update is called once per frame
    void Update()
    {
        
         
        movementh = Input.GetAxis("Horizontal");// Gets user inputs for Horizontal movement
        if (Input.GetButtonDown("Jump")){
            jumpPressed = true;
        }
             movementv = Input.GetAxis("Vertical");// Gets user inputs for Vertical movement
     if (isLadder && Mathf.Abs(movementv) > 0f )//checks if the user is trying to climb
        {
            isClimbing = true;
        
        }
    }       
    

    //called potentially multiple times per frame
    //used for physics & movement
    void FixedUpdate()
    { 
  
       
        rigid.velocity = new Vector2(movementh * speed, rigid.velocity.y);
        
        if (movementh < 0 && isFacingRight || movementh > 0 && !isFacingRight)//Flips the sprite
            Flip();
        if (jumpPressed && isGrounded){//let the user jump
            Jump();
    }
     if (isClimbing)// Allows the player to go up ladder by setting gravity to zero. Else sets gravity to 1
        {
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, movementv * climbspeed);		
        }
        else 
        {
            rigid.gravityScale = 1f;
        }
    
    
    }

    void Flip()//flips sprite
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void Jump()//lets sprite jump
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        isGrounded = false;
        jumpPressed = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
      private void OnTriggerEnter2D(Collider2D collision)// Check collision with ladder
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = true;
        }

 if (collision.CompareTag("bad") )// Check collision with bad ladders
        {
            isLadder = true;
             isBadLadder = true;

        }
    }

     private void OnTriggerExit2D(Collider2D collision)// Check for when the player ets off a ladder. Setss all ladder varible to false
    {
        if (collision.CompareTag("ladder")||collision.CompareTag("bad"))
        {
          
		isLadder = false;
          isBadLadder = false;
            isClimbing = false;
;
        }
    }


}



