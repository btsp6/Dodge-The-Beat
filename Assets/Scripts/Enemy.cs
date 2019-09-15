﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject bullet_prefab;
	public const int NUMBER_OF_BARS = BarControllerScript.NUMBER_OF_BARS; 
	public float THRESHOLD;
	public int COOLDOWN, index;
	private int timer;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

	void LateUpdate() {
		if (timer > 0) timer--;
		float val = MusicConversionScript.reducedData[index]*50;
		if (val > THRESHOLD && timer == 0) {
			timer = COOLDOWN;
			GameObject bullet = (GameObject) Instantiate(
				bullet_prefab,
				transform
			);
			bullet.GetComponent<Bullet>().index = index;
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			rb.velocity = -transform.position;
		}
	}
}
