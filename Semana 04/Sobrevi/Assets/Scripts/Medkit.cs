using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

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
            if(obj.gameObject.GetComponent<Jogador>().vida < 0.9f)
            {
                obj.gameObject.GetComponent<Jogador>().vida += 0.1f;
                Destroy(gameObject);
            }
        }       
    }

}
