using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiro : MonoBehaviour {

    GameObject cam;
    AudioSource somDisparo;
    Text balasTXT;
    Text recargaTXT;

    int balas = 10;
    int tamanhoDoPente = 10;
    int balasRecarga = 30;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        somDisparo = GetComponent<AudioSource>();
        balasTXT = GameObject.FindGameObjectWithTag("BalasTXT").GetComponent<Text>();
        recargaTXT = GameObject.FindGameObjectWithTag("RecargaTXT").GetComponent<Text>();
        balasTXT.text = balas.ToString();
        recargaTXT.text = balasRecarga.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        Recarga();
        Disparo();
	}

    void Disparo()
    {
        if (Input.GetMouseButtonDown(0) && balas > 0)
        {
            RaycastHit objeto;
            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out objeto))
            {
                somDisparo.Play();
                balas--;
                AtualizaTextos(balas,balasRecarga);
                if (objeto.transform.gameObject.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = objeto.transform.gameObject.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(250f, objeto.point, 10);
                }
            }
        }
    }

    void AtualizaTextos(int balas,int balasNaReserva)
    {
        balasTXT.text = balas.ToString();
        recargaTXT.text = balasNaReserva.ToString();
    }

    void Recarga()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int qtdACarregar = tamanhoDoPente - balas;
            int aux = 0;

            if(balasRecarga >= qtdACarregar) 
            {
                aux = qtdACarregar;
            }
            else
            {
                aux = balasRecarga;
            }

            balasRecarga -= aux;
            balas += aux;

        }

        AtualizaTextos(balas,balasRecarga);
    }
}
