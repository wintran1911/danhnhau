using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MoneyCounter : MonoBehaviour
{
   private TextMeshProUGUI txtCoin;
  // [SerializeField] private TextMeshProUGUI txtCoin2;

    private void Awake()
    {
        txtCoin = GetComponent<TextMeshProUGUI>();
       
    }
    private void Update()
    {
        txtCoin.text = SaveCoins.instance.money +"";
    }
}
