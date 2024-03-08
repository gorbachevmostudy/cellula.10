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
    private float upDelta;
    private float camSize;
    // Start is called before the first frame update
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
    private void FixedUpdate()
    {
        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;
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
            delta = camSize * 1.4f * upDelta;//delta = 8;//camSize * 1.6f; // Горбачев 21.01.2024 убрал зависимость скорости от массы игрока
            //mass *= 0.9995f;
        }
        else
        {
            //delta = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
            delta = camSize * 0.7f * upDelta;//camSize; // Горбачев 21.01.2024 убрал зависимость скорости от массы игрока
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
