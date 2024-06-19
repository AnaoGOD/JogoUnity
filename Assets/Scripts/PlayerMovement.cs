
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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

    public float xInput;
    public float yInput;
    public Animator myAnim;

    private void Awake()
    {
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (PlayerAttack.instance.delay == true) 
        { 
            if (PlayerAttack.instance.delayAndar < 0.2)
            {
                HandleJump();
            }
        }
        else
        {
            HandleJump();
        }

    }

    void FixedUpdate()
    {
        CheckGround();
        if (PlayerAttack.instance.delay == true)
        {
            PlayerAttack.instance.delayAndar -= Time.deltaTime;
            if (PlayerAttack.instance.delayAndar < 0)
            {
                PlayerAttack.instance.delayAndar = PlayerAttack.instance.delayAndarDefault;
                HandleXMovement();
                PlayerAttack.instance.delay = false;
            }
            
        }
        else
        {
            HandleXMovement();
        }
        ApplyFriction();
    }


    //Fun��es



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
            if (grounded==true)  { 
                myAnim.Play("Corre");
                myAnim.SetBool("notRunning", false);
                myAnim.SetBool("notGrounded", false);

            }
        }
        else if (Mathf.Abs(xInput) == 0)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            myAnim.SetBool("notRunning", true);
            if (grounded == true)
            {
                myAnim.SetBool("notGrounded", false);
            }
        }

        if (body.velocity.y > 1 && !grounded)
        {
            myAnim.Play("Jump");
            myAnim.SetBool("notGrounded", true);

        }
        if (body.velocity.y < 1.2 && !grounded)
        {
            myAnim.Play("Fall");
            myAnim.SetBool("notGrounded", true);

        }
    }
    public void FaceInput()
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