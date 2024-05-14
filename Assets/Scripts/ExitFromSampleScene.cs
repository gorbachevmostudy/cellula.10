using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitFromSampleScene : MonoBehaviour
{
    float playerMass;
    // Start is called before the first frame update
    public void OnButtonEnter()
    {
        int min = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().min;
        int sec = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().sec;

        int savedTime = PlayerPrefs.GetInt("minutes") * 60 + PlayerPrefs.GetInt("seconds");
        if (min * 60 + sec > savedTime)
        {
            PlayerPrefs.SetInt("minutes", min);
            PlayerPrefs.SetInt("seconds", sec);
        }

        playerMass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        //YandexGame.NewLeaderboardScores("massLeader", (int)playerMass);
    }
}
