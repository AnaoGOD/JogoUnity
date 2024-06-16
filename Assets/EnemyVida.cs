using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVida : MonoBehaviour

{
    public Rigidbody2D body;
    public Animator myAnimEnemy;
    public int vidaMax = 5;
    int vidaAtual;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        vidaAtual -= damage;

        myAnimEnemy.SetTrigger("Hurt");

        if (vidaAtual <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        myAnimEnemy.SetBool("IsDead",true);
    }
}
