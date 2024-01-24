using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class JoystickController : MonoBehaviour
{
    private float mass;
    private float lookspeed;
    public bool touch;


    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private float delta;
    // Start is called before the first frame update
    void Start()
    {
        delta = 6;
        lookspeed = 10f;
    }
    private void FixedUpdate()
    {

        mass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        //delta = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
        rb.velocity = new Vector3(joystick.Horizontal * delta, rb.velocity.y, joystick.Vertical * delta);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            rb.transform.position += rb.transform.forward * delta * Time.fixedDeltaTime;
        }
        //transform.position += transform.forward * delta * Time.fixedDeltaTime;
        if (touch == true)   // ускорение
        {
            //delta = 14 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));  
            delta = 8; // Горбачев 21.01.2024 убрал зависимость скорости от массы игрока
            //mass *= 0.9995f;
        }
        else
        {
            //delta = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
            delta = 5; // Горбачев 21.01.2024 убрал зависимость скорости от массы игрока
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
