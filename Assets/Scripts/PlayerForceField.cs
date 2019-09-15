using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForceField : MonoBehaviour
{
    public GameObject ForceFieldFront;
    public GameObject ForceFieldBack;
    public float MAX_SIZE;
    public float COOLDOWN_TIME;
    public float DELAY_TIME;
    public float FORCE_FIELD_SPEED;

    private bool deployed;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), ForceFieldFront.GetComponent<Collider>(), true);
        ForceFieldFront.SetActive(false);
        ForceFieldBack.SetActive(false);
        deployed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && !deployed){
            deployed = true;
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0.5F, 0));
            StartCoroutine("CoolDown");
            StartCoroutine("StartFront");
            StartCoroutine("WaitForStartBack");
        }
    }

    IEnumerator CoolDown()
    {
        Debug.Log("CoolDown");
        yield return new WaitForSeconds(COOLDOWN_TIME);
        deployed = false;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        ForceFieldFront.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        ForceFieldBack.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    IEnumerator StartFront()
    {
        Debug.Log("StartFront");
        ForceFieldFront.SetActive(true);
        while(ForceFieldFront.GetComponent<Transform>().localScale.x < MAX_SIZE)
        {
            ForceFieldFront.GetComponent<Transform>().localScale += new Vector3(1, 1, 0) * FORCE_FIELD_SPEED * Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator WaitForStartBack()
    {
        Debug.Log("WaitForStartBack");

        yield return new WaitForSeconds(DELAY_TIME);

        StartCoroutine("StartBack");

    }

    IEnumerator StartBack()
    {
        Debug.Log("StartBack");
        ForceFieldBack.SetActive(true);
        while (ForceFieldBack.GetComponent<Transform>().localScale.x < MAX_SIZE)
        {
            ForceFieldBack.GetComponent<Transform>().localScale += new Vector3(1, 1, 0) * FORCE_FIELD_SPEED * Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        ForceFieldFront.SetActive(false);
        ForceFieldBack.SetActive(false);
    }
}
