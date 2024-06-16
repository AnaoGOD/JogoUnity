using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{

    public Animator myAnim;
    public bool isAttacking = false;
    public static PlayerAttack instance;

    public bool delay = false;
    public float delayAtaqueConst;
    public float delayAtaque;
    int comboHits;
    float timer = 0;
    [SerializeField] float timerSet;

    public float attackRange;// Alcance do ataque
    public LayerMask enemyLayers; // Camada dos inimigos

    public int attackDamage=1;
    public float knockback=1.5f;
    public float knockbackDefault=1.5f;
    public bool facingRight = true;
    public GameObject player;


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
        Direcao();
        if (UnityEngine.Input.GetKeyDown(KeyCode.H) && !isAttacking) 
        { 
            Attack();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            comboHits = 0;
            if (facingRight == true)
            {
                knockback = knockbackDefault;
            }
            else
            {
                knockback = -knockbackDefault;
            }
          
        }

    }

    void Direcao()
    {
        PlayerMovement pMove =  player.GetComponent<PlayerMovement>();
        if (pMove.xInput == 1)
        {
            facingRight = true;
        }
        else if (pMove.xInput == -1)
        {
            facingRight = false;
        }
    }


    void Attack()
    {
        
        
        isAttacking = true;
        delay = true;
        delayAtaque = delayAtaqueConst;

        switch (comboHits)
        {
            case 0:
                FirstHit();
                break;
            case 1:
                SecondHit();
                break;
            case 2:
                FinalHit();
                break;
        }




        //acerta em todos os inimigos dentro do circulo de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);




        // Realiza o ataque para cada inimigo encontrado
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyVida enemyKnockback = enemy.GetComponent<EnemyVida>();
            enemyKnockback.body.velocity = new Vector2(enemyKnockback.body.velocity.x + knockback, enemyKnockback.body.velocity.y);

            EnemyVida enemyHP = enemy.GetComponent<EnemyVida>();
            if (enemyHP != null)
            {
                enemyHP.takeDamage(attackDamage);
            }

        }
            
        
    }
    void OnDrawGizmosSelected()
    {
        if (transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void FirstHit()
    {
        print("First Hit");
        comboHits++;
        timer = timerSet;
    }
    void SecondHit()
    {
        print("Second Hit");
        comboHits++;
        timer = timerSet;
        
    }
    void FinalHit()
    {
        print("Third Hit");
        timer = 0;
        comboHits = 0;
        knockback = knockback * 3;
    }
}
