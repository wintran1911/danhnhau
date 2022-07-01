using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //ButtonUI
    [SerializeField] private Button btnBackHome1;
    [SerializeField] private Button btnBackHome2;
    [SerializeField] private Button btnWeapon;
    [SerializeField] private Button btnUPgrade;
    [SerializeField] private Button btnPlay;
    //[SerializeField] private Button btnEquid;
    //Canvan
    [SerializeField] private GameObject uiHome;
    [SerializeField] private GameObject uiWeapon;
    [SerializeField] private GameObject uiUpgrade;
    [SerializeField] private int select;
    public static bool saveweapon =false;
    private void Start()
    {
        btnBackHome1.onClick.AddListener(BackHome);
        btnBackHome2.onClick.AddListener(BackHome);
        btnWeapon.onClick.AddListener(WeaPonPlayer);
        btnUPgrade.onClick.AddListener(UpgradePlayer);
        btnPlay.onClick.AddListener(PlayGame);
       // btnEquid.onClick.AddListener(EquidWeapon);
       // btnEquid.onClick.AddListener(BackHome);
        
       
    }
    private void Update()
    {

    }
    private void BackHome()
    {
        uiHome.SetActive(true);
        uiWeapon.SetActive(false);
        uiUpgrade.SetActive(false);
    }
    private void WeaPonPlayer()
    {
        uiHome.SetActive(false);
        uiWeapon.SetActive(true);
        uiUpgrade.SetActive(false);
    }
    private void UpgradePlayer()
    {
        uiHome.SetActive(false);
        uiWeapon.SetActive(false);
        uiUpgrade.SetActive(true);
    }
    public void PlayGame()
    {
        if(SaveCoins.WeaponUnlocked!= null)
        {
          SceneManager.LoadScene(1);
        }
       
    }
    [Header("txtEquip")]
    [SerializeField] private TextMeshProUGUI txtequip;
    public void EquidWeapon()
    {
        DataInGame.activeWeaponIndex = select;
        saveweapon = true;  
        if(select==1)
        {
            txtequip.text = "EQUIP";
        }    
        else
        {
            txtequip.text = "UNEQUIP";
        }    
    }
    
}
