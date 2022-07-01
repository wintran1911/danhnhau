using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAdd : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveCoins.instance.money += 100;
            SaveCoins.instance.Save();
            Debug.Log("+");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SaveCoins.instance.money -= 100;
            SaveCoins.instance.Save();
            Debug.Log("-");
        }
    }
}
