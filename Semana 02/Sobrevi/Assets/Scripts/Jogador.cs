using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour {

    Slider barraDeVida;
    public float vida = 1.0f;


	// Use this for initialization
	void Start () {
        barraDeVida = GameObject.FindGameObjectWithTag("BarraDeVida").GetComponent<Slider>();
        barraDeVida.value = vida;
    }
	
	// Update is called once per frame
	void Update () {
        barraDeVida.value = vida;
	}
}
