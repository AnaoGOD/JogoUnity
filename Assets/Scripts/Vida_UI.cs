using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_UI : MonoBehaviour
{
    public Text text;
    public GameObject player;
    private Player vida;

    // Start is called before the first frame update
    void Start()
    {
        vida = player.GetComponent<Player>();

        text.text = vida.vidaMax+"/5";
        

    }

    // Update is called once per frame
    void Update()
    {
        text.text = vida.vidaAtual + "/" + vida.vidaMax;
    }
}
