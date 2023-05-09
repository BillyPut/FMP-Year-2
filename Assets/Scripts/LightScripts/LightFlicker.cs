using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light lightChange;
    public float turnOnTime, turnOffTime, turnOnTime2, turnOffTime2;
    public GameObject bulb;
    [HideInInspector]
    public Material material;
    [HideInInspector]
    public Color ogColor, ogColorE;

    void Awake()
    {
        lightChange = GetComponent<Light>();
        material = bulb.GetComponent<Renderer>().material;
        ogColor = material.GetColor("_BaseColor");
        ogColorE = material.GetColor("_EmissiveColor");
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        //Loops Infinitely
        while(true)
        {
            yield return new WaitForSeconds(turnOnTime);

            lightChange.enabled = false;
            material.SetColor("_BaseColor", Color.black);
            material.SetColor("_EmissiveColor", Color.black);

            yield return new WaitForSeconds(turnOffTime);

            lightChange.enabled = true;
            material.SetColor("_BaseColor", ogColor);
            material.SetColor("_EmissiveColor", ogColorE);

            yield return new WaitForSeconds(turnOnTime2);

            lightChange.enabled = false;
            material.SetColor("_BaseColor", Color.black);
            material.SetColor("_EmissiveColor", Color.black);

            yield return new WaitForSeconds(turnOffTime2);

            lightChange.enabled = true;
            material.SetColor("_BaseColor", ogColor);
            material.SetColor("_EmissiveColor", ogColorE);

        }

    }
}
