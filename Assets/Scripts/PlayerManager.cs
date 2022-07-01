
using System.Numerics;
using System.Data;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager ins;

    //private Mana mana;
    public const int MANA_MAX = 100;
    private float manaAmount;
    public float manaRegenAmount = 200;
   // public Image powerBar;
    public TextMeshProUGUI manaPower;
    public float manaCurrent;
    public bool isFullManaBar;
    private bool isUpdateManaBar;
    public bool canAddMana =false;
    private void Awake() {
        ins = this;

        //mana = new Mana();
        //powerBar.fillAmount = 0.2f;
        manaCurrent = 5;
        manaPower.text = manaCurrent.ToString();
    }
     private void Start() {
        UpdataManabar();
    }
    private void Update() {
        //mana.Update();
        //powerBar.fillAmount = mana.GetManaNormalized();
        manaPower.text = manaCurrent.ToString();
        CheckManaFull();
        
        
    }
    IEnumerator UpdataManaBar(){
        while(manaCurrent <5){
            canAddMana = true;
           
           // Debug.Log(manaCurrent);
            yield return new WaitForSeconds(2f);
            //mana.Update();
            //powerBar.fillAmount = mana.GetManaNormalized();
            manaCurrent++;
            UIManager.ins.UpdateManaPowerPlayer(manaCurrent/5f);
            //manaPower.text = manaCurrent.ToString();
            
        }
    }
    private void CheckManaFull(){
         if(manaCurrent == 5){
            isFullManaBar = true;
        }
        else{
            isFullManaBar = false;
        }
    }
    public void UpdataManabar(){
        StartCoroutine(UpdataManaBar());
    }
    public void AddManaBar(){
        manaAmount += manaRegenAmount * Time.deltaTime;
        //powerBar.fillAmount = manaAmount / MANA_MAX;
    }
    
}
// public class Mana{
//         public const int MANA_MAX = 100;
//         private float manaAmount;
//         private float manaRegenAmount;

//         public Mana(){
//             manaAmount = 0;
//             manaRegenAmount = 20f;
//         }
//         public void Update() {
//             manaAmount += manaRegenAmount * Time.deltaTime;
//         }
//         public void TrySpendMana(int amount){
//             if(manaAmount >= amount){
//                 manaAmount -= amount;
//             }
//         }
//         public float GetManaNormalized(){
//             return manaAmount / MANA_MAX;
//         }
//     }

