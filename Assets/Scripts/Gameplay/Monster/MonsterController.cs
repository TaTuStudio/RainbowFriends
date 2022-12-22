using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cinemachine;

public class MonsterController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public Animator animator;

    public AIPath aIPath;

    public float turnTime = 0f;

    public bool setDefault = false;

    [Header("Turn settings")]

    public int turnType = -1;
    // turnType = 0 -> stay
    // turnType = 1 -> move to checkPoints
    // turnType = 2 -> move to one of player position
    PlayerController selectedPlayer;
    PlayerAIController selectedAIPlayer;

    public MonsterSpawner.MonsterSpawnInfo monsterInfo;

    [Header("Run to nearest settings")]
    //Run to nearest
    float runToRange = 5f;
    float runToNearestDelay = 3f;
    public float curRunToNearestDelay = 0f;
    public bool runToNearest = false;
    public Transform nearest = null;

    [Header("Attack settings")]
    //Attack
    float attackRange = 3f;
    float attackDelay = 1f;
    float curAttackDelay = 0f;
    [SerializeField]
    float hitDelay = 0f;
    [SerializeField]
    float hitScareDelay = 0f;
    float curHitDelay = 0f;

    public bool attacking = false;

    public SoundEffectSO jumpScareSfx;

    private void OnEnable()
    {
        setDefault = true;
    }

    private void Awake()
    {
        aIPath = GetComponent<AIPath>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.instance._RegisterVirtualCamera(virtualCam);
        CameraManager.instance._DeactiveCamera(virtualCam);
    }

    // Update is called once per frame
    void Update()
    {
        _Default();

        _SetAnimMove();

        _CheckTurn();

        _CheckFindPlayer();

        _CheckRunToNearest();

        _Attack();
    }

    public void _Default()
    {
        if (setDefault == false) return;

        setDefault = false;

        _TurnDefault();

        curAttackDelay = attackDelay;
        attacking = false;
        curHitDelay = hitDelay;

        runToNearest = false;
        nearest = null;

        _SetAnimJumpScare(false);
        _SetAnimAttack(false);
    }

    void _TurnDefault()
    {
        turnTime = 0f;

        turnType = -1;

        selectedPlayer = null;

        selectedAIPlayer = null;
    }

    void _CheckTurn()
    {
        if (runToNearest || attacking) return;

        if(turnTime > 0f)
        {
            turnTime -= Time.deltaTime;
        }

        if(turnTime <= 0f)
        {
            _TurnDefault();

            int ranIndex = Random.Range(0, 100);

            if(ranIndex < monsterInfo.moveToAnyWhereRate)
            {
                turnType = 0;
            }
            else if(ranIndex < monsterInfo.moveToAnyWhereRate + monsterInfo.moveToCheckPointRate && monsterInfo != null && monsterInfo.checkPoints.Count > 0)
            {
                turnType = 1;
            }
            else
            {
                turnType = 2;
            }

            if (turnType == 0)
            {
                turnTime = (float)Random.RandomRange(1, 3);

                aIPath._SetMoveToPosition(transform.position);

                //Debug.Log("Monster Stay");
            }
            else if (turnType == 1)
            {
                turnTime = (float)Random.RandomRange(5, 10);

                int checkPointIndex = Random.Range(0, monsterInfo.checkPoints.Count);

                Transform checkPoint = monsterInfo.checkPoints[checkPointIndex];

                aIPath._SetMoveToPosition(checkPoint.position);

                //Debug.Log("Monster Move to check point");
            }
            else if (turnType == 2)
            {
                turnTime = (float)Random.RandomRange(5, 10);

                int ranNum = Random.Range(0, 100);

                if(ranNum < 50)
                {
                    //find ai player

                    List<PlayerAIController> canUsePlayerAIList = new List<PlayerAIController>();

                    foreach(PlayerAIController aiController in PlayerManager.instance.spawnedAIPlayers)
                    {
                        if(aiController.isDead == false && aiController.isHiding == false)
                        {
                            canUsePlayerAIList.Add(aiController);
                        }
                    }

                    if (canUsePlayerAIList.Count > 0)
                    {
                        int ranAIIndex = Random.Range(0, canUsePlayerAIList.Count);

                        selectedAIPlayer = canUsePlayerAIList[ranAIIndex];
                    }
                    else if (PlayerManager.instance.spawnedPlayer.isDead == false && PlayerManager.instance.spawnedPlayer.isHiding == false)
                    {
                        selectedPlayer = PlayerManager.instance.spawnedPlayer;
                    }
                    else
                    {
                        turnTime = 0f;
                    }

                    //Debug.Log("Find AI Player");
                }
                else
                {
                    //find player

                    if (PlayerManager.instance.spawnedPlayer.isDead == false && PlayerManager.instance.spawnedPlayer.isHiding == false)
                    {
                        selectedPlayer = PlayerManager.instance.spawnedPlayer;
                    }
                    else
                    {
                        turnTime = 0f;
                    }

                    //Debug.Log("Find Player");
                }
            }
        }
    }

    void _CheckFindPlayer()
    {
        if (runToNearest || attacking) return;

        if (turnType == 2)
        {
            if(selectedAIPlayer != null)
            {
                if (selectedAIPlayer.isHiding)
                {
                    aIPath._SetMoveToPosition(transform.position);
                }
                else
                {
                    aIPath._SetMoveToPosition(selectedAIPlayer.transform.position);
                }
            }
            else if (selectedPlayer != null)
            {
                if (selectedPlayer.isHiding)
                {
                    aIPath._SetMoveToPosition(transform.position);
                }
                else
                {
                    aIPath._SetMoveToPosition(selectedPlayer.transform.position);
                }
            }
        }
    }

    void _CheckRunToNearest()
    {
        if (attacking) return; 

        List<Transform> transforms = new List<Transform>();

        foreach (PlayerAIController aiController in PlayerManager.instance.spawnedAIPlayers)
        {
            float dist = Vector3.Distance(transform.position, aiController.transform.position);

            if (aiController.isDead == false && aiController.isHiding == false && aiController.catched == false && dist < runToRange
                || aiController.isDead == false && aiController.catched == false && monsterInfo.avoidHide == true && dist < runToRange)
            {
                transforms.Add(aiController.transform);
            }
        }

        {
            PlayerController player = PlayerManager.instance.spawnedPlayer;

            float dist = Vector3.Distance(transform.position, player.transform.position);

            if (player.isDead == false && player.isHiding == false && player.catched == false && dist < runToRange
                                || player.isDead == false && player.catched == false && monsterInfo.avoidHide == true && dist < runToRange)
            {
                transforms.Add(player.transform);
            }
        }

        if(transforms.Count > 0)
        {
            nearest = transforms[0];

            for(int i = 1; i < transforms.Count; i++)
            {
                float curDist = Vector3.Distance(transform.position, transforms[i].position);
                float nearestDist = Vector3.Distance(transform.position, nearest.position);

                if(curDist < nearestDist)
                {
                    nearest = transform;
                }
            }

            aIPath._SetMoveToPosition(nearest.position);
        }
        else
        {
            nearest = null;
        }

        if(nearest != null && curRunToNearestDelay > 0f &&  runToNearest == false)
        {
            curRunToNearestDelay -= Time.deltaTime;

            if(curRunToNearestDelay <= 0f)
            {
                runToNearest = true;
            }
        }
        else if(nearest == null)
        {
            curRunToNearestDelay = runToNearestDelay;

            runToNearest = false;
        }
    }

    void _Attack()
    {
        if (attacking == false)
        {
            if (nearest != null)
            {
                if (curAttackDelay > 0)
                {
                    curAttackDelay -= Time.deltaTime;
                }

                if (curAttackDelay <= 0f)
                {
                    attacking = true;

                    _SetNearestCatched();

                    _SetAnimAttack(true);
                }
            }
            else
            {
                curAttackDelay = attackDelay;
            }
        }

        if (attacking == true)
        {
            if (curHitDelay > 0)
            {
                curHitDelay -= Time.deltaTime;
            }

            if (curHitDelay <= 0f)
            {
                attacking = false;

                curAttackDelay = attackDelay;

                _SetNearestHit();
            }
        }
    }

    void _SetNearestCatched()
    {
        if(nearest != null)
        {
            PlayerController playerController = nearest.GetComponent<PlayerController>();
            PlayerAIController playerAIController = nearest.GetComponent<PlayerAIController>();

            if (playerController != null && playerController.enabled)
            {
                curHitDelay = 2.22f;

                playerController._SetCatched();

                _SetAnimJumpScare(true);

                CameraManager.instance._GameplaySwitchCam(virtualCam);

                jumpScareSfx.Play(gameObject);
            }
            else
            if (playerAIController != null && playerAIController.enabled)
            {
                curHitDelay = 1.5f;

                playerAIController._SetCatched();
            }
        }
    }

    void _SetNearestHit()
    {
        if (nearest != null)
        {
            PlayerController playerController = nearest.GetComponent<PlayerController>();
            PlayerAIController playerAIController = nearest.GetComponent<PlayerAIController>();

            if (playerController != null && playerController.enabled)
            {
                playerController._SetHit();

                Debug.Log("Monster hit player");
            }
            else
            if (playerAIController != null && playerAIController.enabled)
            {
                playerAIController._SetHit();

                Debug.Log("Monster hit ai player");
            }
        }

        nearest = null;

        _SetAnimAttack(false);
        _SetAnimJumpScare(false);
    }

    #region Animations
    void _SetAnimMove()
    {
        // Calculate the velocity relative to this transform's orientation
        Vector3 relVelocity = transform.InverseTransformDirection(aIPath.velocity);
        relVelocity.y = 0;

        if (runToNearest )
        {
            animator.SetFloat("NormalizedSpeed", (relVelocity.magnitude / animator.transform.lossyScale.x) * 2f);
        }
        else
        {
            // Speed relative to the character size
            animator.SetFloat("NormalizedSpeed", relVelocity.magnitude / animator.transform.lossyScale.x);
        }
    }

    void _SetAnimAttack(bool attack)
    {
        animator.SetBool("Attack", attack);
    }

    void _SetAnimJumpScare(bool active)
    {
        animator.SetBool("JumpScare", active);
    }

    #endregion

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runToRange);

        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, attackRange);

        if (nearest != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nearest.position);
        }
    }
#endif
}
