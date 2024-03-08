using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundScript_BG : MonoBehaviour
{
    public GameObject BGMusic1;
    private AudioSource audioSrc1;
    public GameObject[] objs11;

    void Awake()
    {
        objs11 = GameObject.FindGameObjectsWithTag("Sound");
        if (objs11.Length == 0)
        {
            BGMusic1 = Instantiate(BGMusic1);
            BGMusic1.name = "BGMusic1";
            DontDestroyOnLoad(BGMusic1.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            BGMusic1 = GameObject.Find("BGMusic1");
        }
        
    }
    void Start()
    {
        audioSrc1 = BGMusic1.GetComponent<AudioSource>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
            audioSrc1.mute = true;
        else
            audioSrc1.mute = false;
    }

    void Destroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}