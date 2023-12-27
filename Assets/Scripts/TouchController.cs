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


    void Start()
    {
        delta = 6;
        lookspeed = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // КОД ДЛЯ УМЕНЬШЕНИЯ СКОРОСТИ ОТ МАССЫ
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
            delta = 14 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
            mass *= 0.999f;
        }
        else
        {
            delta = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
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
