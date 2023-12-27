using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform myObject;
    private Vector3 deltaPos;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float bottomLimit;
    [SerializeField] float topLimit;

    void Start()
    {
        deltaPos = transform.position - myObject.position;
    }

    void FixedUpdate()
    {
        transform.position = myObject.position + deltaPos;
        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bottomLimit, topLimit)


            );
    }
}
