using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    
    GameObject _PauseMenu;
    bool paused;
    //[SerializeField]
    public static string playerName = "Player";
    public static int playerLevel = 1;
    public GameObject Player;
    public AudioSource music;

    // Use this for initialization
    void Start () {
        paused = false;
        _PauseMenu = GameObject.Find("PauseMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if(paused)
        {
            _PauseMenu.SetActive(true);
            Time.timeScale = 0;
            music.mute = true;
        }
        else if(!paused)
        {
            _PauseMenu.SetActive(false);
            Time.timeScale = 1;
            music.mute = false;
        }
	}

    public void Resume()
    {
        paused = false;
    }

    public void MainMenu()
    {
        Application.LoadLevel("StartScreen");
    }

    public void Save()
    {
        //PlayerPrefs.SetInt("saveScreen", Application.loadedLevel);

        //===================
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //using (FileStream fsScene = new FileStream("scenesave.bin", FileMode.Create, FileAccess.Write))
        //{
        //    Scene scene = SceneManager.GetActiveScene();
        //    binaryFormatter.Serialize(fsScene, scene.name);
        //    Debug.Log("Scene: " + scene.name);
        //}
        Scene scene = SceneManager.GetActiveScene();
        System.IO.StreamWriter file = new System.IO.StreamWriter("scene.txt");
        file.Write(scene.name);

        file.Close();

        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Create, FileAccess.Write))
        {
            //binaryFormatter.Serialize(fs, GameData.playerName);
            //binaryFormatter.Serialize(fs, GameData.playerLevel);

            binaryFormatter.Serialize(fs, GameObject.FindGameObjectWithTag("Player").transform.position.x);
            binaryFormatter.Serialize(fs, GameObject.FindGameObjectWithTag("Player").transform.position.y);
            
            //PlayerPrefs.SetFloat("Player", GameObject.FindGameObjectWithTag("Player").transform.position.x);
            //PlayerPrefs.SetFloat("Player", GameObject.FindGameObjectWithTag("Player").transform.position.y);

            Debug.Log("X = " + GameObject.FindGameObjectWithTag("Player").transform.position.x);
            Debug.Log("Y = " + GameObject.FindGameObjectWithTag("Player").transform.position.y);
            
        }

       
        //===================



    }

    public void Load()
    {
        //Application.LoadLevel(PlayerPrefs.GetInt("saveScreen"));

        //============
        //if (!File.Exists("gamesave.bin") || !File.Exists("scenesave.txt"))
        //    return;

        using (StreamReader sr = new StreamReader("scene.txt"))
        {
            // Read the stream to a string, and write the string to the console.
            String line = sr.ReadToEnd();
            Application.LoadLevel(line);
            
            Debug.Log("Scene = " + line);
        }
        Time.timeScale = 0;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
        {
            //GameData.playerName = (string)binaryFormatter.Deserialize(fs);
            //GameData.playerLevel = (int)binaryFormatter.Deserialize(fs);

            float x = (float)binaryFormatter.Deserialize(fs);
            float y = (float)binaryFormatter.Deserialize(fs);
            Player.transform.position = new Vector2(x, y);


            //float x = PlayerPrefs.GetFloat("Player");
            //float y = PlayerPrefs.GetFloat("Player");

            //Player.transform.position = new Vector2(x, y);

            Debug.Log("X = " + x);
            Debug.Log("Y = " + y);

            //using (FileStream fsScene = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
            //{
            //    string scene = (string)binaryFormatter.Deserialize(fsScene);

            //    //Application.LoadLevel(scene);
            //    EditorSceneManager.LoadScene(scene);
            //    Debug.Log("Scene = " + scene);
            //}
            #region vùng load position
            //using (FileStream fs = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
            //{
            //    //GameData.playerName = (string)binaryFormatter.Deserialize(fs);
            //    //GameData.playerLevel = (int)binaryFormatter.Deserialize(fs);

            //    float x = (float)binaryFormatter.Deserialize(fs);
            //    float y = (float)binaryFormatter.Deserialize(fs);
            //    Player.transform.position = new Vector2(x, y);


            //    //float x = PlayerPrefs.GetFloat("Player");
            //    //float y = PlayerPrefs.GetFloat("Player");

            //    //Player.transform.position = new Vector2(x, y);

            //    Debug.Log("X = " + x);
            //    Debug.Log("Y = " + y);

            //}
            #endregion
            //==================
        }

        //public void OnLevelWasLoaded(int level)
        //{
        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (FileStream fs = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
        //    {
        //        //GameData.playerName = (string)binaryFormatter.Deserialize(fs);
        //        //GameData.playerLevel = (int)binaryFormatter.Deserialize(fs);

        //        float x = (float)binaryFormatter.Deserialize(fs);
        //        float y = (float)binaryFormatter.Deserialize(fs);
        //        Player.transform.position = new Vector2(x, y);


        //        //float x = PlayerPrefs.GetFloat("Player");
        //        //float y = PlayerPrefs.GetFloat("Player");

        //        //Player.transform.position = new Vector2(x, y);

        //        Debug.Log("X = " + x);
        //        Debug.Log("Y = " + y);

        //    }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
