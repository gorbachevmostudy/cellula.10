using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject Food;
    public GameObject Enemy;
    public GameObject EnemyBoss;
    public GameObject Coin;
    public GameObject Buff;
    private Vector3 randVector;
    private Transform player;
    public Camera cam;
    public float spawnRange;
    bool left;  // ������� ������ ������� �����. True - �����, false - ������


    private void Start()
    {
        //StartCoroutine("DoMessage");   // ����� ������� �� ������ �������
        //left = true;
 
        

    }
    void Awake()
    {
        spawnRange = 99;
        for (int i = 0; i < 1500; i++)  // ���
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Food, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // ������ ��������
        {
            randVector.Set(Random.Range(-300, 300), 0, Random.Range(-300, 300));
            Instantiate(Enemy, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)     // ������ ����� �� �������
        {
            randVector.Set(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            Instantiate(Enemy, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // ������
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Coin, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 40; i++)     // ���������
        {
            randVector.Set(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            Instantiate(Buff, randVector, Quaternion.identity);
        }

    }

    //private IEnumerator DoMessage()     // ����� ������� ������
    //{
    //    for (; ; )
    //    {
    //        AttackPlayer();
    //        yield return new WaitForSeconds(120f);
    //    }
    //}

    //void AttackPlayer()   // c���� ������� ����� ��� � 30 ������. ��� ������� ������ � ������ ����������
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
}
