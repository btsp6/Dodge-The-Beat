using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
	public int index;
	private int timer;

    // Start is called before the first frame update
    void Start() {
    	timer = 0;
    }

	void FixedUpdate() {
		timer++;
	}

    // Update is called once per frame
    void Update() {
        
    }

	private void OnTriggerEnter(Collider other) {
		if (other.name == "Player") {
			other.gameObject.GetComponent<PlayerHealth>().BulletHit();
			Destroy(gameObject);
		}
		// timer > 10 is extremely jank
		else if (timer > 10 && other.name != "Bullet(Clone)") {
			Destroy(gameObject);
		}
	}
}
