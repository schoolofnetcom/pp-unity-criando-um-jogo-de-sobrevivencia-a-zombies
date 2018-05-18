using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    NavMeshAgent agente;
    GameObject player;
    public bool deveSeguir = false;
    bool estaNaAreaDeDano = false;
    public int vida = 100;
    public Animator anim;
   
	// Use this for initialization
	void Start () {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (deveSeguir)
        {
            //Despause
            agente.isStopped = false;
            agente.destination = player.transform.position;
        }
        else
        {
            // Pause
            agente.isStopped = true;
        }

        Morreu();
    }

    private void OnTriggerEnter(Collider other)
    {
        deveSeguir = false;
        estaNaAreaDeDano = true;
        StartCoroutine(Ataque());
    }

    private void OnTriggerExit(Collider other)
    {
        deveSeguir = true;
        estaNaAreaDeDano = false;
        StopCoroutine(Ataque());
    }


    IEnumerator Ataque()
    {
        while (estaNaAreaDeDano)
        {
            player.GetComponent<Jogador>().vida -= 0.02f;
            yield return new WaitForSeconds(1.5f);
        }
    }

    void Morreu()
    {
        if(vida < 1)
        {

            anim.SetBool("Morreu", true);
            Destroy(gameObject, 2f);
        }
    }
}
