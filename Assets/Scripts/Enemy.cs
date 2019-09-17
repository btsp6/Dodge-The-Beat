using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject bullet_prefab;
	public const int NUMBER_OF_BARS = BarControllerScript.NUMBER_OF_BARS; 
	public const int RADIUS = EnemyControllerScript.RADIUS;
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
        float average_amplitude = 0;
        for (int i = 0; i < NUMBER_OF_BARS; i++)
        {
            average_amplitude += MusicConversionScript.reducedData[i] / NUMBER_OF_BARS;
        }
        float val = MusicConversionScript.reducedData[index]*MusicConversionScript.SCALING_FACTOR;
		if (val > Mathf.Max(1.5f - 1f*average_amplitude, 1) * average_amplitude + 0.01 && timer == 0) {
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
