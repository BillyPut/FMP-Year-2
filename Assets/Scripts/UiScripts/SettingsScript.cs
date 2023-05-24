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

    public Toggle fullscreenToggle, vSyncToggle, shadowToggle;
    //bools for Fullscreen, Vsync and Shadows
    private bool isOnT, isOnV, isOnS;

    public Camera cam;
    private HDAdditionalCameraData hdCam;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public int currentResolutionIndex;

    public Button hfQButton, pQButton;
    public GameObject pointer1, pointer2;

    // Start is called before the first frame update
    void Start()
    {
        hdCam = cam.GetComponent<HDAdditionalCameraData>();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;             
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("Resolution") == true)
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }
        else
        {
            PlayerPrefs.SetInt("Resolution", currentResolutionIndex);
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }
        
        volumeProfile = volume.sharedProfile;

        if (PlayerPrefs.HasKey("Quality") == true)
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }
        else
        {
            PlayerPrefs.SetInt("Quality", 0);
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }

        if (PlayerPrefs.GetInt("IsOnT") == 1)
        {
            isOnT = true;
        }
        else
        {
            isOnT = false;   
        }

        fullscreenToggle.isOn = isOnT;

        if (PlayerPrefs.GetInt("IsOnV") == 1)
        {
            isOnV = true;
        }
        else
        {
            isOnV = false;
        }

        vSyncToggle.isOn = isOnV;

        if (PlayerPrefs.GetInt("IsOnS") == 1)
        {
            isOnS = true;
        }
        else
        {
            isOnS = false;
        }

        shadowToggle.isOn = isOnS;

        if (PlayerPrefs.HasKey("bright") == true)
        {
            brightness.value = PlayerPrefs.GetFloat("bright");
        }
        else
        {
            PlayerPrefs.SetFloat("bright", 0f);
            brightness.value = PlayerPrefs.GetFloat("bright");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SettingPlayerPrefs();
        //Enable camera override for shadow maps
        hdCam.renderingPathCustomFrameSettingsOverrideMask.mask[(int)FrameSettingsField.ShadowMaps] = true;
        
        if (PlayerPrefs.GetInt("Quallity") == 0)
        {
            pointer1.SetActive(true);
            pointer2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Quality") == 2)
        {
            pointer1.SetActive(false);
            pointer2.SetActive(true);
        }

     
    }

    public void SettingPlayerPrefs()
    {
        if (fullscreenToggle.isOn == true)
        {
            PlayerPrefs.SetInt("IsOnT", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsOnT", 0);
        }

        if (vSyncToggle.isOn == true)
        {
            PlayerPrefs.SetInt("IsOnV", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsOnV", 0);
        }

        if (shadowToggle.isOn == true)
        {
            PlayerPrefs.SetInt("IsOnS", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsOnS", 0);
        }
    }

    public void SetHighFidelity()
    {
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("Quality", 0);

        if (vSyncToggle.isOn == true)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

    }

    public void SetPerformant()
    {
        QualitySettings.SetQualityLevel(2);
        PlayerPrefs.SetInt("Quality", 2);

        if (vSyncToggle.isOn == true)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen == true)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetVsync(bool isVsync)
    {
        if (isVsync == true)
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }
    }

    public void SetShadows(bool isShadows)
    {
        //Turns the shadow override on and off
        if (isShadows == true)
        {
            hdCam.renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ShadowMaps, true);
        }
        else
        {
            hdCam.renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ShadowMaps, false);
        }
    }

    public void SetBrightness (float brightnessLevel)
    {
        if (!volumeProfile.TryGet<ColorAdjustments>(out var exposure))
        {
            exposure = volumeProfile.Add<ColorAdjustments>(false);
        }

        exposure.postExposure.value = brightnessLevel;
        PlayerPrefs.SetFloat("bright", brightness.value);
    }

}
