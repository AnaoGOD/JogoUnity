using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class EnemyVida : MonoBehaviour

{
    public Rigidbody2D body;
    public Animator myAnimEnemy;
    public int vidaMax = 5;
    int vidaAtual;
    public bool hurt;
    float timer = 0;
    float timerSet = 0.6f;
    public GameObject player;
    public GameObject enemy;
    public GameObject enemyHurtBox;
    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement grounded = enemy.GetComponent<EnemyMovement>();
        if (timer > 0 && grounded)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            hurt = false;
        }
    }
    
    public void takeDamage(int damage)
    {
        PlayerAttack cameraShake = player.GetComponent<PlayerAttack>();
        if (cameraShake.cameraShake == 0) 
        { 
            CameraShake.instance.Camera_Shake(impulseSource);//tremer camara quando leva dano
        }
        else if (cameraShake.cameraShake == 1)
        {
            CameraShake.instance.Camera_Shake2(impulseSource);//tremer camara quando leva dano
        }

        vidaAtual -= damage;

        myAnimEnemy.SetTrigger("Hurt");
        hurt = true;
        timer = timerSet;

        if (vidaAtual <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        enemyHurtBox.GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        myAnimEnemy.SetBool("IsDead",true);
    }


}
