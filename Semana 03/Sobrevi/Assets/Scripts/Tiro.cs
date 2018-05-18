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

    public Animator anim;
    private bool estaRecarregando = false;


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

    private void FixedUpdate()
    {
        estaRecarregando = anim.GetCurrentAnimatorStateInfo(0).IsName("reload");
    }

    void Disparo()
    {
        if (Input.GetMouseButtonDown(0) && balas > 0 && !estaRecarregando)
        {

            anim.CrossFadeInFixedTime("fire", 0.1f);
            somDisparo.Play();
            balas--;
            AtualizaTextos(balas, balasRecarga);
            RaycastHit objeto;

            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out objeto))
            {
                if (objeto.transform.CompareTag("Zombie"))
                {
                    objeto.transform.gameObject.GetComponent<Zombie>().vida -= 10;
                }

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
        if (Input.GetKeyDown(KeyCode.R) && balasRecarga > 0)
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
            RecargaAnimacao();
            balasRecarga -= aux;
            balas += aux;
        }

        AtualizaTextos(balas,balasRecarga);
    }
    
    void RecargaAnimacao()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("reload"))
        {
            return;
        }

        anim.CrossFadeInFixedTime("reload", 0.1f);
    }

}
