using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextScript : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        CoinScript.OnMoneyCollected
            += ScriptOnOnMoneyCollected;
    }

    private void ScriptOnOnMoneyCollected(object sender, EventArgs e)
    {
        var rawText = "Coins: " + CoinScript.GetCoinAmount().ToString("0");
        if (text != null) text.text = rawText;
    }
}