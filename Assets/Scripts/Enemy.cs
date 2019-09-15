using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject bullet_prefab;
	public const int NUMBER_OF_BARS = BarControllerScript.NUMBER_OF_BARS; 
	public const int RADIUS = EnemyControllerScript.RADIUS;
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
        float scaled_average_amplitude = 0;
        for (int i = 0; i < NUMBER_OF_BARS; i++)
        {
            scaled_average_amplitude += MusicConversionScript.reducedData[i] / NUMBER_OF_BARS * BarControllerScript.SCALING_FACTOR;
        }
        float val = MusicConversionScript.reducedData[index]*BarControllerScript.SCALING_FACTOR;
		if (val > Mathf.Max(THRESHOLD - 2*scaled_average_amplitude, 1) * scaled_average_amplitude && timer == 0) {
			timer = COOLDOWN;
			float sector = 2*Mathf.PI/NUMBER_OF_BARS;
			float angle = index*sector + Random.Range(-sector/2, sector/2);
			GameObject bullet = (GameObject) Instantiate(
				bullet_prefab,
                RADIUS * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0), 
                Quaternion.Euler(0, 0, 360.0F*index/NUMBER_OF_BARS)
			);
			bullet.GetComponent<Bullet>().index = index;
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			rb.velocity = -transform.position;
		}
	}
}
