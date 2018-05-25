using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour {


    public int balasQtd = 10;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.transform.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Mao").GetComponent<Tiro>().balasRecarga += balasQtd;
            Destroy(gameObject);
        }
    }

}
