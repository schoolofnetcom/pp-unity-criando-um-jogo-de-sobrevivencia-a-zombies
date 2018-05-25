using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Jogador : MonoBehaviour {

    Slider barraDeVida;
    Slider barraDeEnergia;
    public float vida = 1.0f;
    private float energia = 1.0f;

    float velocidade = 0f;
    FirstPersonController player;

	// Use this for initialization
	void Start () {
        barraDeVida = GameObject.FindGameObjectWithTag("BarraDeVida").GetComponent<Slider>();
        barraDeEnergia = GameObject.FindGameObjectWithTag("BarraDeEnergia").GetComponent<Slider>();
        player = GetComponent<FirstPersonController>();
        barraDeVida.value = vida;
        barraDeEnergia.value = energia;
    }
	
	// Update is called once per frame
	void Update () {
        barraDeVida.value = vida;
        barraDeEnergia.value = energia;
        velocidade = player.m_CharacterController.velocity.magnitude;
        StartCoroutine(energiaRotina());
    }

    private void LateUpdate()
    {
       if(energia > 0f && energia < 0.1f)
        {
            player.podeCorrer = false;
        }
        else
        {
            player.podeCorrer = true;
        }
    }


    IEnumerator energiaRotina()
    {
        if(velocidade > 5.2f)
        {
            energia -= 0.002f;
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            if(energia < 1.0f)
            {
                energia += 0.001f;
                yield return new WaitForSeconds(0.5F);
            }
        }

        yield return null;
    }

}
