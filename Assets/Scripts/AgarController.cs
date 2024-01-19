using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AgarController : MonoBehaviour
{

    //public float delta;
    [SerializeField] Text coins;
    public int coinsCount;

    public float mass;
    public GameObject sphere;
    public float alpha;
    public Rigidbody rb;
    public Text textScore;
    public Camera cam;
    public float camSize;
    private float lookspeed;
    private Vector3 randVec;
    private Vector3 vecScale;
    private float massCoin;
    float massEnemy; 
    float massEnemyBoss;
    private bool touch;
    public GameObject deathScreen;
    private float spawnRange;

    public float UpdateIntervalSec = 3f / 3f; // 3 (c����� 1 ���) ���� � �������
    public float ZoomSpeed = 1f; // �������� ���� ������

    public float buffTimer; // ����� �������� ����
    public bool buffActive;    // ������ - ���������� ����

    private float _time;
    private bool _zoomIsSmall = true;
    private Camera _camera;

    private float doubleClickTime = 0.5f, lastClickTime;   // ���� ���� ��� ���������


    private void Start()
    {
        if (PlayerPrefs.HasKey("coinsFinal"))
        {
            coinsCount = PlayerPrefs.GetInt("coinsFinal");
        }
        else
        {
            coinsCount = 0;
        }
        coins.text = "Money: " + coinsCount.ToString();
        //delta = 6;
        mass = 50f;
        //lookspeed = 10f;
        vecScale.Set(1, 1, 1);
        camSize = 5f;
        
        buffActive = false;
        buffTimer = 10f;
        spawnRange = 99f;
        //StartCoroutine("DoMessage");

    }

    void FixedUpdate()
    {

        if (mass < 400f)   // ���������� �������� ��� � ������ ����
        {
            massCoin = 6f;
        } else massCoin = 3f;


        // ��� ��� ���������� �������� �� �����

        //Plane playerPlane = new Plane(Vector3.up, transform.position);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float hitdist = 0;
        //if (playerPlane.Raycast(ray, out hitdist))  // ������� � �������� �� �����
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitdist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookspeed);
        //    transform.position += transform.forward * delta * Time.fixedDeltaTime;

        //}

        vecScale.Set((mass / 200 + 0.95f), 1, (mass / 200 + 0.95f));  // ������� �� �����
        transform.localScale = vecScale;

        mass -= 0.000002f * mass;      // ���������� ����� �� ��������

        if (IntervalTicked())   // ��� ������
        {
            UpdateCameraZoom();
        }

        textScore.text = "Score: " + ((int)mass).ToString();  // ����

        touch = GameObject.FindGameObjectWithTag("Player").GetComponent<TouchController>().touch;
        if (touch == false) 
        {
            touch = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickController>().touch;
        }
        if (touch == true)   // ���������
        {
            if (buffActive == false)
            {
                mass *= 0.999f;
            }
        }

        if (mass < 0)
        {
            death();
        }

    }

void OnTriggerEnter(Collider col)  // �������� ����� � ���
    {
        if (col.gameObject.tag == "Food")
        {
            mass += massCoin;
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;

            //camSize += 0.002f * massCoin;   // ��������� ������
        }
        if (col.gameObject.tag == "Enemy")
        {
            massEnemy = col.GetComponent<EnemyController>().mass;
            //massEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass >= massEnemy)
            {
                mass += massEnemy * 0.3f;
                randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
                col.gameObject.transform.position = randVec;
            }
        }

        if (col.gameObject.tag == "EnemyBoss")
        {
            massEnemyBoss = col.GetComponent<EnemyBossController>().mass;
            //massEnemyBoss = GameObject.FindGameObjectWithTag("EnemyBoss").GetComponent<EnemyBossController>().mass;
            if (mass >= massEnemyBoss)
            {
                mass += massEnemyBoss * 0.3f;
                Destroy(col.gameObject); 
            }
            else
            {
                mass -= massEnemyBoss * 0.4f;
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.tag == "Coin")
        {
            coinsCount += 1;
            coins.text = "Money: " + coinsCount.ToString();
            savePlayer();
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;
            
            //camSize += 0.002f * massCoin;   // ��������� ������
        }

        if (col.gameObject.tag == "Buff")
        {

            UseBuff();
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;

        }
    }
    private void savePlayer()
    {
        PlayerPrefs.SetInt("coinsFinal", coinsCount);
    }

    //private IEnumerator DoMessage()   // ������� ���� ������
    //{
    //    for (; ; )
    //    {
    //        RedZone();
    //        yield return new WaitForSeconds(5f);
    //    }
    //}

    //void RedZone()  // ������� ����
    //{

    //    // "������� ����"

    //    if (transform.position.x < -99.0f ^ transform.position.x > 99.0f)
    //    {

    //        mass *= 0.8f;
            
    //    }

    //    if (transform.position.z < -99.0f ^ transform.position.z > 99.0f)
    //    {

    //        mass *= 0.8f;

    //    }
    //}


    private bool IntervalTicked()  // ������ ������
    {
        _time += Time.deltaTime;
        if (_time >= UpdateIntervalSec)
        {
            _time %= UpdateIntervalSec;
            return true;
        }

        return false;
    }

    private void UpdateCameraZoom()  // ��� ������
    {
        if (cam.orthographicSize / vecScale.x < 1.75f)
        {
            camSize += 5f;
            StartCoroutine(nameof(CameraZoom), camSize);
        }else if (cam.orthographicSize / vecScale.x > 5f)
        {
            camSize -= 5f;
            StartCoroutine(nameof(CameraZoom), camSize);
        }

    }

        
    private IEnumerator CameraZoom(float targetZoom)  // ��� ������
    {
        float from = cam.orthographicSize;
        float time = 0;

        while (time < 1f)
        {
            time += ZoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(from, targetZoom, time);

            yield return null;
        }

        spawnRange += 50f;
        cam.orthographicSize = targetZoom;
    }


    private void CamScale(float size)
    {

    }

    //public void speedUp()  
    //{
    //    touch = true;
    //}

    //public void speedDown()
    //{
    //    touch = false;
    //}

    private void death()  // ����� ������
    {
        if (!deathScreen.activeSelf)
        {
            //Destroy(sphere);
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void UseBuff()
    {
        if (buffActive == false)
        {
            buffActive = true;
            Invoke("SetBuffFalse", buffTimer);
        }
    }

    private void SetBuffFalse()
    {
        buffActive = false;
    }
}
