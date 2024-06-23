
using Cinemachine;
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
    public float delayAndarDefault;
    public float delayAndar;
    public float delayAtaque;
    public float delayAnimacao=0.1f;

    int comboHits;
    float timer = 0;
    [SerializeField] float timerSet;

    public float attackRange;// Alcance do ataque
    public LayerMask enemyLayers; // Camada dos inimigos

    public int attackDamage=1;
    public int cameraShake;
    public float knockback=1.5f;
    public float knockbackDefault=1.5f;
    public bool facingRight = true;
    public GameObject player;

    private CinemachineImpulseSource impulseSource;


    private void Awake()
    {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Direcao();
        if (UnityEngine.Input.GetKey(KeyCode.W))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && !isAttacking )
            {
                AttackCima();
                timer = timerSet;
                

            }
        }
        if (UnityEngine.Input.GetMouseButtonDown(0) && !isAttacking && myAnim.GetBool("notAttackingUp") == true)
        {
            Attack();

        }
        if (UnityEngine.Input.GetMouseButtonDown(0) && !isAttacking && myAnim.GetBool("notAttackingAr") == true)
        {
            AttackAr();
            timer = timerSet;
        }



        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            comboHits = 0;
            
            myAnim.SetBool("notAttacking", true);
            myAnim.SetBool("notAttackingUp", true);
            myAnim.SetBool("notAttackingAr", true);
            if (facingRight == true)
            {
                knockback = knockbackDefault;
            }
            else
            {
                knockback = -knockbackDefault;
            }

        }
        if (delayAtaque > 0)//delayAtaque dá uma pausa entre ataques (3 ataques seguidos e ataque para cima)
        {
            delayAtaque -= Time.deltaTime;
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
        PlayerMovement grounded = player.GetComponent<PlayerMovement>();
        if (delayAtaque <= 0 && grounded.grounded==true) 
        {

            //Para as animações quando estamos parados e quando corremos e começamos a atacar
            if (myAnim.GetBool("notRunning") == false)//a correr
            {
                myAnim.SetBool("notAttacking", false);
            }
            else 
            {
            isAttacking = true;//parado          
            }
            myAnim.SetBool("notRunning", true);//a parar animação correr
            delay = true;
            delayAndar = delayAndarDefault;
        
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

    }

    IEnumerator AttackCimaD() //usado para haver um delay dentro da função
    {
        PlayerMovement grounded = player.GetComponent<PlayerMovement>();
        if (delayAtaque <= 0 && grounded.grounded == true)
        {
            attackDamage = 1;
            cameraShake = 0;
            attackRange = 0.53f;

            //Para as animações quando estamos parados e quando corremos e começamos a atacar  
            myAnim.SetBool("notAttackingUp", false);
            myAnim.SetBool("notRunning", true);

            delay = true;
            delayAndar = delayAndarDefault;

            yield return new WaitForSeconds(0.1f);//delay da execução da função

            //acerta em todos os inimigos dentro do circulo de ataque
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

            // Realiza o ataque para cada inimigo encontrado
            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyVida enemyKnockback = enemy.GetComponent<EnemyVida>();
                enemyKnockback.body.velocity = new Vector2(enemyKnockback.body.velocity.x + (knockback / 2), enemyKnockback.body.velocity.y + (Mathf.Abs(knockback) * 6));

                EnemyVida enemyHP = enemy.GetComponent<EnemyVida>();
                if (enemyHP != null)
                {
                    enemyHP.takeDamage(attackDamage);
                }

            }
            delayAtaque = 0.6f;
        }
    }
    void AttackCima()
    {
        StartCoroutine(AttackCimaD());
    }

    IEnumerator AttackArD()
    {
        PlayerMovement grounded = player.GetComponent<PlayerMovement>();
        if (delayAtaque <= 0 && grounded.grounded == false)
        {
            //certificar que ao executar tem os valores corretos
            attackDamage = 1;
            cameraShake = 1;
            attackRange = 0.55f;


            //Para as animações quando estamos parados e quando corremos e começamos a atacar
            myAnim.SetBool("notAttackingAr", false);

            myAnim.SetBool("notRunning", true);//a correr
            delay = true;

            yield return new WaitForSeconds(0.1f);//delay da execução da função

            //acerta em todos os inimigos dentro do circulo de ataque
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

            // Realiza o ataque para cada inimigo encontrado
            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyVida enemyKnockback = enemy.GetComponent<EnemyVida>();
                enemyKnockback.body.velocity = new Vector2(enemyKnockback.body.velocity.x + (knockback * 3), enemyKnockback.body.velocity.y + 5);

                EnemyVida enemyHP = enemy.GetComponent<EnemyVida>();
                if (enemyHP != null)
                {
                    enemyHP.takeDamage(attackDamage);
                }

            }
            delayAtaque = 0.8f;
        }
    }

    void AttackAr()
    {
        StartCoroutine(AttackArD());
    }


    void OnDrawGizmosSelected()//cria área de ataque
    {
        if (transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void FirstHit()
    {
        comboHits++;
        timer = timerSet;
        attackDamage = 1;
        cameraShake = 0;
        attackRange = 0.53f;
    }
    void SecondHit()
    {
        comboHits++;
        timer = timerSet;
        attackDamage = 1;
    }
    void FinalHit()
    {
        timer = 0;
        delayAtaque = 0.5f;
        delayAndar = delayAndar+0.1f;
        comboHits = 0;
        attackDamage = 3;
        knockback = knockback * 3;
        cameraShake = 1;
    }
}

