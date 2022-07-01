using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Spine;
using UnityEngine.UI;
using TMPro;
public class WeaPonPlayer : MonoBehaviour
{
    //button
    [SerializeField] private Button btnPrew;
    [SerializeField] private Button btnNext;

    // character skins
    [SpineSkin] public string[] InitaialSkin = {"default","boxing","bua","chao","gang_tay","gay_bong_chay","gay_goft","iron_fist","muoi_chien_com","thuoc_ke" };
    public static int activeWeaponIndex = 0;
    SkeletonAnimation skeletonAnimation;
    Skin characterSkin;
    private bool IsWeapon;
   /* [Header("PLay/Buy btn")]
      [SerializeField] private Button btnPlay;
      [SerializeField] private Button btnBuy;
      [SerializeField] private TextMeshProUGUI txtCoinsPrice;
      

    [Header("weapon unlock")]
      [SerializeField] private int[] WeaponPrices;*/

    // Start is called before the first frame update
    private void Awake()
    {
        skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        PlayerPrefs.GetInt("activeWeaponIndex", activeWeaponIndex);
       
    }
    void Start()
    {
       
        if (!PlayerPrefs.HasKey("activeWeaponIndex"))
        {
            activeWeaponIndex = 0;
        }
        else
        {
           LoadWeaPon();
        }
        UpdateCharacterSkin();
        UpdateCombinedSkin();
        btnPrew.onClick.AddListener(PrevWeponSkin);
        btnPrew.onClick.AddListener(NextWeaponSkin);
    }

    // Update is called once per frame
    void Update()
    {
       
        SaveWeaPones();
    }

    public void NextWeaponSkin()
    {
        activeWeaponIndex = (activeWeaponIndex + 1) % InitaialSkin.Length;
        UpdateCharacterSkin();
        UpdateCombinedSkin();
        // SaveWeaPones();
        // GobalDataWeapon.IndexWeaPon = activeWeaponIndex;
    }

    public void PrevWeponSkin()
    {
        activeWeaponIndex = (activeWeaponIndex + InitaialSkin.Length - 1) % InitaialSkin.Length;
        UpdateCharacterSkin();
        UpdateCombinedSkin();
        //  SaveWeaPones();
        //  GobalDataWeapon.IndexWeaPon = activeWeaponIndex;
       
    }
   public void UpdateCharacterSkin()
    {
        var skeleton = skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;
        characterSkin = new Skin("character-base");
        // Note that the result Skin returned by calls to skeletonData.FindSkin()
        // could be cached once in Start() instead of searching for the same skin
        // every time. For demonstration purposes we keep it simple here.
       
        characterSkin.AddSkin(skeletonData.FindSkin(InitaialSkin[activeWeaponIndex]));
    }

    void AddEquipmentSkinsTo(Skin combinedSkin)
    {
        var skeleton = skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;

       
    }

    void UpdateCombinedSkin()
    {
        var skeleton = skeletonAnimation.Skeleton;
        var resultCombinedSkin = new Skin("character-combined");

        resultCombinedSkin.AddSkin(characterSkin);
        AddEquipmentSkinsTo(resultCombinedSkin);

        skeleton.SetSkin(resultCombinedSkin);
        skeleton.SetSlotsToSetupPose();
    }
    void LoadWeaPon()
    {
        activeWeaponIndex = PlayerPrefs.GetInt("activeWeaponIndex");
    }
    void SaveWeaPones()
    {

        if (GameManager.saveweapon)
        {
            PlayerPrefs.SetInt("activeWeaponIndex", activeWeaponIndex);
            PlayerPrefs.Save();
            IsWeapon = true;
        }
        else
        {
            IsWeapon = false;
        }
    }
}
