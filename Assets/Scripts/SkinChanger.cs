using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    bool Change;
    void Start()
    {
        AgarController.camGrown.AddListener(SkinChange);
        //StartCoroutine("Timer");
    }
    
    private IEnumerator Timer()
    {
        while (Change)
        {
            Debug.Log("dd");
            yield return new WaitForSeconds(5f);
            
        }
    }

    private void SkinChange(bool camGrown)
    {
        Change = camGrown;



        StartCoroutine("Timer");
        //Change = 
    }

}
