using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    Text CoinTexts;
    public static int Coin;
    void Start()
    {
        CoinTexts = GetComponent<Text>();
        if (PlayerPrefs.HasKey("coinsFinal"))
        {
            Coin = PlayerPrefs.GetInt("coinsFinal");
        }
        //Coin = PlayerPrefs.GetInt("ñoinsFinal");
    }

    // Update is called once per frame
    void Update()
    {
        CoinTexts.text = "your dna: " + Coin.ToString();
    }
}
