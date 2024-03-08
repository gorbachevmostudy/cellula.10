using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] private int coins, enemies, speedPrice, enemiesPrice;
    [SerializeField] private float speed;

    private void Start()
    {
        if (coins != 0)
        {
            PlayerPrefs.SetInt("coinsFinal", coins);
        }
        if (speed != 0)
        {
            PlayerPrefs.SetFloat("playerSpeed", speed);
        }
        if (enemies != 0)
        {
            PlayerPrefs.SetInt("enemiesCount", enemies);
        }
        if (speedPrice != 0)
        {
            PlayerPrefs.SetInt("speedPrice", speedPrice);
        }
        if (enemiesPrice != 0)
        {
            PlayerPrefs.SetInt("enemiesPrice", enemiesPrice);
        }
    }
}
