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

    bool left;  // стороны спавна красных энеми. True - слева, false - справа


    private void Start()
    {
        //StartCoroutine("DoMessage");   // старт скрипта по спавну красных
        //left = true;
    }
    void Awake()
    {
        for (int i = 0; i < 1500; i++)  // еда
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Food, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)     // желтые
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Enemy, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // монеты
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Coin, randVector, Quaternion.identity);
        }

        for (int i = 0; i < 100; i++)     // ускорение
        {
            randVector.Set(Random.Range(-99.0f, 99.0f), 0, Random.Range(-99.0f, 99.0f));
            Instantiate(Buff, randVector, Quaternion.identity);
        }

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

}
