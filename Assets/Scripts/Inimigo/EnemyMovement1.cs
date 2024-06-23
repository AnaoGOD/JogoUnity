using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D body;
    public LayerMask groundMask;
    public BoxCollider2D groundCheck;
    public Animator myAnimEnemy;
    public float speed;
    public bool grounded;
    public GameObject player;
    public GameObject enemy;
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float scale;
    float xdistance;
    float ydistance;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xdistance = Mathf.Abs(transform.position.x - playerTransform.position.x);
        ydistance = Mathf.Abs(transform.position.y - playerTransform.position.y);
        
        if (isChasing && xdistance > chaseDistance)
        {
            isChasing = false;
        }

        else if (!isChasing && xdistance < chaseDistance)
        {
           isChasing = true;
        }
        if (isChasing && grounded && ydistance < 3)//se estiver a perseguir, no chao e a uma distancia no Y inferior a 4 ele anda em diracao ao player
        {
            EnemyVida Hurt = enemy.GetComponent<EnemyVida>();
            if (transform.position.x > playerTransform.position.x && !Hurt.hurt)
            {
                transform.localScale = new Vector3(-scale, scale, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
                myAnimEnemy.Play("Inimigo_Andar");
            }
            if (transform.position.x < playerTransform.position.x && !Hurt.hurt)
            {
                transform.localScale = new Vector3(scale, scale, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
                myAnimEnemy.Play("Inimigo_Andar");
            }
        }

        //Andar antigo
        /*Vector3 scale = transform.localScale;
        if (myAnimEnemy.GetBool("IsDead") == false) 
        {
            EnemyVida Hurt = enemy.GetComponent<EnemyVida>();
            if (player.transform.position.x > transform.position.x) 
            {
                scale.x = Mathf.Abs(scale.x);
                if (!Hurt.hurt) 
                {
                body.velocity = new Vector2(body.velocity.x + speed,body.velocity.y);
                }
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                if (!Hurt.hurt) 
                { 
                    body.velocity = new Vector2((body.velocity.x + speed)*-1,body.velocity.y);
                }
            }

            transform.localScale = scale;
        }*/
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }









}
