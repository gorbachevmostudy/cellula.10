using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AgarController : MonoBehaviour
{
    [SerializeField] GameObject[] Tutorials;

    //public float delta;
    [SerializeField] Text coins;
    public int coinsCount;
    public GameObject particleSystem;
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
    float massEnemy_2;
    float massEnemy_3;
    private bool touch;
    public GameObject deathScreen;
    public float spawnRange;

    public float UpdateIntervalSec = 3f / 3f; // 3 (cейчас 1 раз) раза в секунду
    public float ZoomSpeed = 0.5f; // скорость зума камеры

    public float buffTimer; // время действия бафа
    public bool buffActive;    // булева - активность бафа
    public bool buffActiveRepeat;

    private float _time;
    private bool _zoomIsSmall = true;
    private Camera _camera;

    private float doubleClickTime = 0.5f, lastClickTime;   // дабл клик для ускорения

    public int sec = 0;
    public int min = 0;
    private Text TimerText;
    [SerializeField] private int timedelta = 0;

    public AudioSource eatFoodSound;
    public AudioSource eatEnemySound;
    public AudioSource eatMoneySound;
    public AudioSource eatSpeedSound;
    public AudioSource deathSound;
    //internal float camsize;  // Горбачев закомментил 04_02_24, хз что за поле
    private float koef;
    private void Start()
    {

        TimerText = GameObject.Find("TimerText").GetComponent<Text>();
        StartCoroutine(ITimer());

        if (PlayerPrefs.HasKey("coinsFinal"))
        {
            coinsCount = PlayerPrefs.GetInt("coinsFinal");
        }
        else
        {
            coinsCount = 0;
        }
        coins.text = "ДНК: " + coinsCount.ToString();
        //delta = 6;
        mass = 50f;
        //lookspeed = 10f;
        vecScale.Set(1, 1, 1);
        camSize = 7f;
        
        buffActive = false;
        buffTimer = 10f;
        spawnRange = 99f;
        koef = 0.8f; // коэффициент разброса спавна для кучности при отдалении камеры
        //StartCoroutine("DoMessage");

    }

    void FixedUpdate()
    {

        if (mass < 400f)   // увеличение ценности еды в начале игры
        {
            massCoin = 6f;
        } else massCoin = 3f;


        // КОД ДЛЯ УМЕНЬШЕНИЯ СКОРОСТИ ОТ МАССЫ

        //Plane playerPlane = new Plane(Vector3.up, transform.position);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float hitdist = 0;
        //if (playerPlane.Raycast(ray, out hitdist))  // поворот и движение за мышью
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitdist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookspeed);
        //    transform.position += transform.forward * delta * Time.fixedDeltaTime;

        //}

        vecScale.Set((mass / 200 + 0.95f), 1, (mass / 200 + 0.95f));  // масштаб от массы
        transform.localScale = vecScale;

        //mass -= 0.000002f * mass;      // уменьшение массы со временем

        if (IntervalTicked())   // зум камеры
        {
            UpdateCameraZoom();
        }

        textScore.text = "Масса: " + ((int)mass).ToString();  // счет

        touch = GameObject.FindGameObjectWithTag("Player").GetComponent<TouchController>().touch;
        if (touch == false) 
        {
            touch = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickController>().touch;
        }
        if (touch == true)   // ускорение
        {
            if (buffActive == false)
            {
                mass *= 0.999f;
            }
        }

        if (mass < 0)
        {
            deathSound.Play();
            death();
        }

    }

