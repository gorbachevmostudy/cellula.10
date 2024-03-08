using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffScript : MonoBehaviour
{
    Image timeBar;
    private float time;
    private float timeLeft;
    private bool buffActive;
    private bool buffActiveRepeat;
    private Vector3 vecScale;
    private float camSize;
    // Start is called before the first frame update
    void Start()
    {
        timeBar = GetComponent<Image>();
        //time = 10f;
        //timeLeft = time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        buffActive = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffActive;
        buffActiveRepeat = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffActiveRepeat;
        if (buffActiveRepeat)
        {
            timeBar.fillAmount = 1;
            time = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffTimer; 
            timeLeft = time;
        }
        if (buffActive)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / time;
        }
        //else if (buffActive)
        //{
        //    timeBar.fillAmount = 0;
        //    time = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffTimer; // тестирую взятие тайма из скрипта
        //    timeLeft = time;
        //}
        else 
        {
            timeBar.fillAmount = 0;
            time = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffTimer; // тестирую взятие тайма из скрипта
            timeLeft = time;
        }

    }
    //void OnTriggerEnter(Collider col)  // поедание челов и еды
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        time = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffTimer; // тестирую взятие тайма из скрипта
    //        timeLeft = time;
    //    }
    //}
}