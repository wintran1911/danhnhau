
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class PlayerController : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask ememyLayers;
    public float attackRange = 1.5f;
    public int damageAttack = 20;
    public float health = 100;
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
    
    float timetriger;
    // animation
    [SpineAnimation] public string idleAnimationName;
    [SpineAnimation] public string attackAnimationName;
    [SpineAnimation] public string attackCritAnimationName;
    [SpineAnimation] public string moveAnamationName;
    [SpineAnimation] public string blockAniamtionname;    
    
    SkeletonAnimation skeletonAnimation;
	public Spine.AnimationState spineAnimationState;
	public Spine.Skeleton skeleton;
    private void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
		spineAnimationState = skeletonAnimation.AnimationState;
		skeleton = skeletonAnimation.Skeleton;
        rb = GetComponent<Rigidbody2D>();
    }
      private void Update() {
        // if(rb.velocity == new Vector2(0, 0)){
        //     canAttack = true;
        // }
        Move();
        //CheckStateAnimation();
        if(attacked){
            if(Input.GetMouseButtonDown(0)){
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
                Block();
            }
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
        spineAnimationState.SetAnimation(0, attackAnimationName, false);
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
        spineAnimationState.SetAnimation(0, attackCritAnimationName, false)
        ;
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
            spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        }
        }

    }
    IEnumerator AttackCoroutine(){
        canMove = false;
        isRandomEnemyBlock = true;
        if(isRandomEnemyBlock){
                int randomValue = Random.Range(0,10);
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
            enemy.GetComponent<Enemy>().DamageTake(20);
        }
        else if(Enemy.ins.isEnemyBlocked && isPlayerAttacked){
            FxEnemy.ins.ShowBlockFx();
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
            enemy.GetComponent<Enemy>().DamageTake(40);
        }
        else if(Enemy.ins.isEnemyBlocked && isPlayerAttacked){
            FxEnemy.ins.ShowBlockFx();
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
         spineAnimationState.SetAnimation(0, moveAnamationName, false).TimeScale = 1.5f;
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
   
     private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall")){
            rb.velocity = new Vector2Int(-2, 0);
            StartCoroutine(hitWall());
        }
    }
    IEnumerator hitWall(){
        fxWall.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fxWall.SetActive(false);
    }
    IEnumerator BlockCoroutine(){
        yield return new WaitForSeconds(0.1f);
        spineAnimationState.SetAnimation(0, blockAniamtionname, false);
        yield return new WaitForSeconds(.1f);
        isPlayerBlock = true;
        yield return new WaitForSeconds(.3f);
        isPlayerBlock  =false;
        attacked = true;
    }
    public void Block(){
        StartCoroutine(BlockCoroutine());
    }
    

}
