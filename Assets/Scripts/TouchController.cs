using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private float mass;
    //private Vector3 vecScale;
    public float delta;
    private float lookspeed;
    public bool touch;
    private float camSize;
    private float upDelta;


    void Start()
    {
        if (PlayerPrefs.HasKey("playerSpeed"))
        {
            upDelta = PlayerPrefs.GetFloat("playerSpeed");
        }
        else
        {
            PlayerPrefs.SetFloat("playerSpeed", 1);
            upDelta = PlayerPrefs.GetFloat("playerSpeed");
        }
        lookspeed = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // КОД ДЛЯ УМЕНЬШЕНИЯ СКОРОСТИ ОТ МАССЫ
        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;
        mass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (playerPlane.Raycast(ray, out hitdist))  // поворот и движение за мышью
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookspeed);
            transform.position += transform.forward * delta * Time.fixedDeltaTime;

        }
        
        if (touch == true)   // ускорение
        {
            delta = camSize * 1.4f * upDelta;
        }
        else
        {
            delta = camSize * 0.7f * upDelta;
        }
    }


    public void speedUp()
    {
        touch = true;
    }

    public void speedDown()
    {
        touch = false;
    }
}
