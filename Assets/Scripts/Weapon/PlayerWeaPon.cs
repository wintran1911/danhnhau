using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Spine;
using UnityEngine.UI;

public class PlayerWeaPon : MonoBehaviour
{
    [SpineSkin] public string[] InitaialSkin = { "default", "boxing", "bua", "chao", "gang_tay", "gay_bong_chay", "gay_goft", "iron_fist", "muoi_chien_com", "thuoc_ke" };
   
    SkeletonAnimation skeletonAnimation;
    Skin characterSkin;

    public int weapon;
    // Start is called before the first frame update
    private void Awake()
    {
        skeletonAnimation = this.GetComponent<SkeletonAnimation>();
        
    }
    void Start()
    {

        //if (!PlayerPrefs.HasKey("activeWeaponIndex"))
        {
            weapon = WeaPonPlayer.activeWeaponIndex;
        }
       // else
        {
          //  LoadWeaPon();
        }
        UpdateCharacterSkin();
        UpdateCombinedSkin();

    }

    // Update is called once per frame
    void Update()
    {
        
       // SaveWeaPones();
    }
    
    public void UpdateCharacterSkin()
    {
        var skeleton = skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;
        characterSkin = new Skin("character-base");
        // Note that the result Skin returned by calls to skeletonData.FindSkin()
        // could be cached once in Start() instead of searching for the same skin
        // every time. For demonstration purposes we keep it simple here.

        characterSkin.AddSkin(skeletonData.FindSkin(InitaialSkin[weapon]));
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
    /*void LoadWeaPon()
    {
        weapon = PlayerPrefs.GetInt("weaponindex");
    }
    void SaveWeaPones()
    {
        if (GameManager.saveweapon)
        {
            PlayerPrefs.SetInt("weaponindex", weapon);
        }

    }*/
}
