using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public int MAX_HEALTH, REGENERATE_TIMER;
	public static int health;
	private float timer;

    void Start() {
		health = MAX_HEALTH;
    	timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > REGENERATE_TIMER)
        {
            timer = 0;
            Regenerate();
        }
    }

	public void BulletHit() {
		health -= 1;
		if (health <= 0) {
			Dead();
		}
	}

	private void Dead() {
		SceneManager.LoadScene("GameOver");
	}
	
	public void Regenerate() {
		if (health < MAX_HEALTH) health++;
	}
}
