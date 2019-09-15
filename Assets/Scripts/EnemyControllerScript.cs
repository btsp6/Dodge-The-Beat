using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
	public GameObject enemy_prefab;
    public const int NUMBER_OF_ENEMIES = BarControllerScript.NUMBER_OF_BARS;
	public const int RADIUS = 4;

	private GameObject[] enemy_list;

    // Start is called before the first frame update
    void Start()
    {
        enemy_list = new GameObject[NUMBER_OF_ENEMIES];

        for(int i = 0; i < NUMBER_OF_ENEMIES; i++)
        {
			Debug.Log(i);
            enemy_list[i] = Instantiate(
                enemy_prefab, 
                RADIUS * new Vector3(Mathf.Cos(2*Mathf.PI*i/NUMBER_OF_ENEMIES), Mathf.Sin(2 * Mathf.PI * i / NUMBER_OF_ENEMIES), 0), 
                Quaternion.Euler(0, 0, 360.0F*i/NUMBER_OF_ENEMIES));
			enemy_list[i].GetComponent<Enemy>().index = i;
            enemy_list[i].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
