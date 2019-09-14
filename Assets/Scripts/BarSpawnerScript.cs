using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSpawnerScript : MonoBehaviour
{
    public GameObject bar_prefab;
    public int NUMBER_OF_BARS;
    public int RADIUS;

    private GameObject[] bar_list;

    
    // Start is called before the first frame update
    void Start()
    {
        bar_list = new GameObject[NUMBER_OF_BARS];
        for(int i = 0; i < NUMBER_OF_BARS; i++)
        {
            bar_list[i] = (GameObject)Instantiate(
                bar_prefab, 
                RADIUS * new Vector3(Mathf.Cos(2*Mathf.PI*i/NUMBER_OF_BARS), Mathf.Sin(2 * Mathf.PI * i / NUMBER_OF_BARS), 0), 
                Quaternion.Euler(0, 0, 360.0F*i/NUMBER_OF_BARS));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
