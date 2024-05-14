using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text coinsBar, speedBar, enemiesBar, speedPriceT, enemiesPriceT;
    [SerializeField] GameObject speedUP, enemiesUP;
    public string objectName;
    public int coins, enemies, speedPrice, enemiesPrice;
    public float speed;
    private CoinText Coin;
    //public GameObject block;
    void Awake()
    {
        if (PlayerPrefs.HasKey("coinsFinal"))
        {
            coins = PlayerPrefs.GetInt("coinsFinal");
        }
        else
        {
            coins = 0;
        }
        coinsBar.text = "твои ДНК: " + coins.ToString();

        if (PlayerPrefs.HasKey("playerSpeed"))
        {
            speed = PlayerPrefs.GetFloat("playerSpeed");
        }
        else
        {
            PlayerPrefs.SetFloat("playerSpeed", 1);
            speed = PlayerPrefs.GetFloat("playerSpeed");
        }
        speedBar.text = "скорость: " + (speed*100f).ToString("###") + "%";

        if (PlayerPrefs.HasKey("enemiesCount"))
        {
            enemies = PlayerPrefs.GetInt("enemiesCount");
        }
        else
        {
            PlayerPrefs.SetInt("enemiesCount", 30);
            enemies = PlayerPrefs.GetInt("enemiesCount");
        }
        enemiesBar.text = "враги: " + enemies.ToString();

        if (PlayerPrefs.HasKey("speedPrice"))
        {
            speedPrice = PlayerPrefs.GetInt("speedPrice");
        }
        else
        {
            PlayerPrefs.SetInt("speedPrice", 15);
            speedPrice = PlayerPrefs.GetInt("speedPrice");
        }
        speedPriceT.text = speedPrice.ToString() + " ДНК";

        if (PlayerPrefs.HasKey("enemiesPrice"))
        {
            enemiesPrice = PlayerPrefs.GetInt("enemiesPrice");
        }
        else
        {
            PlayerPrefs.SetInt("enemiesPrice", 50);
            enemiesPrice = PlayerPrefs.GetInt("enemiesPrice");
        }
        enemiesPriceT.text = enemiesPrice.ToString() + " ДНК";
    }

    
    public void OnButtomDownSpeed()
    {
        float actual = PlayerPrefs.GetFloat("playerSpeed");
        int actualPrice = PlayerPrefs.GetInt("speedPrice");
        if (coins >= actualPrice)
        {
            
            PlayerPrefs.SetFloat("playerSpeed", actual * 1.02f);
            PlayerPrefs.SetInt("coinsFinal", coins - actualPrice);
            PlayerPrefs.SetInt("speedPrice", actualPrice + 13);

            speed = PlayerPrefs.GetFloat("playerSpeed");
            coins = PlayerPrefs.GetInt("coinsFinal");
            CoinText.Coin -= actualPrice;
            //coinsBar.text = "your dna: " + coins.ToString();
            speedBar.text = "скорость: " + (speed * 100).ToString("###") + "%";
            speedPriceT.text = PlayerPrefs.GetInt("speedPrice").ToString() + " ДНК";

        }
    }

    public void OnButtomDownEnemies()
    {
        int actual = PlayerPrefs.GetInt("enemiesCount");
        int actualPrice = PlayerPrefs.GetInt("enemiesPrice");
        if (coins >= actualPrice)
        {
            
            PlayerPrefs.SetInt("enemiesCount", actual + 1);
            PlayerPrefs.SetInt("coinsFinal", coins - actualPrice);
            PlayerPrefs.SetInt("enemiesPrice", actualPrice + 37);

            coins = PlayerPrefs.GetInt("coinsFinal");
            enemies = PlayerPrefs.GetInt("enemiesCount");
            CoinText.Coin -= actualPrice;
            //coinsBar.text = "your dna: " + coins.ToString();
            enemiesBar.text = "враги: " + enemies.ToString();
            enemiesPriceT.text = PlayerPrefs.GetInt("enemiesPrice").ToString() + " ДНК";

        }
    }

    private void Update()
    {
        
    }
}
