using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public GameObject Light;
    public bool Isflickering = false;
    public float delay;
    public float random1;
    public float random2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Isflickering == false)
        {
            StartCoroutine(FlickerLight());
        }
    }
    IEnumerator FlickerLight()
    {
        Isflickering = true;
        Light.SetActive(false);
        delay = Random.Range(random1, random2);
        yield return new WaitForSeconds(delay);
        Light.SetActive(true);
        delay = Random.Range(random1, random2);
        yield return new WaitForSeconds(delay);
        Isflickering = false;

    }
}
