using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MusicConversionScript : MonoBehaviour
{

    AudioSource audioSource;

    const int N_BARS = BarControllerScript.NUMBER_OF_BARS;
    public const int N_FFT_SAMPLES = 8192; // must be a power of 2 in the interval [64, 8192]

    public static float[] fftData = new float[N_FFT_SAMPLES];
    public static float[] reducedData = new float[N_BARS + 1]; //+1 for bass

    private Scene CurrentScene;
    private AudioClip MyClip;
    private AudioClip ScannedClip;
    private GameObject CurrentButton;

    // Used to add OnSceneLoaded to scene manager
    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        audioSource = GetComponent<AudioSource>();
    }

    // Called when Scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopCoroutine("StopMusic");

        CurrentScene = scene;
        if (CurrentScene.name == "Menu")
        {
            audioSource.Stop();
            audioSource.pitch = 1;
        }

        if (CurrentScene.name == "LevelSelect")
        {
            audioSource.Stop();
            audioSource.pitch = 1;
            MyClip = null;
        }
        
        if (CurrentScene.name == "Game")
        {
            //this.gameObject;
            //Debug.Log("FFT data buffer length: " + fftData.Length);
            //AudioClip clip = Resources.Load<AudioClip>("Audio/A Life Full Of Joy.wma");
            //Debug.Log(clip);
            //Debug.Log("Playing music");

            audioSource.pitch = 1;
            audioSource.Play();
            ToneIndices.Initialize();
        }

        if (CurrentScene.name == "GameOver")
        {
            StartCoroutine("StopMusic");
        }
    }

    IEnumerator StopMusic()
    {
        while(audioSource.pitch > 0)
        {
            audioSource.pitch -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator GetAudioClip(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                ScannedClip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log(ScannedClip.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentScene.name == "LevelSelect")
        {
            GameObject selected_button = EventSystem.current.currentSelectedGameObject;
            if (selected_button != CurrentButton 
                && selected_button.GetComponentInChildren<Text>().text != null 
                && selected_button.GetComponentInChildren<Text>().text.Contains(".wav")
                && !selected_button.GetComponentInChildren<Text>().text.Contains(".meta")) 
            {
                CurrentButton = selected_button;
                Debug.Log("selected new button");
                string local_path = "file:///" + Application.streamingAssetsPath + "/" + selected_button.GetComponentInChildren<Text>().text;
                StartCoroutine("GetAudioClip", local_path);
            }
            
            ///Debug.Log(CurrentClip);
            //Debug.Log(MyClip);
            if(ScannedClip != MyClip)
            {
                MyClip = ScannedClip;
                audioSource.clip = MyClip;
                audioSource.Play();
            }
        }

        if (CurrentScene.name == "Game") {
            //Debug.Log("FFT data buffer length: "+fftData.Length);
            audioSource.GetSpectrumData(fftData, 0, FFTWindow.Blackman);


            var reducedDataCount = new int[N_BARS + 1]; //+1 for bass

            for (int i = 0; i < N_BARS; i++)
            {
                reducedData[i] = 0;
            }

            for (int i = 0; i < N_FFT_SAMPLES; i++)
            {
                var bar_index = ToneIndices.indices[i];

                reducedData[bar_index] += fftData[i];

                reducedDataCount[bar_index]++;
            }

            if (!audioSource.isPlaying)
            {
                SceneManager.LoadScene("LevelSelect");
            }

            /*var build = "";
            for (int i = 0; i < N_BARS; i++)
            {
                reducedData[i] /= reducedDataCount[i];
                build += reducedData[i] + " ";
            }
            Debug.Log(build);
            */
        }
    }
}


class ToneIndices
{
    public static int[] indices = new int[MusicConversionScript.N_FFT_SAMPLES];

    const float FREQUENCY_RANGE = 22050; // In Hz
    const double C2_FREQUENCY = 65.4064;

    const double SAMPLE_WIDTH = (double)FREQUENCY_RANGE / MusicConversionScript.N_FFT_SAMPLES;
    const double SAMPLE_HALF_WIDTH = SAMPLE_WIDTH / 2.0;


    const double CUTOFF_SAMPLE = C2_FREQUENCY / SAMPLE_WIDTH;
    readonly static double INTERVAL_HALF_FACTOR = System.Math.Pow(2.0, 1.0 / 24.0);

    public static void Initialize()
    {
        // First, the bass cutoff
        int i;
        for (i = 0; i < CUTOFF_SAMPLE; i++)
        {
            indices[i] = BarControllerScript.NUMBER_OF_BARS; // put bass at the end
        }

        for (double octaveC = C2_FREQUENCY; ; octaveC *= 2)
        {
            // This for loop handles one octave in one iteration.
            // octaveC is also the width of this octave

            double upperIntervalBound = octaveC * INTERVAL_HALF_FACTOR;

            for (int toneIndex = 0; toneIndex < 12; toneIndex++)
            {

                while (SAMPLE_WIDTH * i < upperIntervalBound)
                {
                    indices[i] = toneIndex;
                    i++;
                    if (i >= MusicConversionScript.N_FFT_SAMPLES)
                    {
                        // If we've reached the end of the sample space, we exit
                        goto END;
                    }
                }
                upperIntervalBound *= INTERVAL_HALF_FACTOR * INTERVAL_HALF_FACTOR;
            }

        }

    END:
        var build = "";
        /*for(int j = 0; j < MusicConversionScript.N_FFT_SAMPLES; j++)
        {
            build += j*SAMPLE_WIDTH + ": " + indices[j] + "\n";
        }*/

        Debug.Log(build);
        Debug.Log(FREQUENCY_RANGE + ": " + indices[MusicConversionScript.N_FFT_SAMPLES - 1] + "\n");
    }
}