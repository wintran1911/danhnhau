using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    private void Awake() {
        ins = this;
    }
    public Image manaBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerManager.ins.manaCurrent/5);
        //manaBar.fillAmount = PlayerManager.ins.manaCurrent/5;
    }
    public void UpdateManaPower(float value){
        manaBar.fillAmount = value;
    }
}
