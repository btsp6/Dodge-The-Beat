using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControllerScript : MonoBehaviour
{
    public GameObject bar_prefab;
    public int NUMBER_OF_BARS;
    public float INITIAL_RADIUS;

    private float radius;
    private GameObject[] bar_list;

    
    // Start is called before the first frame update
    void Start()
    {
        radius = INITIAL_RADIUS;

        bar_list = new GameObject[NUMBER_OF_BARS];
        for(int i = 0; i < NUMBER_OF_BARS; i++)
        {
            bar_list[i] = (GameObject)Instantiate(
                bar_prefab, 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS) * (radius * Vector3.right), 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS));
            bar_list[i].GetComponent<Transform>().localScale = new Vector3(0.1F, 1, 1);
            bar_list[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetBarHeight(0, Random.value);
    }

    void SetBarHeight(int index, float size)
    {
        bar_list[index].GetComponent<Transform>().position = Quaternion.Euler(0, 0, 360F * index / NUMBER_OF_BARS) * ((radius + size / 2) * Vector3.right);
        bar_list[index].GetComponent<Transform>().localScale = new Vector3(size, 1, 1);
    }

}
