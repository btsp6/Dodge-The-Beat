using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
	public GameObject enemy_prefab;
	public int NUMBER_OF_ENEMIES;
	public int RADIUS;

	private GameObject[] enemy_list;

    // Start is called before the first frame update
    void Start()
    {
        enemy_list = new GameObject[NUMBER_OF_ENEMIES];

        for(int i = 0; i < NUMBER_OF_ENEMIES; i++)
        {
            enemy_list[i] = Instantiate(
                enemy_prefab, 
                RADIUS * new Vector3(Mathf.Cos(2*Mathf.PI*i/NUMBER_OF_ENEMIES), Mathf.Sin(2 * Mathf.PI * i / NUMBER_OF_ENEMIES), 0), 
                Quaternion.Euler(0, 0, 360.0F*i/NUMBER_OF_ENEMIES));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
