using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_BasicAI : MonoBehaviour
{
    //Dylan
    private ScoreCount kill;

    enum State { Idle, Patrol, Chase, Attack, AttackInPlace, Dying, RunToCover, InCover }

    State currentState;

    [Header("--- IMPLEMENTATION ---")]

    [Header("Guard Style: Stationary Guard")]
    [Tooltip("If Guard is stationary, ignore the patrol guard options")]
    public bool stationaryGuard;

    [Header("Guard Style: Patrol Guard")]
    public float patrolPointDistance;
    public float patrolPointSwitchDistance;
    public int curPatrolPoint;
    public int maxPatrolPoint;
    [Tooltip("For the Patrol Guard, only change the array below. Do not touch the values above")]
    public Transform[] patrolPointArray;

    [Header("GuardStyle: Chase or go to Cover")]
    public bool goToCover;
    public float runToCoverSpeed;
    public float insideCoverDistance;
    public float coverDistance;
    [Tooltip("If the bot chases the player, ignore these points")]
    public Transform coverPoint;

    [Header("Check for Player")]
    public bool isCheckingWithRayCast;
    public bool isCheckingWithDistanceAndAngle;

    [Header("--- DO NOT TOUCH ---")]

    [Header("StateMachine")]
    public string enemyCurrentState;

    [Header("References")]
    public Transform target;
    private NavMeshAgent navyMeshy;
    public Scr_HealthScript healthScript;
    public Scr_EnemyShoot enemyShoot;
    public Rigidbody rgbd;
    public LayerMask vLayer;
    private Scr_alertManager alertArray;
    private Animator animator;
    //private Animator anim;

    [Header("Bools")]
    public bool boolChase;
    public bool inLineOfSight;

    public bool isDead;

    [Header("Floats")]
    public float floatDistance;
    public float floatAlertedRange;
    //public float floatMeleeRange;
    public float floatAttackInPlaceRange;
    public float attackWiggleDistance;
    public float floatSpeedHolder;
    public float floatMeleeTime;
    public float rayDistance;
    public float viewDistance;
    public float viewAngle;
    public float chaseSpeed;

    void Awake()
    {
        healthScript = this.gameObject.GetComponent<Scr_HealthScript>();
        navyMeshy = this.gameObject.GetComponent<NavMeshAgent>();

        enemyShoot = this.gameObject.GetComponentInChildren<Scr_EnemyShoot>();
        navyMeshy.speed = floatSpeedHolder;
        rgbd = this.gameObject.GetComponent<Rigidbody>();

        //anim = this.gameObject.GetComponent<Animator>();
        //target = GameObject.FindWithTag("Player").transform;
        target = GameObject.FindWithTag("MainOVR").transform;
        animator = GetComponentInChildren<Animator>();


        //This will decide if the enemy starts in an idle state or a patrol state
        if (stationaryGuard) { this.currentState = State.Idle; }
        else { this.currentState = State.Patrol; }
    }

    void Start()
    {

        boolChase = false;
        inLineOfSight = false;

        if (stationaryGuard == false)
        { this.currentState = State.Patrol; GoToNextPoint(); }
        else { this.currentState = State.Idle; }

        curPatrolPoint = 0;
        maxPatrolPoint = patrolPointArray.Length;
        patrolPointSwitchDistance = 1;
        floatAttackInPlaceRange = 3f;
        attackWiggleDistance = floatAttackInPlaceRange + 2;
    }


    void Update()
    {

        //Dylan
        transform.LookAt(target);
        GameObject theScore = GameObject.Find("ScoreCounter");
        ScoreCount scoreBoard = theScore.GetComponent<ScoreCount>();
        if (isDead)
        {
            scoreBoard.score += 1;

        }
        //Dylan End


        switch (this.currentState)
        {
            case State.Idle: this.Idle(); break;
            case State.Patrol: this.Patrol(); break;
            case State.Chase: this.Chase(); break;
            case State.Attack: this.Attack(); break;
            case State.AttackInPlace: this.AttackInPlace(); break;
            case State.Dying: this.Dying(); break;
            case State.RunToCover: this.RunToCover(); break;
            case State.InCover: this.InCover(); break;
        }

        enemyCurrentState = "" + currentState;

        floatDistance = Vector3.Distance(transform.position, target.transform.position);

        if (!stationaryGuard)
        {
            patrolPointDistance = Vector3.Distance(transform.position, patrolPointArray[curPatrolPoint].transform.position);
        }
        if (goToCover)
        {
            coverDistance = Vector3.Distance(transform.position, coverPoint.transform.position);
        }

        if (healthScript.curHealth < healthScript.maxHealth)
        {
            Alerted();
        }
        //MELEE, TO DO IN THE FUTURE
        /*if (boolChase && floatDistance<=floatMeleeRange)
		{
			navyMeshy.speed=0;
			StartCoroutine(Melee());
			
		}*/

    }

    void FixedUpdate()
    {
        //-------RAYCAST
        if (isCheckingWithRayCast)
        {
            /*RaycastHit hit;
            Debug.DrawRay (transform.position +transform.up *0.75f, transform.TransformDirection(Vector3.forward*rayDistance), Color.red);
            if (Physics.Raycast (transform.position+transform.up *0.75f, transform.TransformDirection(Vector3.forward*rayDistance), out hit))
            if (hit.collider.CompareTag("MainOVR")) {
                Alerted();
                inLineOfSight=true;
            }
            else 
            {
                inLineOfSight =false;
                enemyShoot.isShooting=false;
            }*/


            Ray tRay = new Ray(transform.position + transform.up * .75f, transform.TransformDirection(Vector3.forward * rayDistance));
            Debug.DrawRay(transform.position + transform.up * .75f, transform.TransformDirection(Vector3.forward * rayDistance), Color.red);
            RaycastHit tHit;

            if (Physics.Raycast(tRay, out tHit, rayDistance, vLayer))
            {
                if (tHit.collider.CompareTag("MainOVR"))
                {
                    Alerted();
                    inLineOfSight = true;
                }
                else
                {
                    inLineOfSight = false;
                    enemyShoot.isShooting = false;
                }
            }
        }
        //-------DISTANCE + ANGLE CHECK
        if (isCheckingWithDistanceAndAngle)
        {
            Vector3 direction = target.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(target.position, this.transform.position) < viewDistance && angle < viewAngle)
            {
                //inLineOfSight=true;
                transform.LookAt(target);
            }
            /*else 
            {
                inLineOfSight =false;
                enemyShoot.isShooting=false;
            }
            }*/
            /*if (inLineOfSight)
            {
                boolChase=true;
            }*/


        }
    }



    void Idle()
    {
        if (boolChase && !goToCover)
        { currentState = State.Chase; }

        if (boolChase && goToCover)
        { currentState = State.RunToCover; }

        /*if (floatDistance<=floatAlertedRange)
		{currentState=State.Chase;}*/

        if (healthScript.curHealth <= 0) { currentState = State.Dying; }

        return;

    }
    void Patrol()
    {

        //-- WALKING
        animator.SetBool("isWalking", true);
        animator.SetBool("isShootingWalking", false);
        animator.SetBool("isShootingInPlace", false);
        animator.SetBool("isRunning", false);

        navyMeshy.speed = floatSpeedHolder;

        if (boolChase && !goToCover)
        { currentState = State.Chase; }

        if (boolChase && goToCover)
        { currentState = State.RunToCover; }

        if (patrolPointDistance <= patrolPointSwitchDistance)
        {
            Debug.Log("Off to the next point");
            curPatrolPoint++;
            GoToNextPoint();
        }
        if (curPatrolPoint >= maxPatrolPoint)
        {
            curPatrolPoint = 0;
        }


        //GoToNextPoint();

        if (healthScript.curHealth <= 0) { currentState = State.Dying; }
        return;

    }
    void GoToNextPoint()
    {
        if (curPatrolPoint < maxPatrolPoint)
        { navyMeshy.destination = patrolPointArray[curPatrolPoint].position; }
        else if (curPatrolPoint >= maxPatrolPoint)
        {
            navyMeshy.destination = patrolPointArray[0].position;
            curPatrolPoint = 0;
        }


    }

    void Chase()
    {
        //-- WALKING
        animator.SetBool("isWalking", true);
        animator.SetBool("isShootingWalking", false);
        animator.SetBool("isShootingInPlace", false);
        animator.SetBool("isRunning", false);

        boolChase = true;
        if (healthScript.curHealth <= 0) { currentState = State.Dying; }
        if (floatDistance <= floatAttackInPlaceRange && inLineOfSight) { currentState = State.AttackInPlace; }
        navyMeshy.speed = chaseSpeed;
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        //raycast, when player in line of sight THEN start shooting

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = State.Attack;
        }

        if (inLineOfSight) { currentState = State.Attack; }
        transform.LookAt(target);
        //rgbd.constraints = RigidbodyConstraints.None;
        return;
    }

    void Attack()
    {
        //--SHOOT AND WALK
        animator.SetBool("isShootingWalking", true);
        animator.SetBool("isShootingInPlace", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);

        if (floatDistance <= floatAttackInPlaceRange) { currentState = State.AttackInPlace; }
        if (healthScript.curHealth <= 0) { currentState = State.Dying; }
        enemyShoot.isShooting = true;
        if (!inLineOfSight) { currentState = State.Chase; }
        transform.LookAt(target);
        //rgbd.constraints = RigidbodyConstraints.None;
        return;

    }

    void AttackInPlace()
    {
        //--SHOOT IN PLACE
        animator.SetBool("isShootingInPlace", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isShootingWalking", false);

        if (floatDistance > attackWiggleDistance) { currentState = State.Chase; }
        if (healthScript.curHealth <= 0) { currentState = State.Dying; }

        enemyShoot.isShooting = true;
        navyMeshy.speed = 0;
        transform.LookAt(target);
        //rgbd.constraints = RigidbodyConstraints.FreezeRotationX |RigidbodyConstraints.FreezeRotationZ;
        return;
    }


    private IEnumerator Melee()
    {
        navyMeshy.speed = 0;
        Debug.Log("EnemyMelee");
        yield return new WaitForSeconds(floatMeleeTime);
        navyMeshy.speed = chaseSpeed;
    }

    void Dying()
    {
        animator.SetBool("isDying", true);

        boolChase = false;
        enemyShoot.enabled = false;
        navyMeshy.speed = 0;
        this.gameObject.GetComponent<Collider>().enabled = false;
        isDead = true;
        Destroy(this.gameObject, 3f);
    }

    void RunToCover()
    {
        //-- RUNNING
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isShootingWalking", false);
        animator.SetBool("isShootingInPlace", false);

        transform.LookAt(coverPoint);
        if (healthScript.curHealth <= 0) { currentState = State.Dying; }
        if (insideCoverDistance >= coverDistance) { currentState = State.InCover; }
        navyMeshy.destination = coverPoint.position;
        navyMeshy.speed = runToCoverSpeed;
        return;
    }

    void InCover()
    {
        //--SHOOT IN PLACE
        animator.SetBool("isShootingInPlace", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isShootingWalking", false);

        if (healthScript.curHealth <= 0) { currentState = State.Dying; }
        if (inLineOfSight) { enemyShoot.isShooting = true; }
        else { enemyShoot.isShooting = false; }
        navyMeshy.speed = 0;
        transform.LookAt(target);
        rayDistance = 20f;
        return;
    }

    public void Alerted()
    {
        if (!boolChase)
        {
            boolChase = true;
            alertArray = FindObjectOfType<Scr_alertManager>();
            alertArray.AlertEnemiesInArray();

        }

    }
    //Dylan Addition
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
            healthScript.curHealth -= 3;
    }
}
