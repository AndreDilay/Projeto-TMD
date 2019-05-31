using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeJogo : MonoBehaviour
{

    [SerializeField] CameraSeguir cameraSeguir;
    [SerializeField] Transform posicaoPlayer;

    //Para Fazer a camera seguir outra coisa basta add uma referencia
    // Transform nomeDaVariavel;

    // Start is called before the first frame update
    void Start()
    {
        cameraSeguir.Config(() => posicaoPlayer.position);

        //Modificar esta linha para seguir a referencia
        //cameraSeguir.SetGetCameraSeguirPosicaoFunc(() => nomeDaVariavel.position);
        //Pode também criar botões para seguir diferentes referencias
    }

}
