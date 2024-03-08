using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private Material[] backs;
    [SerializeField] private AudioClip[] music;
    private AudioSource AudioSource;
    private Material plane;
    public GameObject Food;
    public GameObject Enemy;
    public GameObject Enemy_2;
    public GameObject Enemy_3;
    private GameObject selectedEnemy;
    public GameObject Coin;
    public GameObject Buff;
    private Vector3 randVector;
    private Transform player;
    public Camera cam;
    public float spawnRange;
    private int typeOfEnemy;
    private int typeOfBackground;
    private int enemyCount;
    bool left;  // стороны спавна красных энеми. True - слева, false - справа


    private void Start()
    {

        
    }
    void Awake()
    {

        AudioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        typeOfBackground = Random.Range(0, backs.Length);

        MeshRenderer render = GameObject.FindGameObjectWithTag("Plane").GetComponent<MeshRenderer>();
        //render.material = backs[typeOfBackground];

        typeOfEnemy = Random.Range(1, 4);
        switch (typeOfEnemy)
        {
            case 1: // fast
                selectedEnemy = Enemy;
                render.material = backs[3];
                AudioSource.clip = music[2];
                AudioSource.Play();
                break;
            case 2: // dark
                selectedEnemy = Enemy_2;
                render.material = backs[0];
                AudioSource.clip = music[0];
                AudioSource.Play();
                break;
            case 3: // safe
                selectedEnemy = Enemy_3;
                render.material = backs[2];
                AudioSource.clip = music[1];
                AudioSource.Play();
                break;
        }

        spawnRange = 99f;
        //spawnRange = GameObject.FindGameObjectWithTag("Player").GetComponent<AgarController>().spawnRange;
        for (int i = 0; i < 1500; i++)  // еда
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Food, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // монеты
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Coin, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 40; i++)     // ускорение
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Buff, randVector, Quaternion.identity);
        }


        ////////// СПАВН ЭНЕМИ \\\\\\\\\\\\
        if (PlayerPrefs.HasKey("enemiesCount"))
        {
            enemyCount = PlayerPrefs.GetInt("enemiesCount");
        }
        else
        {
            PlayerPrefs.SetInt("enemiesCount", 30);
            enemyCount = PlayerPrefs.GetInt("enemiesCount");
        }
            
        for (int i = 0; i < enemyCount; i++)     // энеми основные
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(selectedEnemy, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)     // энеми основные
        {
            randVector.Set(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            Instantiate(selectedEnemy, randVector, Quaternion.identity);
        }

        //for (int i = 0; i < 5; i++)     // энеми2 рядом со спавном
        //{
        //    randVector.Set(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        //    Instantiate(Enemy_2, randVector, Quaternion.identity);
        //}

        //for (int i = 0; i < 5; i++)     // энеми3 рядом со спавном
        //{
        //    randVector.Set(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        //    Instantiate(Enemy_3, randVector, Quaternion.identity);
    }
        ////////// СПАВН ЭНЕМИ \\\\\\\\\\\\
        
    }

    //private IEnumerator DoMessage()     // спавн красных таймер
    //{
    //    for (; ; )
    //    {
    //        AttackPlayer();
    //        yield return new WaitForSeconds(120f);
    //    }
    //}

    //void AttackPlayer()   // cпавн красных энеми раз в 30 секунд. Они атакуют игрока с любого расстояния
    //{
    //    if (left)
    //    {
    //        randVector.Set(Random.Range(-130.0f, -100.0f), 0, Random.Range(-99.0f, 99.0f));
    //        Instantiate(EnemyBoss, randVector, Quaternion.identity);
    //        left = false;
    //    }
    //    else
    //    {
    //        randVector.Set(Random.Range(100.0f, 130.0f), 0, Random.Range(-99.0f, 99.0f));
    //        Instantiate(EnemyBoss, randVector, Quaternion.identity);
    //        left = true;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    //    if (cam.orthographicSize / player.localScale.x < 1.75f)
    //    {
    //        spawnRange += 20f;  
    //    }
    //    else if (cam.orthographicSize / player.localScale.x > 5f)
    //    {
    //        spawnRange -= 20f;
    //    }
    //}

