using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wall_prefab;
    public int number_of_walls = BarControllerScript.NUMBER_OF_BARS;

    private GameObject[] walls;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < number_of_walls; i++)
        {
            walls[i] = (GameObject)Instantiate(
                wall_prefab,
                Quaternion.Euler(0, 0, 360F * i / number_of_walls) * (0.5F * Vector3.right),
                Quaternion.Euler(0, 0, 360F * i / number_of_walls));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
