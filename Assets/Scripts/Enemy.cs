

using System.Collections;

using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public static Enemy ins;
    private void Awake()
    {
        ins = this;
    }
    public GameObject player;
    public float speed;
    public float speedPush;
    public int health = 100;
    public int currHeath;
    public Transform attackPoint;
    
   // public float attackRangeBoxing;
    public int damageAttack;
    public int intAttackCrit;
    public float miniumDistance;
    private Rigidbody2D rb;
    public bool isEnemyBlocked;
    public bool isEnemyAttacked = false;
    public bool startBlock;
    public GameObject fxWall;
    private bool isMove = true;
    private bool isStop = true;
    [SerializeField]private bool isDie = false;
    private bool DieBool = true;
    SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    public AnimationCurve curve;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isRandomEnemyBlock;
    [SerializeField] private bool isPlayerAttacked;
    //animation
    [SpineAnimation] public string blockAniamtionname;
    [SpineAnimation] public string idleAnimationName;
    [SpineAnimation] public string hitAniamtionName;
    [SpineAnimation] public string moveAnamationName;
    [SpineAnimation] public string dieAniamtionName;

    

    //animation
    [SpineAnimation] public string attackAnimationName;
    [SpineAnimation] public string attackCritAnimationName;
    [SerializeField] private bool attacked;

    [Header("HealthBar")]
    [SerializeField] private Slider healthEnemy;
    [SerializeField] private int currenhealthbar;
    [SerializeField] private TextMeshProUGUI txthealthbar;
    private void Start()
    {
       
        Time.timeScale = 1;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        rb = GetComponent<Rigidbody2D>();
        currHeath = health;
        isDie = false;
        StartCoroutine(AutoAttack());
        //silder health
        currenhealthbar = currHeath;
        healthEnemy.maxValue = currHeath;
        healthEnemy.value = currHeath;
        txtRound.text = "ROUND" + (win + 1);
        Rewardscoins.text = "" + Rewardscoin;
    }
    [Header("win/lose")]
    private static int win=0;
    public TextMeshProUGUI txtRound;
    public GameObject scrWin;
    public int Rewardscoin =1;
    public TextMeshProUGUI Rewardscoins;
    public void DamageTake(int damageTake)
    {
        if (currHeath > 0)
        {
            currHeath -= damageTake;
            DamagePush();
            healthEnemy.value = currHeath;
        }
        //  if (currHeath <= 0){
        //     //DamagePush();
        //     if(DieBool){
        //         Die();
        //         DieBool = false;
        //     }
        // }
    }
    private void scrWin1()
    {  //Time.timeScale = 0;
        scrWin.SetActive(true);     
        //SaveCoins.instance.money += Rewardscoin 
    }
    private void Update()
    {
       
        CheckDie();
        Move();
       if(!isDie)
        {
         if (attacked)
          {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("attack");
                attacked = false;
                Attack();
                StartCoroutine(AttackCountdownCoroutine(.6f));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("attackcrit");
                AttackCrit();
                StartCoroutine(AttackCountdownCoroutine(.6f));
            }
        }

        }   
        if(win>=2)
        {
            scrWin1();
           
        }
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
        txthealthbar.text = "" + currHeath;
       
    }
    IEnumerator AttackCountdownCoroutine(float boxingAttackCountdown)
    {
        yield return new WaitForSeconds(boxingAttackCountdown);
        attacked = true;
    }
    private void Move()
    {
        if (!isDie)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > miniumDistance)
            {
                if (isMove)
                {
                    StartCoroutine(MoveAnimation());
                    isMove = false;
                }
            }
            else
            {
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
    private void DamagePush()
    {
        rb.AddForce(Vector3.right * speedPush);
        spineAnimationState.SetAnimation(0, hitAniamtionName, false);
        spineAnimationState.AddAnimation(0, idleAnimationName, true, 0);
        FxEnemy.ins.ShowPowFx();
    }
    IEnumerator BlockCoroutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        spineAnimationState.SetAnimation(0, blockAniamtionname, false);
        yield return new WaitForSeconds(.1f);
        isEnemyBlocked = true;
        yield return new WaitForSeconds(.3f);
        isEnemyBlocked = false;
    }

    public void Block(float timeDelay)
    {
        //timedelay = .1 thi block, khac 0.1 thi block hut
        StartCoroutine(BlockCoroutine(timeDelay));
    }
    IEnumerator MoveAnimation()
    {
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
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        Debug.Log("enemy die cmnr");
        isDie = true;
        rb.AddForce(Vector3.right * speedPush / 30);
        spineAnimationState.SetAnimation(0, hitAniamtionName, false);
        //spineAnimationState.AddAnimation(0, dieAniamtionName, false, 0);
        yield return new WaitForSeconds(0.4f);
        FxEnemy.ins.ShowDieFx();
         spineAnimationState.AddAnimation(0,dieAniamtionName, false, 0);
        yield return null;


        //yield return new WaitForSeconds(3f);
    }
    private void CheckDie()
    {
        if (currHeath <= 0)
        {
            if (DieBool)
            {
                Die();
                DieBool = false;
                isDie = true;
                isStartAttack = false;
                win += 1;
                if(win==1)
                {
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneIndex);
                    
                }
            }
        }
    }
    public void turnoffscrwin()
    {
        scrWin.gameObject.SetActive(false);
        win = 0;
        SaveCoins.instance.money += Rewardscoin;
    }    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2Int(-2, 0);
            StartCoroutine(hitWall());
        }
    }
    IEnumerator hitWall()
    {
        fxWall.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fxWall.SetActive(false);
    }
    // danh thuong
    public void Attack()
    {
        //if(canAttack){
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitplayer)
        {
            if (player)
            {
                //enemy.GetComponent<Enemy>().DamageTake(damageAttack);
                //Debug.Log(enemy.name);
                StartCoroutine(AttackCoroutine());
                isMove = true;
            }
            else
            {
                return;
            }
        }
        spineAnimationState.SetAnimation(0, attackAnimationName, false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator AttackCoroutine()
    {
        canMove = false;
        isRandomEnemyBlock = true;
        if (isRandomEnemyBlock)
        {
            int randomValue = Random.Range(0, 10);
            //int randomValue = 0;
            Debug.Log(randomValue);
            isRandomEnemyBlock = false;
            if (randomValue == 0)
            {
                PlayerController.ins.Block(.1f);
            }
            // else if(randomValue == 1){
            //     Enemy.ins.Block(.4f);
            // }
        }
        yield return new WaitForSeconds(0.3f);
        isEnemyAttacked = true;

        if (!PlayerController.ins.isPlayerBlock)
        {
            player.GetComponent<PlayerController>().DamageTake(damageAttack);
        }
        else if (PlayerController.ins.isPlayerBlock && isEnemyAttacked)
        {
            FxPlayer.inst.ShowBlockFx();
            Debug.Log("player block");
        }
        if (isPlayerAttacked)
        {
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
    //danh chi mang
    public void AttackCrit()
    {
        //if(canAttack){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (Collider2D player in hitPlayer)
        {
            if (player)
            {
                //enemy.GetComponent<Enemy>().DamageTake(damageAttack);
                //Debug.Log(enemy.name);
                StartCoroutine(AttackCritCoroutine());
                isMove = true;
            }
            else
            {
                return;
            }
        }
        spineAnimationState.SetAnimation(0, attackCritAnimationName, false)
        ;
        //}
    }
    //delay
    IEnumerator AttackCritCoroutine()
    {
        canMove = false;
        isRandomEnemyBlock = true;
        if (isRandomEnemyBlock)
        {
            int randomValue = Random.Range(0, 2);
            //int randomValue = 0;
            Debug.Log(randomValue);
            isRandomEnemyBlock = false;
            if (randomValue == 0)
            {
                Enemy.ins.Block(.1f);
            }
            // else if(randomValue == 1){
            //     Enemy.ins.Block(.4f);
            // }
        }
        yield return new WaitForSeconds(0.3f);
        isEnemyAttacked = true;

        if (!PlayerController.ins.isPlayerAttacked)
        {
            player.GetComponent<PlayerController>().DamageTake(damageAttack * intAttackCrit);
        }
        else if (PlayerController.ins.isPlayerBlock && isEnemyAttacked)
        {
            FxEnemy.ins.ShowBlockFx();
            Debug.Log("block player");
        }
        if (isEnemyAttacked)
        {
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
    public bool isStartAttack = true;
    IEnumerator AutoAttack()
    {
        while (isStartAttack)
        { 
            
            {
             yield return new WaitForSeconds(3f);
            Attack();
            }    
         

        }
    }
   
}
