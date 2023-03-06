using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoins : MonoBehaviour
{
    public TMP_Text Coins;
    
    // Update is called once per frame
    void Update()
    {
        if (Coins == null)
            return;

        if (GameManager.Instance == null)
            return;
        
        Coins.text = GameManager.Instance.TotalCoins.ToString();
    }
}
