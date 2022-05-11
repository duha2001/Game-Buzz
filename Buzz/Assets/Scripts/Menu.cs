using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject mainMenuHolder;
    public GameObject optionMenuHolder;
    public GameObject levelMenuHolder;

    public Slider[] volumeSlider;
    public Toggle[] resolutionToggle;
    public int[] screenWidth;
    int activeScreenResIndex;

    public void Play()
    {
        //EditorSceneManager.LoadScene("Level1");
        Application.LoadLevel("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionMenu()
    {
        mainMenuHolder.SetActive(false);
        optionMenuHolder.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionMenuHolder.SetActive(false);
        levelMenuHolder.SetActive(false);
    }

    public void MainMenuInGame()
    {
        mainMenuHolder.SetActive(true);
        optionMenuHolder.SetActive(false);
    }

    public void LevellMenu()
    {
        mainMenuHolder.SetActive(false);
        levelMenuHolder.SetActive(true);
    }

    public void SetScreenResulution(int i)
    {
        if(resolutionToggle[i].isOn)
        {
            activeScreenResIndex = i;
            float aspecRatio = 16 / 9f;
            Screen.SetResolution(screenWidth[i], (int)(screenWidth[i] / aspecRatio), false);
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        for(int i = 0; i < resolutionToggle.Length; i++)
        {
            resolutionToggle[i].interactable = !isFullScreen;
        }

        if(isFullScreen)
        {
            Resolution[] allRasolution = Screen.resolutions;
            Resolution maxResolution = allRasolution[allRasolution.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResulution(activeScreenResIndex);
        }
    }

    public void SetMasTerVolume(float value)
    {
        
    }

    public void SetMusicVolume(float value)
    {

    }

    public void SetSfxVolume(float value)
    {

    }

    public void LoadLevel(string level)
    {
        //EditorSceneManager.LoadScene(level);
        Application.LoadLevel(level);
    }
}