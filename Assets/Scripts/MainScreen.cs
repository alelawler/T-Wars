using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

    public GameObject loadingImage;
    public AudioSource musicSource;

    // Use this for initialization
    void Start ()
    {
        musicSource.Play();
    }

    public void LoadScene(int level)
    {
        loadingImage.SetActive(true);
        DontDestroyOnLoad(musicSource);
        SceneManager.LoadScene(level);        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
