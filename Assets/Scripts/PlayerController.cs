
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask ememyLayers;
    public float attackRange = 1.5f;
    [Header("Damage")]
    public int[] damageAttack ;
    public int selecDamage;
    public int damageAttackcrit;
    public int    health = 100;
    public int currHealth;
    private Rigidbody2D rb;
    public GameObject enemy;
    public GameObject fxWall;
    public float speed;
    public float miniumDistance;
    public bool attacked;
    public bool isPlayerAttacked = false;
    public bool isPlayerBlock;
    public bool startAttack = false;
    public bool isRandomEnemyBlock;
    private bool isMove = true;
    private  bool isStop = true;
    public AnimationCurve  curve;
    public bool canAttack = false;
    public bool canMove = true;
    private bool isDie = false;
    private bool DieBool = true;
    float timetriger;
    [Header("Boxing")]
    [SpineAnimation] public string idleAnimationName;
    [SpineAnimation] public string attackAnimationName;
    [SpineAnimation] public string attackCritAnimationName;
    [SpineAnimation] public string moveAnamationName;
    [SpineAnimation] public string blockAniamtionname;    
    [SpineAnimation] public string dieAniamtionName;
    [SpineAnimation] public string hitAniamtionName;
    [Header("Gangtay")]
    [SpineAnimation] public string idleAnimationGT;
    [SpineAnimation] public string attackAnimationGT;
    [SpineAnimation] public string attackCritAnimationGT;
    [SpineAnimation] public string moveAnamationGT;
    [SpineAnimation] public string blockAniamtionGT;
    [SpineAnimation] public string dieAniamtionGT;
    [SpineAnimation] public string hitAniamtionGT;
    [Header("1hand")]
    [SpineAnimation] public string idleAnimation1h;
    [SpineAnimation] public string attackAnimation1h;
    [SpineAnimation] public string attackCritAnimation1h;
    [SpineAnimation] public string moveAnamation1h;
    [SpineAnimation] public string blockAniamtion1h;
    [SpineAnimation] public string dieAniamtion1h;
    [SpineAnimation] public string hitAniamtion1h;
    [Header("2hands")]
    [SpineAnimation] public string idleAnimation2h;
    [SpineAnimation] public string attackAnimation2h;
    [SpineAnimation] public string attackCritAnimation2h;
    [SpineAnimation] public string moveAnamation2h;
    [SpineAnimation] public string blockAniamtion2h;
    [SpineAnimation] public string dieAniamtion2h;
    [SpineAnimation] public string hitAniamtion2h;
    [Header("mix_pinwheel")]
    [SpineAnimation] public string mix_pinwhell;
    SkeletonAnimation skeletonAnimation;
	public Spine.AnimationState spineAnimationState;
	public Spine.Skeleton skeleton;
    public static PlayerController ins;

    //selection weapon
    private int SlWeapon;
    [SerializeField]private float speedPush;
    
    [SerializeField] private bool isEnemyBlocked;

    [SerializeField] private int[] Hp;
  
    [Header("swipe controller")]
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;
    public float swipeRange;
    public float tapRange;
    

    [Header("HealthBar")]
    [SerializeField] private Slider healthplayer;
    [SerializeField] private int currenhealthbar;
    [SerializeField] private TextMeshProUGUI txthealthbar;

    [Header("lose/win")]
    private static int lose = 0;
    
    public GameObject scrLose;
   
    public void  turnoffscrlose()
    {
        lose = 0;
        scrLose.gameObject.SetActive(true);
    }    
    private void Awake()
    {
        ins = this;
    }
    private void Start() {
        Time.timeScale = 1;
        ///weapon
        SlWeapon = WeaPonPlayer.activeWeaponIndex;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
		spineAnimationState = skeletonAnimation.AnimationState;
		skeleton = skeletonAnimation.Skeleton;
        //damage
        health = PlayerPrefs.GetInt("levelplayerIndex");
        selecDamage = WeaPonPlayer.activeWeaponIndex;
        // currHealth = Hp[intHp];
        currHealth = Hp[health];
        rb = GetComponent<Rigidbody2D>();
        isDie = false;
        //silder health
        currenhealthbar = currHealth;
        healthplayer.maxValue = currHealth;
        healthplayer.value = currHealth;
       }
      private void Update() 
      {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
            
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {

                if (Distance.x < -swipeRange)
                {
                  
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                  
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange)
                {
                    
                       // Debug.Log("LEFT");
                        if (PlayerManager.ins.manaCurrent > 0 && !PlayerManager.ins.isFullManaBar)
                        {
                            PlayerManager.ins.manaCurrent--;
                            UIManager.ins.UpdateManaPowerPlayer(PlayerManager.ins.manaCurrent / 5f);
                            attacked = false;
                            Attack();
                            StartCoroutine(AttackCountdownCoroutine(.6f));
                        }
                        if (PlayerManager.ins.isFullManaBar)
                        {
                            PlayerManager.ins.manaCurrent --;
                            UIManager.ins.UpdateManaPowerPlayer(PlayerManager.ins.manaCurrent / 5f);
                            PlayerManager.ins.UpdataManabar();
                            AttackCrit();
                            StartCoroutine(AttackCountdownCoroutine(.6f));
                        

                    }
                   
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                   
                    stopTouch = true;
                    
                       
                    
                }

            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                
                attacked = false;
                Block(0.1f);
               // Debug.Log("RIGHT");
            }

        }
        if (lose >= 2)
        {
            ShowLose();
            //SaveCoins.instance.money += Rewardscoin;
        }
        txthealthbar.text ="" + currHealth; 
        // if(rb.velocity == new Vector2(0, 0)){
        //     canAttack = true;
        CheckDie();// }
        if(!isDie)
        {
            Move();
          //CheckStateAnimation();
        /*if(attacked){
            if(Input.GetMouseButtonDown(0)){
                    Debug.Log("LEFT");
                if(PlayerManager.ins.manaCurrent > 0 && !PlayerManager.ins.isFullManaBar){
                    PlayerManager.ins.manaCurrent--;
                    UIManager.ins.UpdateManaPower(PlayerManager.ins.manaCurrent/5f);
                    attacked = false;
                    Attack();
                    StartCoroutine(AttackCountdownCoroutine(.6f));
                }
                if(PlayerManager.ins.isFullManaBar){
                    PlayerManager.ins.manaCurrent = 0;
                    UIManager.ins.UpdateManaPower(PlayerManager.ins.manaCurrent/5f);
                    PlayerManager.ins.UpdataManabar();
                    AttackCrit();
                    StartCoroutine(AttackCountdownCoroutine(.6f));
                }
              
            }
            if(Input.GetMouseButtonDown(1)){
                attacked = false;
                Block(0.1f);
                    Debug.Log("RIGHT");
                }   
        }
       */
       
        }

        //Debug.Log(curve.Evaluate(.1f));

    }
    public void Attack(){
       //if(canAttack){
         Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,ememyLayers );
        foreach(Collider2D enemy in hitEnemy){
            if(enemy){
                //enemy.GetComponent<Enemy>().DamageTake(damageAttack);
                //Debug.Log(enemy.name);
                StartCoroutine(AttackCoroutine());
                isMove = true;
            }
            else {
                return;
            }
        }
        if(SlWeapon ==0|| SlWeapon==1 || SlWeapon == 7)
        {
         spineAnimationState.SetAnimation(0, attackAnimationName, false);
        // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon==3  ||SlWeapon==8 ||SlWeapon==9)
        {
            spineAnimationState.SetAnimation(0, attackAnimation1h, false);
            //spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5||SlWeapon==6)
        {
            spineAnimationState.SetAnimation(0, attackAnimation2h, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if(SlWeapon==4)
        {
            spineAnimationState.SetAnimation(0, attackAnimationGT, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        //}
    }
    public void AttackCrit(){
         //if(canAttack){
         Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,ememyLayers );
        foreach(Collider2D enemy in hitEnemy){
            if(enemy){
                //enemy.GetComponent<Enemy>().DamageTake(damageAttack);
                //Debug.Log(enemy.name);
                StartCoroutine(AttackCritCoroutine());
                isMove = true;
            }
            else {
                return;
            }
        }
        if (SlWeapon == 0 || SlWeapon == 1 || SlWeapon == 7)
        {
            spineAnimationState.SetAnimation(0, attackCritAnimationName, false);
            //spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon == 3  || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(0, attackCritAnimation1h, false);
            //spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(0, attackCritAnimation2h, false);
            //spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 4)
        {
            spineAnimationState.SetAnimation(0, attackAnimationGT, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
       
       //}
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
  
    private void Move(){
        if(canMove){
             if(Vector2.Distance(transform.position, enemy.transform.position) > miniumDistance){
            //Debug.Log("Move");
           if(canMove){
             if(isMove){
                        spineAnimationState.SetAnimation(1, mix_pinwhell, true);
                        StartCoroutine(MoveAnimation());
                isMove = false;
            }
           }
        }
        
        else{
                // if(isStop){
                //     StartCoroutine(IdleAnimation());
                //     isStop = false;
                // }
                if (SlWeapon == 0 || SlWeapon == 1 || SlWeapon == 7)
                {
                    spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
                   // spineAnimationState.AddAnimation(1, mix_pinwhell, true,0).TimeScale = 1.5f;
                }
                if (SlWeapon == 2 || SlWeapon == 3  || SlWeapon == 8 || SlWeapon == 9)
                {
                    spineAnimationState.AddAnimation(0, idleAnimation1h, true, 0);
                   // spineAnimationState.AddAnimation(1, mix_pinwhell, true, 0).TimeScale = 1.5f;
                }
                if (SlWeapon == 5 || SlWeapon == 6)
                {
                    spineAnimationState.AddAnimation(0, idleAnimation2h, true, 0);
                   // spineAnimationState.AddAnimation(1, mix_pinwhell, true, 0).TimeScale = 1.5f;
                }
                if (SlWeapon == 4)
                {
                      spineAnimationState.AddAnimation(0, idleAnimationGT, true, 0);
                  //  spineAnimationState.AddAnimation(1, mix_pinwhell, true, 0).TimeScale = 1.5f;
                }
               
        }
        }

    }
    IEnumerator AttackCoroutine(){
        canMove = false;
        isRandomEnemyBlock = true;
        if(isRandomEnemyBlock){
                int randomValue = Random.Range(0,10);
                //int randomValue = 0;
              //  Debug.Log(randomValue);
                isRandomEnemyBlock = false;
                if(randomValue == 0){
                    Enemy.ins.Block(.1f);
                }
                // else if(randomValue == 1){
                //     Enemy.ins.Block(.4f);
                // }
            }
        yield return new WaitForSeconds(0.3f);
        isPlayerAttacked = true;

        if(!Enemy.ins.isEnemyBlocked){
            enemy.GetComponent<Enemy>().DamageTake(damageAttack[selecDamage]);
        }
        else if(Enemy.ins.isEnemyBlocked && isPlayerAttacked){
           FxEnemy.ins.ShowBlockFx();
            Debug.Log("enemy block");
        }
        if(isPlayerAttacked){
            // if(isRandomEnemyBlock){
            //     //int randomValue = Random.Range(0,3);
            //     int randomValue = 0;
            //     Debug.Log(randomValue);
            //     isRandomEnemyBlock = false;
            //     if(randomValue == 0){
            //         Enemy.ins.Block(0f);
            //     }
            //     else if(randomValue == 1){
            //         Enemy.ins.Block(.3f);
            //     }
            // }
        }
        yield return new WaitForSeconds(.2f);
        isPlayerAttacked = false;
        canMove = true;
    }
     IEnumerator AttackCritCoroutine(){
        canMove = false;
        isRandomEnemyBlock = true;
        if(isRandomEnemyBlock){
                int randomValue = Random.Range(0,2);
                //int randomValue = 0;
                Debug.Log(randomValue);
                isRandomEnemyBlock = false;
                if(randomValue == 0){
                    Enemy.ins.Block(.1f);
                }
                // else if(randomValue == 1){
                //     Enemy.ins.Block(.4f);
                // }
            }
        yield return new WaitForSeconds(0.3f);
        isPlayerAttacked = true;

        if(!Enemy.ins.isEnemyBlocked){
            enemy.GetComponent<Enemy>().DamageTake(damageAttack[selecDamage]*damageAttackcrit);
        }
        else if(Enemy.ins.isEnemyBlocked && isPlayerAttacked){
           FxEnemy.ins.ShowBlockFx();
            Debug.Log("block enemy");
        }
        if(isPlayerAttacked){
            // if(isRandomEnemyBlock){
            //     //int randomValue = Random.Range(0,3);
            //     int randomValue = 0;
            //     Debug.Log(randomValue);
            //     isRandomEnemyBlock = false;
            //     if(randomValue == 0){
            //         Enemy.ins.Block(0f);
            //     }
            //     else if(randomValue == 1){
            //         Enemy.ins.Block(.3f);
            //     }
            // }
        }
        yield return new WaitForSeconds(.2f);
        isPlayerAttacked = false;
        canMove = true;
    }
    IEnumerator AttackCountdownCoroutine(float boxingAttackCountdown){
        yield return new WaitForSeconds(boxingAttackCountdown);
        attacked = true;
    }
    IEnumerator MoveAnimation(){
        //canAttack = true;
        if (SlWeapon == 0 || SlWeapon == 1 || SlWeapon == 7)
        {
            spineAnimationState.SetAnimation(0, moveAnamationName, false).TimeScale = 1.5f;
            spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon == 3  || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(0, moveAnamation1h, false).TimeScale = 1.5f;
            spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(0, moveAnamation2h, false).TimeScale = 1.5f;
            spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 4)
        {
            spineAnimationState.SetAnimation(0, moveAnamationGT, false).TimeScale = 1.5f;
            spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }

       // spineAnimationState.SetAnimation(0, moveAnamationName, false).TimeScale = 1.5f;
         yield return new WaitForSeconds(.111f);
         //rb.AddForce(Vector3.right * speed * Time.deltaTime);
         rb.velocity = new Vector2(1.5f, 0);
         yield return new WaitForSeconds(.444f);
         isMove = true;
    }
    // private void CheckStateAnimation(){
    //      if(Vector2.Distance(transform.position, enemy.transform.position) > miniumDistance){
    //        spineAnimationState.SetAnimation(0, moveAnamationName, true);

    //     }
    // }
    // IEnumerator IdleAnimation(){
    //     spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
    //     yield return new WaitForSeconds(1f);
    //     isStop = true;
    // }
    IEnumerator hitWall(){
        fxWall.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fxWall.SetActive(false);
    }
     IEnumerator BlockCoroutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (SlWeapon == 0 || SlWeapon == 1 || SlWeapon == 7)
        {
            spineAnimationState.SetAnimation(0, blockAniamtionname, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon == 3  || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(0, blockAniamtion1h, false);
            //spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(0, blockAniamtion2h, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 4)
        {
            spineAnimationState.SetAnimation(0, blockAniamtionGT, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }

        //spineAnimationState.SetAnimation(0, blockAniamtionname, false);
        yield return new WaitForSeconds(.1f);
        isPlayerBlock = true;
        yield return new WaitForSeconds(.3f);
        isPlayerBlock  =false;
        attacked = true;
    }
    public void Block(float timeDelay)
    {
        StartCoroutine(BlockCoroutine(timeDelay));
    }

    //dame
    public void DamageTake(int damageTake)
    {
        if (currHealth > 0)
        {
            currHealth -= damageTake;
            DamagePush();
            healthplayer.value = currHealth;
        }
        //  if (currHeath <= 0){
        //     //DamagePush();
        //     if(DieBool){
        //         Die();
        //         DieBool = false;
        //     }
        // }
    }
    private void DamagePush()
    {
        rb.AddForce(Vector3.right * -speedPush);
        if (SlWeapon == 0 || SlWeapon == 1||SlWeapon ==7)
        {
            spineAnimationState.SetAnimation(0, hitAniamtionName, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
            spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        }
        if (SlWeapon == 2 || SlWeapon == 3 || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(0, hitAniamtion1h, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
            spineAnimationState.AddAnimation(0, idleAnimation1h, true, 0);
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(0, hitAniamtion2h, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
            spineAnimationState.AddAnimation(0, idleAnimation2h, true, 0);
        }
        if (SlWeapon == 4)
        {
            spineAnimationState.SetAnimation(0, hitAniamtionGT, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
            spineAnimationState.AddAnimation(0, idleAnimationGT, true, 0);
        }

       // spineAnimationState.SetAnimation(0, hitAniamtionName, false);
       // spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
       FxPlayer.inst.ShowPowFx();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2Int(-2, 0);
            StartCoroutine(hitWall());
        }
    }
   
   
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        Debug.Log("diePlayerasmdnjasdlkaskdjkl");
        isDie = true;
        rb.AddForce(Vector3.right * speedPush / 30);
        if (SlWeapon == 0 || SlWeapon == 1 || SlWeapon == 7)
        {
            spineAnimationState.SetAnimation(0, hitAniamtionName, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon == 3  || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(0, hitAniamtion1h, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(0, hitAniamtion2h, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 4)
        {
            spineAnimationState.SetAnimation(0, hitAniamtionGT, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }

       // spineAnimationState.SetAnimation(0, hitAniamtionName, false);
        //spineAnimationState.AddAnimation(0, dieAniamtionName, false, 0);
        yield return new WaitForSeconds(0.4f);
        FxPlayer.inst.ShowDieFx();
        if (SlWeapon == 0 || SlWeapon == 1)
        {
            spineAnimationState.SetAnimation(1, dieAniamtionName, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 2 || SlWeapon == 3 || SlWeapon == 4 || SlWeapon == 8 || SlWeapon == 9)
        {
            spineAnimationState.SetAnimation(1, dieAniamtion1h, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 5 || SlWeapon == 6)
        {
            spineAnimationState.SetAnimation(1, dieAniamtion2h, false);
           // spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }
        if (SlWeapon == 7)
        {
            spineAnimationState.SetAnimation(1, dieAniamtionGT, false);
          //  spineAnimationState.SetAnimation(1, mix_pinwhell, true).TimeScale = 1.5f;
        }

       // spineAnimationState.SetAnimation(1, dieAniamtionName, false);
        yield return null;
       


        //yield return new WaitForSeconds(3f);
    }
    private void CheckDie()
    {
       
        if (currHealth <= 0)
        {
            if (DieBool)
            {
                Die();
                Debug.Log("die rui dcmn");
                DieBool = false;
                lose += 1;
                if (lose == 1)
                {
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneIndex);
                }
            }          
        }
    }
    public void ShowLose()
    {
        scrLose.gameObject.SetActive(true);
    }

}
