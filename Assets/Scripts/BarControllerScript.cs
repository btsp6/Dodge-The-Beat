using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EzySlice;

public class BarControllerScript : MonoBehaviour
{
    public GameObject bar_prefab;
    public GameObject wall_prefab;
    public GameObject sliced_object;
    public GameObject masker_prefab;

    public const int NUMBER_OF_BARS = 12;
    public const int NUMBER_OF_WALLS = 64;

    public float INITIAL_RADIUS;
    public float DELTA_RADIUS;

    private float radius;
    private GameObject masker;
    private GameObject bar;
    private GameObject[] bar_list;
    private GameObject[] walls;



    // Start is called before the first frame update
    void Start()
    {
        radius = INITIAL_RADIUS;

        sliced_object.GetComponent<Transform>().localScale = new Vector3(radius, radius, 1);
        SliceBars();

        bar_list = new GameObject[NUMBER_OF_BARS];
        walls = new GameObject[NUMBER_OF_WALLS];
        for(int i = 0; i < NUMBER_OF_BARS; i++)
        {
            bar_list[i] = (GameObject)Instantiate(
                bar, 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS) * (0.1F * Vector3.right) + Vector3.forward*10, 
                Quaternion.Euler(0, 0, 360F*i/NUMBER_OF_BARS));
            bar_list[i].GetComponent<Transform>().localScale = new Vector3(0.1F, 1, 1);
            bar_list[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            
        }
        for(int i = 0; i < NUMBER_OF_WALLS; i++)
        {

            walls[i] = (GameObject)Instantiate(
                wall_prefab,
                Quaternion.Euler(0, 0, 360F * i / NUMBER_OF_WALLS) * ((0.5F + radius) * Vector3.right),
                Quaternion.Euler(0, 0, 360F * i / NUMBER_OF_WALLS));
            walls[i].GetComponent<Renderer>().enabled = false;
        }

        bar.GetComponent<Renderer>().enabled = false;

        masker = (GameObject)Instantiate(masker_prefab, Vector3.forward*5, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        
        for (int i = 0; i < NUMBER_OF_BARS; i++)
        {
            SetBarHeight(i, MusicConversionScript.reducedData[i]);
        }
        masker.GetComponent<Transform>().localScale = new Vector3(2 * radius, 2 * radius, 1);
        for(int i = 0; i < NUMBER_OF_WALLS; i++)
        {
            walls[i].GetComponent<Transform>().position = Quaternion.Euler(0, 0, 360F * i / NUMBER_OF_WALLS) * ((0.5F + radius) * Vector3.right);
        }
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
        bar_list[index].GetComponent<Transform>().localScale = new Vector3((1 + DELTA_RADIUS*size) * 2*radius, (1 + DELTA_RADIUS * size) * 2*radius, 1);
        bar_list[index].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1, size*size, size*size, 1));
    }

}
