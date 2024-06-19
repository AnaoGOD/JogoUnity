using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_UI : MonoBehaviour
{
    public Text text;
    public GameObject player;
    private Player vidaAtual;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = player.GetComponent<Player>();
        text.text = vidaAtual.vidaMax+"/5";
        

    }

    // Update is called once per frame
    void Update()
    {
        text.text = vidaAtual.vidaAtual + "/5";
    }
}
