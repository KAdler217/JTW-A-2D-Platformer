using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    // Start is called before the first frame update
    public int startCoin;
    public Text coinCount;
    public static int currentCoin;
    void Start()
    {
        startCoin = PlayerPrefs.GetInt("CoinCount");
        currentCoin = startCoin;
    }

    // Update is called once per frame
    void Update()
    {
        coinCount.text = currentCoin.ToString();
    }
}
