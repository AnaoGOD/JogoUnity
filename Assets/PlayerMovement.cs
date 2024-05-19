using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    public float acceleration;
    [Range(0f, 1f)]
    public float groundDecay;
    public float MaxXSpeed;

    public float jumpSpeed;

    

    public bool grounded ;

    float xInput;
    float yInput;

    public Animator myAnim;


    // Update is called once per frame
    void Update()
    {
        CheckInput();
        HandleJump();
      
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleXMovement();
        ApplyFriction();

       
    }


    //Funções



    //Receber input
    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    //Mexer personagem
    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0) 
        
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -MaxXSpeed,MaxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            FaceInput();
            
        } 
        else if (Mathf.Abs(xInput) == 0 && grounded == false)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }
    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            
        }
        if (body.velocity.y > 1 && !grounded)
        {
            myAnim.Play("Jump");
        }
    }

    //Verificar se personagem esta no chao
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

    }
    //Aplicar friccao ao parar de andar (para nao parar instantaneamente)
    void ApplyFriction()
    {
        if (grounded && xInput == 0 )
        {
            body.velocity = new Vector2(body.velocity.x * groundDecay, body.velocity.y);

        }
    }

}
