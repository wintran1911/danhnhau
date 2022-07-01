using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelUpPlayer : MonoBehaviour
{
   [SerializeField] public  static int LevelpPlayer;
   [SerializeField] public  int Level;
   [SerializeField] public int LevelMax;
   [SerializeField] public int[] MoneyUp;
   [SerializeField] public int[] HpUp;
   [SerializeField] public Button btnUpgrade;
   [SerializeField] public TextMeshProUGUI txtLv1 ;
   [SerializeField] public TextMeshProUGUI txtLv2 ;
   [SerializeField] public TextMeshProUGUI intLevel ;
   [SerializeField] public TextMeshProUGUI intCoinsUP ;
   [SerializeField] public TextMeshProUGUI txtHp1 ;
   [SerializeField] public TextMeshProUGUI txtHp2;
    private void Awake()
    {
        //PlayerPrefs.GetInt("levelplayerIndex", LevelpPlayer);
    }
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("levelplayerIndex", LevelpPlayer));
     
         LoadLevel();
       
        intLevel.text ="" +LevelpPlayer;
        txtLv1.text = "Level" + (LevelpPlayer);
            txtLv2.text = "Level" + (LevelpPlayer + 1);
            txtHp1.text = "" + (HpUp[LevelpPlayer - 1]);
            txtHp2.text = "" + (HpUp[LevelpPlayer + 1]);
    }
    private void Update()
    {
       /* if (LevelpPlayer < LevelMax)
        {
           
        }*/
         Level = LevelpPlayer;
        
        if ((LevelpPlayer > 0) && (LevelpPlayer < LevelMax))
        {
            intCoinsUP.text = "UPGRADE:" + MoneyUp[LevelpPlayer];
        }
        if ((LevelpPlayer >= LevelMax))
        {
            btnUpgrade.gameObject.SetActive(false);
        }

    }
    public void LevelUP()
    {
        if((LevelpPlayer < LevelMax)&&( MoneyUp[LevelpPlayer] <= SaveCoins.instance.money))
        {
            LevelpPlayer+= 1;
            SaveCoins.instance.money -= MoneyUp[LevelpPlayer];
            SaveLevel();
            intLevel.text = "" + LevelpPlayer;
            txtLv1.text = "Level" + (LevelpPlayer);
            txtLv2.text = "Level" + (LevelpPlayer + 1);
            txtHp1.text = "" + (HpUp[LevelpPlayer - 1]);
            txtHp2.text = "" + (HpUp[LevelpPlayer + 1]);
        }

    }
   public  void LoadLevel()
    {
        LevelpPlayer = PlayerPrefs.GetInt("levelplayerIndex");
    }
    public void SaveLevel()
    {
        

       if (LevelpPlayer <21)
        {
            PlayerPrefs.SetInt("levelplayerIndex", LevelpPlayer);
            //PlayerPrefs.GetInt("levelplayerIndex");
            PlayerPrefs.Save();
            //Debug.Log("save level ruif dcm");
            Debug.Log(PlayerPrefs.GetInt("levelplayerIndex"));
           // IsWeapon = true;
        }
      //else
        {
            //IsWeapon = false;
        }
    }
}
