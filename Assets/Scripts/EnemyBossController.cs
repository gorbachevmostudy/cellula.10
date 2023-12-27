using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    //public Transform sphere;
    public GameObject sphere;
    private Transform player;
    private Transform foodTrans;
    public Transform enemy;

    public float playerMass;
    public float enemyMass;
    public float mass;
    private float lookspeed;
    private Vector3 way;
    public float speed;
    private float massCoin;
    private Vector3 randVec;
    private Vector3 vecScale;
    public GameObject deathScreen;
    public GameObject bad_emission;   // красный ореол
    public GameObject good_emission;  // зеленый ореол

    public string nearest;
    public string nearestEnemy;

    void Start()
    {
        good_emission.SetActive(false); // при спавне масса энеми больше, чем игрока, поэтому false
        bad_emission.SetActive(true);   // при спавне true
        speed = 5f;
        massCoin =  3f;
        playerMass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        mass = playerMass * 1.2f;
        lookspeed = 3f;
        vecScale.Set(1, 1, 1);

    }

    void FixedUpdate()
    {
        speed = 6f * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));

        playerMass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foodTrans = GameObject.FindGameObjectWithTag("Food").GetComponent<Transform>();

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        //   -  патч  - мен€ю логику красных мобов, теперь они ед€т только по пути и тупо атакуют игрока
        if(distToPlayer < 250f)
        {
            Quaternion targetrotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        }

        vecScale.Set((mass / 200f + 0.95f), 1, (mass / 200f + 0.95f));
        transform.localScale = vecScale;
        mass -= 0.000002f * mass;

        SetEmission();   // включить красный или зеленый ореол в ззависимости от массы игрока

    }

    void OnTriggerEnter(Collider col)   // поедание еды, игрока и энеми
    {
        if (col.gameObject.tag == "Food")
        {
            mass += massCoin;
            randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            col.gameObject.transform.position = randVec;

        }

        if (col.gameObject.tag == "Enemy" )
        {
            enemyMass = col.GetComponent<EnemyController>().mass;
            enemyMass = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass > enemyMass)
            {
                    mass += enemyMass * 0.3f;
                    randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
                    col.gameObject.transform.position = randVec;
            }
        }

        if (col.gameObject.tag == "EnemyBoss")
        {
            float enemyBossMass = col.GetComponent<EnemyBossController>().mass;
            //float enemyBossMass = GameObject.FindGameObjectWithTag("EnemyBoss").GetComponent<EnemyBossController>().mass;
            if (mass > enemyBossMass)
            {
                mass += enemyBossMass * 0.3f;
                randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
                col.gameObject.transform.position = randVec;
                
            }
        }
    }

    private void SetEmission()  // красный и зеленый ореолы вокруг красного энеми
    {
        if(mass > playerMass)
        {
            if (!bad_emission.activeSelf)
            {
                
                good_emission.SetActive(false);
                bad_emission.SetActive(true);

            }
        }
        else
        {
            if (!good_emission.activeSelf)
            {
                
                bad_emission.SetActive(false);
                good_emission.SetActive(true);

            }
        }
    }
}
