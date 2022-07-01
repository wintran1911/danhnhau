using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class idle_mixplayer : MonoBehaviour
{

    [Header("idle")]
    [SpineAnimation] public string idleAnimation12hand;
    [SpineAnimation] public string idleAnimationgangtay;
    [SpineAnimation] public string idleAnimationbonxing;
    [SpineAnimation] public string mix_pinwheel;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    SkeletonAnimation skeletonAnimation;
    Skin characterSkin;
    public int selectIdle;
    private void FixedUpdate()
    {
        openWeapon();
        selectIdle = WeaPonPlayer.activeWeaponIndex;
    }
    public void openWeapon()
    {
        if (selectIdle == 0)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);

        }
        if (selectIdle == 1)
        {
            spineAnimationState.SetAnimation(0, idleAnimationbonxing, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
            
        }
        if (selectIdle == 2)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
         
        }
        if (selectIdle == 3)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
       
        }
        if (selectIdle == 4)
        {
            spineAnimationState.SetAnimation(0, idleAnimationgangtay, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
           
        }
        if (selectIdle == 5)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
        }
        if (selectIdle == 6)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
        }
        if (selectIdle == 7)
        {
            spineAnimationState.SetAnimation(0, idleAnimationbonxing, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
        }
        if (selectIdle == 8)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
        }
        if (selectIdle == 9)
        {
            spineAnimationState.SetAnimation(0, idleAnimation12hand, true);
            spineAnimationState.SetAnimation(1, mix_pinwheel, true);
        }
    }
}


