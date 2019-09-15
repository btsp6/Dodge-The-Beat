using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    TextMesh textMesh;

    void Start()
    {
        //Debug.Log("Start of Countown");
        StartCountdown();
    }

    void OnEnable()
    {
        //Debug.Log("Enable of countdown");
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update of countdown");
    }
    public async Task StartCountdown()
    {
        //Debug.Log("3");
        await Task.Delay(1000);
        textMesh.text = "2";
        //Debug.Log("2");
        await Task.Delay(1000);
        textMesh.text = "1";
        //Debug.Log("1");
        await Task.Delay(1000);
        textMesh.text = "";
        //Debug.Log("0");
        GameObject.Find("MusicPlayer").GetComponent<MusicConversionScript>().StartMusic();
        //Debug.Log("GO");
    }
}
