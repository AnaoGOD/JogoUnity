using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public int vidaMax = 5;
    public int vidaAtual;
    public Animator myAnim;
    public GameObject player;
    public GameObject playerAttack;
    public float invincibility;
    public float invincibilityDefault=3f;
    public GameManagerScript gameManager;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer trans = GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;

        if (invincibility > 0)
        {
            invincibility -= Time.deltaTime;
            trans.color = new Color(1f, 1f, 1f, 0.85f);//Quando player está invencivel fica um pouco transparente
        }
        else
        {
            trans.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void takeDamage()//funcao levar dano
    {
        PlayerAttack delay = playerAttack.GetComponent<PlayerAttack>();

        if (invincibility <= 0)
        {
            vidaAtual -= 1;
            myAnim.SetTrigger("Hurt");
            delay.delay = true;
            invincibility = invincibilityDefault;
        }
       
        if (vidaAtual <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        Player ScriptVida = player.GetComponent<Player>();
        PlayerMovement ScriptMove = player.GetComponent<PlayerMovement>();
        PlayerAttack ScriptAttack = playerAttack.GetComponent<PlayerAttack>();
        isDead = true;
        myAnim.SetBool("IsDead", true);
        ScriptMove.enabled=false;
        ScriptAttack.enabled=false;
        ScriptVida.enabled = false;
        gameManager.gameOver();
    }
}
