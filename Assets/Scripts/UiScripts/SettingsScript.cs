using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SettingsScript : MonoBehaviour
{
    public Slider brightness;
    public Volume volume;
    public VolumeProfile volumeProfile;

    // Start is called before the first frame update
    void Start()
    {
        volumeProfile = volume.sharedProfile;
    }

    // Update is called once per frame
    void Update()
    {
        if(!volumeProfile.TryGet<ColorAdjustments>(out var exposure))
        {
            exposure = volumeProfile.Add<ColorAdjustments>(false);
        }

        exposure.postExposure.value = brightness.value;

       

    }
}
