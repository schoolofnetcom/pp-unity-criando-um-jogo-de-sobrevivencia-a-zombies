using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiro : MonoBehaviour {

    GameObject cam;
    AudioSource somDisparo;
    Text balasTXT;
    Text recargaTXT;

    int balas = 90;
    int tamanhoDoPente = 90;
    public int balasRecarga = 300;

    float taxaDeDisparo = 0.1f;

    private Animator anim;
    public Animator animPistola;
    public Animator animRifle;

    private bool estaRecarregando = false;
	


	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        somDisparo = GetComponent<AudioSource>();
        balasTXT = GameObject.FindGameObjectWithTag("BalasTXT").GetComponent<Text>();
        recargaTXT = GameObject.FindGameObjectWithTag("RecargaTXT").GetComponent<Text>();
        balasTXT.text = balas.ToString();
        recargaTXT.text = balasRecarga.ToString();

        anim = animRifle;

    }
	
	// Update is called once per frame
	void Update () {
        Recarga();

        if(taxaDeDisparo < 0.01f)
        {
            DisparoRifle();
            taxaDeDisparo = 0.1f;
        }

        taxaDeDisparo -= 0.012f;

    }

    private void FixedUpdate()
    {
        estaRecarregando = anim.GetCurrentAnimatorStateInfo(0).IsName("reload");
    }

    void DisparoPistola()
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

    void DisparoRifle()
    {
        if (Input.GetMouseButton(0) && balas > 0 && !estaRecarregando)
        {

            anim.CrossFadeInFixedTime("fire", 0.1f);
            somDisparo.Play();
            balas--;
            AtualizaTextos(balas, balasRecarga);
            RaycastHit objeto;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out objeto))
            {
                if (objeto.transform.CompareTag("Zombie"))
                {
                    objeto.transform.gameObject.GetComponent<Zombie>().vida -= 15;
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
        if (Input.GetKeyDown(KeyCode.R) && balasRecarga > 0 && balas < tamanhoDoPente && !anim.GetCurrentAnimatorStateInfo(0).IsName("fire"))
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
