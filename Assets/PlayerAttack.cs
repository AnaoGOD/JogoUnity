using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator myAnim;
    public bool isAttacking = false;
    public static PlayerAttack instance;

    private void Awake()
    {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.H) && !isAttacking)
        {
            isAttacking = true;
        } 
    }
}
