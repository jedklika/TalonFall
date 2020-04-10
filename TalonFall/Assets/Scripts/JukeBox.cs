using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;
    public bool HasPlayed;
    public bool isTriggered;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        HasPlayed = false;
        Light1.SetActive(false);
        Light2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HasPlayed == false && isTriggered)
        {
            StartCoroutine(On());
        }
    }
    IEnumerator On()
    {
        Light1.SetActive(true);
        Light2.SetActive(true);
        yield return new WaitForSeconds(delay);
        HasPlayed = true;
        Light1.SetActive(false);
        Light2.SetActive(false);
    }
}
