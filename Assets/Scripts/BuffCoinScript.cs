using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCoinScript : MonoBehaviour
{
    private Vector3 vecScale;
    private float camSize;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);

        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;  // при отдалении камеры размер модели будет увеличиваться
        vecScale.Set((camSize / 10f), 1, (camSize / 10f));
        transform.localScale = vecScale;
    }
}
