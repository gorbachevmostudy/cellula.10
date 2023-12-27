using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private float startPosX;// length;
    private float startPosZ;
    public GameObject camera;
    public float paralaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;

        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //float temp = camera.transform.position.x * (1 - paralaxEffect);
        float distX = camera.transform.position.x * paralaxEffect;
        float distZ = camera.transform.position.z * paralaxEffect;
        // ������� ��� � ��������� �� paralaxEffect
        transform.position = new Vector3(startPosX + distX, transform.position.y, startPosZ + distZ);

        //// ���� ������ ����������� ������, �� ������ startPos
        //if (temp > startPosX + length)
        //    startPosX += length;
        //else if (temp < startPosX - length)
        //    startPosX -= length;
    }
}
