using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
	public int NUM_LIVES;
	public float LENGTH; 
	int current;

    // Start is called before the first frame update
    void Start()
    {
 	    current = NUM_LIVES;   
		transform.localScale = new Vector3(LENGTH*current/NUM_LIVES, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
		current = PlayerHealth.health;
		transform.localScale = new Vector3(LENGTH*current/NUM_LIVES, 0.5f, 1);
		transform.position = new Vector3(4.5f + LENGTH*current/NUM_LIVES/2, 4, 0);
    }
}
