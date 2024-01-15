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
    // Start is called before the first frame update
    void Start()
    {
        timeBar = GetComponent<Image>();
        //time = 10f;
        //timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {

        buffActive = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().buffActive;
        if (buffActive)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / time;
        }

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