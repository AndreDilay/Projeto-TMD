using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Parâmetros de Confuguração
    [Header("Movimentação")]
    [SerializeField] float velocidade = 5f;
    [Header("Esquiva")]
    [SerializeField] float distanciaEsquiva = 5f;


    //Para a esquiva
    [SerializeField] Vector3 ultimaDirecao;


    //Referencias de prefabs
    [Header("Arma Posição")]
    [SerializeField] Transform armaPosicao;


    //Referências de componetes
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializção
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
        Esquiva();
        OlharParaMouse();
    }


    //Metodo de Movimentação
    private void Movimentacao()
    {
        //Movimentação baseada em -1 a 1
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        float movimentoVertical = Input.GetAxisRaw("Vertical");

        //Calculo da movimentação
        rb2d.velocity = new Vector2(movimentoHorizontal * velocidade, rb2d.velocity.y);
        rb2d.velocity = new Vector2(rb2d.velocity.x, movimentoVertical * velocidade);
        
        //Lembrar a ultima direção (para a esquiva)
        Vector2 direcaoMovimento = new Vector2(movimentoHorizontal, movimentoVertical).normalized;
        ultimaDirecao = direcaoMovimento;

    }

    private void Esquiva()
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.position += ultimaDirecao * distanciaEsquiva;
        }
    }


    private void OlharParaMouse()
    {

        Vector3 posicaoMouse = Input.mousePosition;
        posicaoMouse = Camera.main.ScreenToWorldPoint(posicaoMouse);
        Vector2 direcaoOlhar = new Vector2(posicaoMouse.x - transform.position.x, posicaoMouse.y - transform.position.y);
        transform.up = direcaoOlhar;
  
    }

}
