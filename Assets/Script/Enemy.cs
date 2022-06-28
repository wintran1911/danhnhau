using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Enemy : MonoBehaviour
{
    public static Enemy ins;
    private void Awake() {
        ins = this;
    }
    public GameObject player;
    public float speed;
    public float speedPush;
    public int health = 100; 
    public int currHeath;
    public Transform attackPoint;
    public LayerMask playerMask;
    public float attackRangeBoxing;
    public int damageAttackBoxing;
    public float miniumDistance;
    private Rigidbody2D rb;
    public bool isEnemyBlocked;
    public bool startBlock;
    public GameObject fxWall;
    private bool isMove = true;
    private bool isStop = true;
    private bool isDie = false;
    private bool DieBool = true;
    //animation
    SkeletonAnimation skeletonAnimation;
	public Spine.AnimationState spineAnimationState;
	public Spine.Skeleton skeleton;
    [SpineAnimation] public string blockAniamtionname;
    [SpineAnimation] public string idleAnimationName;
    [SpineAnimation] public string hitAniamtionName;
    [SpineAnimation] public string moveAnamationName;
    [SpineAnimation] public string dieAniamtionName;
    private void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
		spineAnimationState = skeletonAnimation.AnimationState;
		skeleton = skeletonAnimation.Skeleton;
        rb = GetComponent<Rigidbody2D>();
        currHeath = health;
        isDie = false;
    }
    public void DamageTake(int damageTake){
        if(currHeath > 0){
            currHeath -= damageTake;
            DamagePush();
        }
        //  if (currHeath <= 0){
        //     //DamagePush();
        //     if(DieBool){
        //         Die();
        //         DieBool = false;
        //     }
        // }
    }
    private void Update() {
        CheckDie();
        Move();
        // if(startBlock){
        //     StartCoroutine(BlockCoroutine());
        //     startBlock = false;
        // }
        // if(Input.GetMouseButtonDown(0)){
        //     StartCoroutine(BlockCoroutine());
        // }
        // else {
        //     spineAnimationState.AddAnimation(0, idleAnimationName, false, 0);
        // }

    }
    private void Move(){
        if(!isDie){
            if(Vector2.Distance(transform.position, player.transform.position) > miniumDistance){
             if(isMove){
                StartCoroutine(MoveAnimation());
                isMove = false;
            }
        }
         else{
            //    if(isStop){
            //     StartCoroutine(IdleAnimation());
            //     isStop = false;
            //     }
            spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        }
        }
    }
    
    // private void DamagePush(){
    //     transform.Translate(Vector3.right * speedPush * Time.deltaTime);
    // }
    private void DamagePush(){
        rb.AddForce(Vector3.right * speedPush );
        spineAnimationState.SetAnimation(0, hitAniamtionName, false);
        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        FxEnemy.ins.ShowPowFx();
    }
    IEnumerator BlockCoroutine(float timeDelay){
        yield return new WaitForSeconds(timeDelay);
        spineAnimationState.SetAnimation(0, blockAniamtionname, false);
        yield return new WaitForSeconds(.1f);
        isEnemyBlocked = true;
        yield return new WaitForSeconds(.3f);
        isEnemyBlocked = false; 
    }
    public void Block(float timeDelay){
        StartCoroutine(BlockCoroutine(timeDelay));
    }
    IEnumerator MoveAnimation(){
         spineAnimationState.SetAnimation(0, moveAnamationName, false).TimeScale = 1.5f;
         yield return new WaitForSeconds(.111f);
         //rb.AddForce(Vector3.right * speed * Time.deltaTime);
         rb.velocity = new Vector2(-1.5f, 0);
         yield return new WaitForSeconds(.444f);
         isMove = true;
    }
    // IEnumerator IdleAnimation(){
    //     spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
    //     yield return new WaitForSeconds(1f);
    //     isStop = true;
    // }
    private void Die(){
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine(){
        isDie = true;
        rb.AddForce(Vector3.right * speedPush / 30 );
        spineAnimationState.SetAnimation(0, hitAniamtionName, false);
        //spineAnimationState.AddAnimation(0, dieAniamtionName, false, 0);
        yield return new WaitForSeconds(0.4f);
        FxEnemy.ins.ShowDieFx();
        spineAnimationState.AddAnimation(0, dieAniamtionName, false, 0);
        yield return null;
        
        
        //yield return new WaitForSeconds(3f);
    }
    private void CheckDie(){
        if(currHeath <= 0){
              if(DieBool){
                 Die();
                 DieBool = false;
             }
        }
    }
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
}
