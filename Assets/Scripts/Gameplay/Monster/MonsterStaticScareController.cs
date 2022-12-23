using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MonsterStaticScareController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public Animator animator;

    public bool setDefault = false;

    public PlayerController selectedPlayer;
    public PlayerAIController selectedAIPlayer;

    [Header("Attack settings")]
    //Attack
    [SerializeField]
    float hitScareDelay = 0f;
    float curHitDelay = 0f;

    public bool attacking = false;

    public SoundEffectSO jumpScareSfx;

    private void OnEnable()
    {
        setDefault = true;
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

        _OutJumpScare();
    }

    public void _Default()
    {
        if (setDefault == false) return;

        setDefault = false;

        selectedPlayer = null;
        selectedAIPlayer = null;

        attacking = false;
        curHitDelay = -1f;

        _SetAnimJumpScare(false);
        _SetAnimAttack(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacking) return;

        PlayerController playerController = other.GetComponent<PlayerController>();
        PlayerAIController playerAIController = other.GetComponent<PlayerAIController>();

        //if (selectedPlayer == null && selectedAIPlayer == null)
        {
            if (playerController != null && playerController.isHiding == false)
            {
                selectedPlayer = playerController;

                selectedPlayer._SetCatched(true);

                attacking = true;

                curHitDelay = hitScareDelay;

                _SetAnimJumpScare(true);

                CameraManager.instance._GameplaySwitchCam(virtualCam);

                jumpScareSfx.Play(gameObject);

                return;
            }

            if(playerAIController != null && playerAIController.isHiding == false)
            {
                selectedAIPlayer = playerAIController;

                selectedAIPlayer._SetCatched(true);

                attacking = true;

                curHitDelay = hitScareDelay;

                _SetAnimJumpScare(false);

                return;
            }
        }
    }

    void _OutJumpScare()
    {
        if(attacking)
        {
            if(curHitDelay >= 0f)
            {
                curHitDelay -= Time.deltaTime;

                if(curHitDelay < 0f)
                {
                    attacking = false;

                    if (selectedPlayer != null)
                    {
                        selectedPlayer._SetCatched(false);
                    }

                    if (selectedAIPlayer != null)
                    {
                        selectedAIPlayer._SetCatched(false);
                    }

                    selectedPlayer = null;
                    selectedAIPlayer = null;

                    _SetAnimJumpScare(false);
                }
            }
        }
    }

    #region Animations

    void _SetAnimAttack(bool attack)
    {
        animator.SetBool("Attack", attack);
    }

    void _SetAnimJumpScare(bool active)
    {
        animator.SetBool("JumpScare", active);
    }

    #endregion
}
