using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeguir : MonoBehaviour
{

    private Func<Vector3> GetCameraSeguirPosicaoFunc;


    //Func para camera (não sei como funciona Func)
    public void Config(Func<Vector3> GetCameraSeguirPosicaoFunc)
    {
        this.GetCameraSeguirPosicaoFunc = GetCameraSeguirPosicaoFunc;
    }


    //Metodo para mudar oq a camera segue
    public void SetGetCameraSeguirPosicaoFunc(Func<Vector3> GetCameraSeguirPosicaoFunc)
    {
        this.GetCameraSeguirPosicaoFunc = GetCameraSeguirPosicaoFunc;
    }

    // Update is called once per frame
    void Update()
    {
        //Achar a posicao da camera
        Vector3 cameraSeguirPosicao = GetCameraSeguirPosicaoFunc();
        cameraSeguirPosicao.z = transform.position.z;

        //Para Suavizar o movimento da camera
        Vector3 cameraMovDir = (cameraSeguirPosicao - transform.position).normalized;
        float distancia = Vector3.Distance(cameraSeguirPosicao, transform.position);
        float cameraVeloMov = 3f;


        //Para não bugar com baixo Framerate
        if (distancia > 0)
        {
            Vector3 novaPosicaoCamera = transform.position = transform.position + cameraMovDir * distancia * cameraVeloMov * Time.deltaTime;

            float distaciaDepoisDeMover = Vector3.Distance(novaPosicaoCamera, cameraSeguirPosicao);

            if (distaciaDepoisDeMover > distancia)
            {
                //Ultrapassou o alvo
                novaPosicaoCamera = cameraSeguirPosicao;
            }

            transform.position = novaPosicaoCamera;
        }    
        

    }
}
