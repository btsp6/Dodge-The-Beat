using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject bullet_prefab;
	public const int NUMBER_OF_BARS = BarControllerScript.NUMBER_OF_BARS;
	private int timer;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
    	timer += 1;
	   	if (timer % 100 == 0) {
			GameObject bullet = (GameObject) Instantiate(
				bullet_prefab,
				transform
			);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			rb.velocity = -transform.position;
			//bullet.velocity = Vector3(1, 0, 0);
		}
    }

	void LateUpdate() {
		
	}
}
