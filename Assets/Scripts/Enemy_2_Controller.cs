using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Controller : MonoBehaviour
{
    [SerializeField] private float cam;
    public GameObject sphere;
    private Transform player;
    private Vector3 randVector;
    private float camSize;

    public float playerMass;
    public float enemyBossMass;
    public float mass;
    private float lookspeed;
    public float speed;
    private float massCoin;
    private Vector3 randVec;
    private Vector3 vecScale;
    public GameObject deathScreen;

    public string nearest;
    public string nearestEnemy;

    void Start()
    {

        speed = 5f;
        massCoin = 3f;
        mass = 40f;
        lookspeed = 3f;
        vecScale.Set(1, 1, 1);
        randVector.Set(Random.Range(-2000, 2000), 1, Random.Range(-2000, 2000));
        StartCoroutine("DoMessage");

    }

    void FixedUpdate()
    {
        //speed = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));
        camSize = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;
        speed = camSize * 0.8f;// Горбачев 21.01.2024 убрал зависимость скорости от массы игрока
        playerMass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //foodTrans = GameObject.FindGameObjectWithTag("Food").GetComponent<Transform>();

        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distToPlayer < cam * 4f)
        {
            if (mass > playerMass)
            {
                Quaternion targetrotation = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed);
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
            }
            else
            {

                Quaternion targetrotation = Quaternion.LookRotation(randVector - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed * 0.8f);
                transform.position = Vector3.MoveTowards(transform.position, randVector, speed * Time.fixedDeltaTime);
                mass *= 1.00020f;

            }
        }
        else
        {
            if (mass < playerMass * 0.7f)
            {

                mass *= 1.0006f;

            }
            else if ((mass >= playerMass * 0.7f) && (mass < playerMass * 1.2f))
            {

                mass *= 1.00040f;

            }
            else if (mass >= playerMass * 1.2f)
            {
                mass *= 1.0003f;
            }

        }

        vecScale.Set((mass / 200f + 0.95f), 1, (mass / 200f + 0.95f));
        transform.localScale = vecScale;

    }

    void OnTriggerEnter(Collider col)   // поедание всех
    {
        if (col.gameObject.tag == "Food")
        {
            mass += massCoin;
            randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            col.gameObject.transform.position = randVec;

        }

        deathScreen = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().deathScreen;

        if (col.gameObject.tag == "Player")
        {
            if (mass > playerMass)
            {
                if (!deathScreen.activeSelf)
                {
                    //Destroy(col.gameObject);
                    deathScreen.SetActive(true);

                    Time.timeScale = 0;
                    //Debug.Break();
                }
            }
        }

        if (col.gameObject.tag == "EnemyBoss")
        {
            enemyBossMass = col.GetComponent<EnemyBossController>().mass;
            if (mass > enemyBossMass)
            {

                mass += enemyBossMass * 0.3f;
                randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
                col.gameObject.transform.position = randVec;

            }
        }

        if (col.gameObject.tag == "Enemy")
        {
            float enemyMass = col.GetComponent<EnemyController>().mass;

            if (mass > enemyMass)
            {
                mass += enemyMass * 0.2f;
                randVec.Set(Random.Range(-300f, 300f), 0, Random.Range(-300f, 300f));
                col.gameObject.transform.position = randVec;

            }
        }
    }

    private IEnumerator DoMessage()     // спавн красных таймер
    {
        for (; ; )
        {
            SetVector();
            yield return new WaitForSeconds(3f);
        }
    }

    void SetVector()   // cпавн красных энеми раз в 30 секунд. Они атакуют игрока с любого расстояния
    {
        randVector.Set(Random.Range(-99, 99), 1, Random.Range(-99, 99));
    }
}
