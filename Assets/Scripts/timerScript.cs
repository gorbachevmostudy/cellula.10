using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    [SerializeField] private Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("minutes") && PlayerPrefs.HasKey("seconds"))
        {
            int min = PlayerPrefs.GetInt("minutes");
            int sec = PlayerPrefs.GetInt("seconds");
            TimerText.text = "best alive time: " + min.ToString("D2") + ":" + sec.ToString("D2");
        }
        else
        {
            TimerText.text = "best alive time: 00:00";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
