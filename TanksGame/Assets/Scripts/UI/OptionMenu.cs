using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    public AudioMixer beatbox;
    public Dropdown resolutiondropdown;
    
    Resolution[] resolutions;

    // Use this for initialization
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutiondropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentresolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {   currentresolution = i;  }
        }

        resolutiondropdown.AddOptions(options);
        resolutiondropdown.value = currentresolution;
        resolutiondropdown.RefreshShownValue();
    }

    public void setresolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setaudio(float volume)
    {
        beatbox.SetFloat("volume", volume);
    }

    public void setsfxaudio(float volume)
    {
        beatbox.SetFloat("sfxvolume", volume);
    }

    public void setdrivingaudio(float volume)
    {
        beatbox.SetFloat("drivingvolume", volume);
    }

    public void setmusicaudio(float volume)
    {
        beatbox.SetFloat("musicvolume", volume);
    }

    public void setquality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    public void SetFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    //public static int userchioce;
    //public void GetColorChoice(int _userchoice)
    //{
    //    userchioce = _userchoice;
    //}



}


//public static class datatransfer
//{
//    //public static TankColor tankColor;
//    //public static Color Color;

//    //public static void SetColor(int userindex)
//    //{
//    //    Color = tankColor.GetColor(userindex);
//    //}
//}