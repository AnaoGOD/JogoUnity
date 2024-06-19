using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D body;
    public LayerMask groundMask;
    public Animator myAnimEnemy;
    public float speed;
    public GameObject player;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        if (myAnimEnemy.GetBool("IsDead") == false) 
        { 
            if (player.transform.position.x > transform.position.x) 
            { 
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }

            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
    }


    //Funções

    




}
