using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{

    private Vector3 vecScale;
    private float camSize;
    // Start is called before the first frame update
    void Start()
    {
        //Food = GameObject.FindGameObjectWithTag("Food");//.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.Rotate(new Vector3 (0, 90, 0) * Time.deltaTime);

        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;  // при отдалении камеры размер модели будет увеличиваться
        vecScale.Set((camSize / 40f), 0.125f, (camSize / 40f));
        transform.localScale = vecScale;

    }
}
