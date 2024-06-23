
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

    [Range(0.9f, 1f)]
    public float Slide;

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
            body.velocity = new Vector2(body.velocity.x * Slide, body.velocity.y);//Se o delay for verdadeiro o personagem nao para de andar instantaneamente, tendo um deslize (Slide)
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

        if (PlayerAttack.instance.delay == true)//se o delay for verdadeiro, espera acabar o delayAndar para poder andar
        {
            PlayerAttack.instance.delayAndar -= Time.deltaTime;
            if (PlayerAttack.instance.delayAndar < 0)
            {
                PlayerAttack.instance.delayAndar = PlayerAttack.instance.delayAndarDefault;
                HandleXMovement();
                PlayerAttack.instance.delay = false;
            }    
        }
        else //senao anda normalmente
        {
            HandleXMovement();
        }
    }


    //Fun��es



    //Receber input
    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");//input horizontal
        yInput = Input.GetAxis("Vertical");//input vertical
    }

    //Mexer personagem
    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0)//se input do player for maior do que 0
        
        {     
            float newSpeed = Mathf.Clamp(body.velocity.x + xInput, -MaxXSpeed,MaxXSpeed);//Velocidade compreendida entre o MaxXSpeed positivo e negativo
            body.velocity = new Vector2(newSpeed, body.velocity.y);
            
            FaceInput();

            if (grounded==true)  { 
                myAnim.Play("Corre");
                myAnim.SetBool("notRunning", false);
                myAnim.SetBool("notGrounded", false);

            }
        }
        else if (Mathf.Abs(xInput) == 0)//se input do player for 0
        {
            body.velocity = new Vector2(0, body.velocity.y);
            myAnim.SetBool("notRunning", true);
            if (grounded == true)
            {
                myAnim.SetBool("notGrounded", false);
            }
        }

        if (body.velocity.y > 1 && !grounded)//se velocida do Y for maior a 1 e nao estiver no chao
        {
            myAnim.Play("Jump");
            myAnim.SetBool("notGrounded", true);

        }
        if (body.velocity.y < 1.2 && !grounded)//se velocida do Y for menor a 1.2 e nao estiver no chao
        {
            myAnim.Play("Fall");
            myAnim.SetBool("notGrounded", true);

        }
    }
    public void FaceInput()//Verifica o input do player e muda a escala do X para o personagem olhar na direcao pressionada
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            PlayerAttack.instance.delayAtaque = 0;
        }

    }

    //Verificar se personagem esta no chao
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }


}
