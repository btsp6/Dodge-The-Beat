using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EzySlice;

public class BarControllerScript : MonoBehaviour
{
    public GameObject bar_prefab;
    public GameObject sliced_object;
    public GameObject masker_prefab;
    public int NUMBER_OF_BARS;
    public float INITIAL_RADIUS;
    public float DELTA_RADIUS;

    private float radius;
    private GameObject masker;
    private GameObject bar;
    private GameObject[] bar_list;


    
    // Start is called before the first frame update
    void Start()
    {
        radius = INITIAL_RADIUS;

        sliced_object.GetComponent<Transform>().localScale = new Vector3(radius, radius, 1);
        SliceBars();

        bar_list = new GameObject[NUMBER_OF_BARS];
        for(int i = 0; i < NUMBER_OF_BARS; i++)
        {
            bar_list[i] = (GameObject)Instantiate(
                bar, 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS) * (0.1F * Vector3.right) + Vector3.forward*10, 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS));
            bar_list[i].GetComponent<Transform>().localScale = new Vector3(0.1F, 1, 1);
            bar_list[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }

        bar.GetComponent<Renderer>().enabled = false;

        masker = (GameObject)Instantiate(masker_prefab, Vector3.forward*5, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {

        masker.GetComponent<Transform>().localScale = new Vector3(2*radius, 2*radius, 1);
        SetBarHeight(0, Random.value);
    }

    void SliceBars()
    {
        GameObject[] slices = sliced_object.SliceInstantiate(Vector3.zero, Quaternion.Euler(0, 0, -180F / NUMBER_OF_BARS) * Vector3.up);
        GameObject[] new_slices = slices[0].SliceInstantiate(Vector3.zero, Quaternion.Euler(0, 0, 180F / NUMBER_OF_BARS) * Vector3.down);
        
        bar = new_slices[0];
        
        Destroy(slices[0]);
        Destroy(slices[1]);
        Destroy(new_slices[1]);
        
    }

    void SetBarHeight(int index, float size)
    {
        // bar_list[index].GetComponent<Transform>().position = Quaternion.Euler(0, 0, 360F * index / NUMBER_OF_BARS) * ((radius + size / 2) * Vector3.right);
        bar_list[index].GetComponent<Transform>().localScale = new Vector3((1 + DELTA_RADIUS*size) * 2*radius, (1 + DELTA_RADIUS * size) * 2*radius, 1);
        bar_list[index].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, size, size, 1));
    }

}
