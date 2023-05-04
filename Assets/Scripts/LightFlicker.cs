using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light lightChange;
    public float turnOnTime, turnOffTime, turnOnTime2, turnOffTime2;

    void Awake()
    {
        lightChange = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        //Loops Infinitely
        while(true)
        {
            yield return new WaitForSeconds(turnOnTime);

            lightChange.enabled = false;

            yield return new WaitForSeconds(turnOffTime);

            lightChange.enabled = true;

            yield return new WaitForSeconds(turnOnTime2);

            lightChange.enabled = false;

            yield return new WaitForSeconds(turnOffTime2);

            lightChange.enabled = true;

        }

    }
}
