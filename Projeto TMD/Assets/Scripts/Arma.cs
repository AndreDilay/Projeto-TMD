using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] float velocidadeTiro = 5f;
    [SerializeField] Transform mira;

    [SerializeField] GameObject municao;

    Rigidbody2D rb2d;
    CursorLockMode mouseTravado;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerAtirar();
        TravarMouse();

    }

    public void PlayerAtirar()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcaoOlhar = posicaoMouse - transform.position;
        direcaoOlhar.Normalize();
        Debug.Log(direcaoOlhar);
        direcaoOlhar = Vector2.ClampMagnitude(direcaoOlhar, 0.2f);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject tiro = Instantiate(municao, transform.position, transform.rotation) as GameObject;
            tiro.GetComponent<Rigidbody2D>().velocity = direcaoOlhar * velocidadeTiro;
        }

    }

    private void TravarMouse()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mouseTravado = Cursor.lockState = CursorLockMode.Locked;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            mouseTravado = Cursor.lockState = CursorLockMode.None;
        }
    }
}
