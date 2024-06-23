using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)//Ao colidir com player causa dano
    {
        Player PlayerHP = player.GetComponent<Player>();

        if (collision.gameObject.tag == "Player")
        {
            PlayerHP.takeDamage();
        }
    }

}
