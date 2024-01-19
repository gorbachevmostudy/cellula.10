using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float cam;
    public GameObject sphere;
    private Transform player;
    //public Transform enemyBoss;
    private Transform foodTrans;
    
    public float playerMass;
    public float enemyBossMass;
    public float mass;
    private float lookspeed;
    private Vector3 way;
    public float speed;
    private float massCoin;
    private Vector3 randVec;
    private Vector3 vecScale;
    public GameObject deathScreen;
    

    //GameObject[] food;
    //GameObject closest;
    //GameObject[] enemyBoss;
    //GameObject closestEnemyBoss;

    public string nearest;
    public string nearestEnemy;


    void Start()
    {
        //food = new GameObject[10];
        //food = GameObject.FindGameObjectsWithTag("Food");
        //enemyBoss = new GameObject[5];
        //enemyBoss = GameObject.FindGameObjectsWithTag("EnemyBoss");
        speed = 5f;
        massCoin = 3f;
        mass = 50f;
        lookspeed = 3f;
        vecScale.Set(1, 1, 1);

    }

    //GameObject FindClosestFood()    // поиск ближайшей еды
    //{

    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject go in food)
    //    {

    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;

    //        if (curDistance < distance)
    //        {
    //            closest = go;
    //            distance = curDistance;
    //        } 
    //    }
    //    return closest;
    //}

    //GameObject FindClosestEnemyBoss()   // поиск ближайшего красного энеми
    //{

    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject go in food)
    //    {

    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;

    //        if (curDistance < distance)
    //        {
    //            closestEnemyBoss = go;
    //            distance = curDistance;
    //        }
    //    }
    //    return closestEnemyBoss;
    //}

    void FixedUpdate()
    {
        speed = 6 * Mathf.Pow(20, -Mathf.Log(2, 0.01f)) * Mathf.Pow(mass, Mathf.Log(2, 0.01f));

        playerMass = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().mass;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foodTrans = GameObject.FindGameObjectWithTag("Food").GetComponent<Transform>();

        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().camSize;
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        //nearest = FindClosestFood().name;
        //nearestEnemy = FindClosestEnemyBoss().name;
        //float distToEnemyBoss = Vector3.Distance(transform.position, closestEnemyBoss.transform.position);
        //enemyBossMass = GameObject.FindGameObjectWithTag("EnemyBoss").GetComponent<EnemyBossController>().mass;

        // Перепис в связи с новой логикой энеми-боссов
        //try   // попытка найти массу красного энеми, чтобы не выпадала ошибка
        //{
        //    enemyBossMass = closestEnemyBoss.GetComponent<EnemyBossController>().mass;
        //}
        //catch { }
        if (distToPlayer < cam * 8f)
        {
            if (mass > playerMass)
            {
                Quaternion targetrotation = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed);
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
            }
            else 
            {
                Quaternion targetrotation = Quaternion.LookRotation(-player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed * 0.8f);
                transform.position = Vector3.MoveTowards(transform.position, -player.position, speed * Time.fixedDeltaTime);
                //Quaternion targetrotation = Quaternion.LookRotation(closest.transform.position - transform.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * lookspeed);
                //transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, speed * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (mass < playerMass * 0.7f) 
            {
                
                mass *= 1.0005f;

            } else if ((mass >= playerMass * 0.7f) && (mass < playerMass * 1.2f))
            {
                
                mass *= 1.00035f;
            
            } else if (mass >= playerMass * 1.2f)
            {
                mass *= 1.0002f;
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
            //enemyBossMass = GameObject.FindGameObjectWithTag("EnemyBoss").GetComponent<EnemyBossController>().mass;
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
            //float enemyMass = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>().mass;
            if (mass > enemyMass)
            {
                mass += enemyMass * 0.3f;
                randVec.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
                col.gameObject.transform.position = randVec;

            }
        }
    }
}
