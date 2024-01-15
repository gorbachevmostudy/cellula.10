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

    bool left;  // ������� ������ ������� �����. True - �����, false - ������


    private void Start()
    {
        //StartCoroutine("DoMessage");   // ����� ������� �� ������ �������
        //left = true;
    }
    void Awake()
    {
        for (int i = 0; i < 1500; i++)  // ���
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Food, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)     // ������
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Enemy, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // ������
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Coin, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // ���������
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
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

}
