using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Spine;
using UnityEngine.UI;
using TMPro;

public class Selectionprices : MonoBehaviour
{
    [Header("PLay/Buy btn")]
    [SerializeField] private Button btnEquip;
    [SerializeField] private Button btnEquiped;
    [SerializeField] private Button btnBuy;
    [SerializeField] private TextMeshProUGUI txtCoinsPrice;
    private int currenmoney;


    [Header("weapon unlock")]
    [SerializeField] private int[] WeaponPrices;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currenmoney = WeaPonPlayer.activeWeaponIndex;
       
        SelectionWeapon();
        if(btnBuy.gameObject.activeInHierarchy)
        {
            ///co du tien mua hay ko
            btnBuy.interactable = (SaveCoins.instance.money >= WeaponPrices[currenmoney]);
        }
    }
   
    private void SelectionWeapon()
    {

        if (SaveCoins.WeaponUnlocked[currenmoney])
        {
            btnEquip.gameObject.SetActive(true);
            btnBuy.gameObject.SetActive(false);
        }
        else
        {
            btnEquip.gameObject.SetActive(false);
            btnBuy.gameObject.SetActive(true);
            txtCoinsPrice.text = WeaponPrices[currenmoney] + "";
           
        }
    }
    public void BuyWeapon()
    {
        SaveCoins.instance.money -= WeaponPrices[currenmoney];
        SaveCoins.WeaponUnlocked[currenmoney] = true;
        SaveCoins.instance.Save();
        
    }    
  /*  public void btnEquipOnOff()
    {
        btnEquiped.gameObject.SetActive(true);
        btnEquip.gameObject.SetActive(false);
    }
    public void btnEquipEdOnOff()
    {
        btnEquiped.gameObject.SetActive(false);
        btnEquip.gameObject.SetActive(true);
    }*/
}