void OnTriggerEnter(Collider col)  // поедание челов и еды
    {
        if (col.gameObject.tag == "Food")
        {
            mass += massCoin;
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;
            eatFoodSound.Play();

            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag))
            {
                OpenTutorial(col.gameObject.tag);
            }
            //camSize += 0.002f * massCoin;   // Отдаление камеры
        }
        if (col.gameObject.tag == "Enemy")
        {
            massEnemy = col.GetComponent<EnemyController>().mass;
            //massEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass >= massEnemy)
            {
                mass += massEnemy * 0.5f;
                randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
                col.gameObject.transform.position = randVec;
                eatEnemySound.Play();
            }
            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag.Substring(0, 5)))
            {
                OpenTutorial(col.gameObject.tag.Substring(0, 5));
            }
        }

        if (col.gameObject.tag == "Enemy_2")
        {
            massEnemy_2 = col.GetComponent<Enemy_2_Controller>().mass;
            //massEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass >= massEnemy_2)
            {
                mass += massEnemy_2 * 0.5f;
                randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
                col.gameObject.transform.position = randVec;
                eatEnemySound.Play();
            }
            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag.Substring(0, 5)))
            {
                OpenTutorial(col.gameObject.tag.Substring(0, 5));
            }
        }

        if (col.gameObject.tag == "Enemy_3")
        {
            massEnemy_3 = col.GetComponent<Enemy_3_Controller>().mass;
            //massEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass >= massEnemy_3)
            {
                mass += massEnemy_3 * 0.5f;
                randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
                col.gameObject.transform.position = randVec;
                eatEnemySound.Play();
            }
            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag.Substring(0, 5)))
            {
                OpenTutorial(col.gameObject.tag.Substring(0, 5));
            }
        }

        if (col.gameObject.tag == "Coin")
        {
            coinsCount += 1;
            coins.text = "ДНК: " + coinsCount.ToString();
            savePlayer();
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;
            eatMoneySound.Play();
            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag))
            {
                OpenTutorial(col.gameObject.tag);
            }
            //camSize += 0.002f * massCoin;   // Отдаление камеры
        }

        if (col.gameObject.tag == "Buff")
        {

            UseBuff();
            randVec.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            col.gameObject.transform.position = randVec;
            if (!PlayerPrefs.HasKey("FirstTime" + col.gameObject.tag))
            {
                OpenTutorial(col.gameObject.tag);
            }
        }
    }
    private void savePlayer()
    {
        PlayerPrefs.SetInt("coinsFinal", coinsCount);
    }

    //private IEnumerator DoMessage()   // КРАСНАЯ ЗОНА ТАЙМЕР
    //{
    //    for (; ; )
    //    {
    //        RedZone();
    //        yield return new WaitForSeconds(5f);
    //    }
    //}

    //void RedZone()  // красная зона
    //{

    //    // "Красная зона"

    //    if (transform.position.x < -99.0f ^ transform.position.x > 99.0f)
    //    {

    //        mass *= 0.8f;
            
    //    }

    //    if (transform.position.z < -99.0f ^ transform.position.z > 99.0f)
    //    {

    //        mass *= 0.8f;

    //    }
    //}


    private bool IntervalTicked()  // таймер камеры
    {
        _time += Time.deltaTime;
        if (_time >= UpdateIntervalSec)
        {
            _time %= UpdateIntervalSec;
            return true;
        }

        return false;
    }

    private void UpdateCameraZoom()  // зум камеры
    {
        if (cam.orthographicSize / vecScale.x < 1.75f)
        {
            camSize *= 2f;
            StartCoroutine(nameof(CameraZoom), camSize);
        }else if (cam.orthographicSize / vecScale.x > 5f)
        {
            camSize /= 2f;
            StartCoroutine(nameof(CameraZoom), camSize);
        }
        
    }


    private IEnumerator CameraZoom(float targetZoom)  // зум камеры
    {
        float from = cam.orthographicSize;
        float time = 0;

        if (from < targetZoom)
        {
            spawnRange *= 2f;

        }
        else
        {
            spawnRange /= 2f;
        }

        particleSystem.SetActive(true); // Разброс еды и бонусов на большую территорию при отдалении камеры
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject nyam in food)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }
        GameObject[] buff = GameObject.FindGameObjectsWithTag("Buff");
        foreach (GameObject nyam in buff)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }
        GameObject[] coin = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject nyam in coin)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject nyam in enemy)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }
        GameObject[] enemy2 = GameObject.FindGameObjectsWithTag("Enemy_2");
        foreach (GameObject nyam in enemy2)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }
        GameObject[] enemy3 = GameObject.FindGameObjectsWithTag("Enemy_3");
        foreach (GameObject nyam in enemy3)
        {
            randVec.Set(Random.Range(-spawnRange * koef, spawnRange * koef), 0, Random.Range(-spawnRange * koef, spawnRange * koef));
            nyam.transform.position = randVec;
        }

        while (time < 2f)
        {
            time += ZoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(from, targetZoom, time);

            yield return null;
        }
        particleSystem.SetActive(false);

        cam.orthographicSize = targetZoom;

        //camGrown.Invoke(true);  // Горбачев 09_02_2024 триггер для скинченджера

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

    private void death()  // экран смерти
    {
        if (!deathScreen.activeSelf)
        {
            //Destroy(sphere);
            
            deathSound.Play();
            deathScreen.SetActive(true);
            //Time.timeScale = 0;
            
        }
    }

    private void UseBuff()
    {
        if (buffActive == false)
        {
            buffActive = true;
            Invoke("SetBuffFalse", buffTimer);
            eatSpeedSound.Play();
        }
        else
        {
            buffActive = true;
            buffActiveRepeat = true;
            CancelInvoke("SetBuffFalse");
            Invoke("SetBuffFalse", buffTimer);
            Invoke("SetBuffFalseRepeat", 0.5f);
            eatSpeedSound.Play();
        }
    }

    private void SetBuffFalse()
    {
        buffActive = false;
    }

    private void SetBuffFalseRepeat()
    {
        buffActiveRepeat = false;
    }

    IEnumerator ITimer()
    {
        while (true)
        {
            if(sec == 59)
            {
                min++;
                sec = -1;
            }
            sec += timedelta;
            TimerText.text = min.ToString("D2") + ":" + sec.ToString("D2");

            int savedTime = PlayerPrefs.GetInt("minutes")*60 + PlayerPrefs.GetInt("seconds");
            if (min*60 + sec > savedTime)
            {
                PlayerPrefs.SetInt("minutes", min);
                PlayerPrefs.SetInt("seconds", sec);
            }

            yield return new WaitForSeconds(1);
            
           
        }
    }

    private void OpenTutorial(string coll)
    {
        
        foreach(GameObject go in Tutorials)
        {
            if (go.name == coll+"Tutorial")
            {
                go.SetActive(true);
                Time.timeScale = 0;
                PlayerPrefs.SetInt("FirstTime" + coll, 1);
            }
        }

    }

    public void CloseTutorial()
    {
        foreach (GameObject go in Tutorials)
        {
          
            go.SetActive(false);
            Time.timeScale = 1;
            
        }
    }
    //public delegate void Action(bool zoom);  // Горбачев 09_02_2024 триггер для скинченджера
    public static UnityEvent<bool> camGrown = new UnityEvent<bool>();
    //public static event Action camGrown;  // Горбачев 09_02_2024 триггер для скинченджера

}
