using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private Material material;
    private float startPosX;// length;
    private float startPosZ;
    public GameObject camera;
    public float paralaxEffect;
    private Vector3 vecScale;
    private float camSize;


    void Start()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;

        MeshRenderer render = GetComponent<MeshRenderer>();
#if UNITY_EDITOR
        material = render.sharedMaterial;
#else
            material = render.material;
#endif

        //length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //float temp = camera.transform.position.x * (1 - paralaxEffect);
        float distX = camera.transform.position.x * paralaxEffect;
        float distZ = camera.transform.position.z * paralaxEffect;
        // двигаем фон с поправкой на paralaxEffect
        transform.position = new Vector3(startPosX + distX, transform.position.y, startPosZ + distZ);

        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;  // при отдалении камеры размер модели будет увеличиваться
        vecScale.Set((camSize * 5f), 1, (camSize * 5f));
        transform.localScale = vecScale;

        material.mainTextureScale = new Vector2(camSize, camSize);



        //// если камера перескочила спрайт, то меняем startPos
        //if (temp > startPosX + length)
        //    startPosX += length;
        //else if (temp < startPosX - length)
        //    startPosX -= length;
    }
}
