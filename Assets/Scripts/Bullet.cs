using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

	void FixedUpdate() {
		
	}

    // Update is called once per frame
    void Update() {
        
    }

	private void OnTriggerEnter(Collider other) {
		if (other.name == "Player") {
			other.gameObject.GetComponent<PlayerHealth>().BulletHit();
			Destroy(gameObject);
		}
	}
}
