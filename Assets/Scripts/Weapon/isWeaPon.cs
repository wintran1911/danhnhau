using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isWeaPon : MonoBehaviour
{
    [SerializeField] public GameObject defaultweapon;
    [SerializeField] public GameObject boxing;
    [SerializeField] public GameObject bua ;
    [SerializeField] public GameObject chao;
    [SerializeField] public GameObject gangtay;
    [SerializeField] public GameObject gaybongchay;
    [SerializeField] public GameObject gaygolf;
    [SerializeField] public GameObject ironFist;
    [SerializeField] public GameObject muoichiencom;
    [SerializeField] public GameObject thuocke;


 
    private void FixedUpdate()
    {
        openWeapon();
       
    }
    public void openWeapon()
    {
    if(WeaPonPlayer.activeWeaponIndex==0)
        {
            defaultweapon.SetActive(true);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
       

        }
        if (WeaPonPlayer.activeWeaponIndex == 1)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(true);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
         
        }
        if (WeaPonPlayer.activeWeaponIndex == 2)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(true);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
      
        }
        if (WeaPonPlayer.activeWeaponIndex == 3)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(true);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
    
        }
        if (WeaPonPlayer.activeWeaponIndex == 4)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(true);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
 
        }
        if (WeaPonPlayer.activeWeaponIndex == 5)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(true);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);

        }
        if (WeaPonPlayer.activeWeaponIndex == 6)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(true);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);

        }
        if (WeaPonPlayer.activeWeaponIndex == 7)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(true);
            muoichiencom.SetActive(false);
            thuocke.SetActive(false);
    
        }
        if (WeaPonPlayer.activeWeaponIndex == 8)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(true);
            thuocke.SetActive(false);
    
        }
        if (WeaPonPlayer.activeWeaponIndex == 9)
        {
            defaultweapon.SetActive(false);
            boxing.SetActive(false);
            bua.SetActive(false);
            chao.SetActive(false);
            gangtay.SetActive(false);
            gaybongchay.SetActive(false);
            gaygolf.SetActive(false);
            ironFist.SetActive(false);
            muoichiencom.SetActive(false);
            thuocke.SetActive(true);
    
        }
    }    
}
