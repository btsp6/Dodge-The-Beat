using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public int MAX_HEALTH;
	public static int health;

    void Start() {
		health = MAX_HEALTH;
    	Debug.Log(health);    
    }

    void Update()
    {
        
    }

	public void BulletHit() {
		health -= 1;
		Debug.Log(health);
		if (health <= 0) {
			Dead();
		}
	}

	private void Dead() {
		Debug.Log("Dead");
		SceneManager.LoadScene("GameOver");
	}
}
