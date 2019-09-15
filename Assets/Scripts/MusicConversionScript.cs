using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicConversionScript : MonoBehaviour
{

    AudioSource audioSource;

    const int N_BARS = BarControllerScript.NUMBER_OF_BARS;
    const int N_FFT_SAMPLES = 1 << (N_BARS+2); // must be a power of 2 in the interval [64, 8192]

    public static float[] fftData = new float[N_FFT_SAMPLES];
    public static float[] reducedData = new float[N_BARS];
    
    // Start is called before the first frame update
    void Start() {
        //this.gameObject;
        Debug.Log("FFT data buffer length: " + fftData.Length);
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Audio/A Life Full Of Joy.wma");
        Debug.Log(clip);

        //audioSource.clip = clip;
        audioSource.Play();
        Debug.Log("Playing music");
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("FFT data buffer length: "+fftData.Length);
        audioSource.GetSpectrumData(fftData, 0, FFTWindow.Blackman);

        int powerOfTwo = 1;
        for (int i = 0; i < N_BARS; i++)
        {
            int count = 2 << i; // 2 ^ (i + 1);
            float sum = 0;
            for (int j = 0; j < count; j++)
            {
                sum += fftData[count - 2 + j];
            }
            reducedData[i] = sum / count;
        }
        if(Random.value*100  <1 )
        {

            string build = "";
            foreach (float value in fftData)
            {
                build += value+ " ";
            }
            //Debug.Log(build);
            
        }
    }
}
