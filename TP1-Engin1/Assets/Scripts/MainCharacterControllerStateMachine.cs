using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class MainCharacterControllerStateMachine : GenericStateMachine<CharacterState>, IDamageable
{
    public Camera Camera { get; private set; }

    //public CinemachineVirtualCamera VirtualCamera { get; private set; }

    [field: SerializeField]
    public Rigidbody RB { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; private set; }

    [field:SerializeField]
    public CharacterEffectsController EffectsController { get; private set; }

    [field: SerializeField]
    public float AccelerationValue { get; private set; }

    [field: SerializeField]
    public float InAirAccelerationValue { get; private set; } = 0.2f;
   
    [field: SerializeField]
    public float DecelerationValue { get; private set; } = 0.3f;

    [field: SerializeField]
    public float MaxForwardVelocity { get; private set; }

    [field: SerializeField]
    public float MaxSideVelocity { get; private set; }

    [field: SerializeField]
    public float MaxBackwardVelocity { get; private set; }

    private UnityEngine.Vector2 CurrentRelativeVelocity { get; set; }
    public UnityEngine.Vector2 CurrentDirectionalInputs { get; private set; }
    public bool OnHitStimuliReceived { get; set; } = false;
    public bool OnStunStimuliReceived { get; set; } = false;
    public bool InNonGameplayState { get; set; } = false;

   [field: SerializeField]
    public float JumpIntensity { get; private set; } = 1000.0f;

    [SerializeField]
    private GameObject m_ArmHitbox;
    [SerializeField]
    private CharacterFloorTrigger m_floorTrigger;
    [SerializeField]
    private CharacterRoodTrigger m_roodTrigger;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new AttackState());
        m_possibleStates.Add(new HitState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new OnGroundState());
        m_possibleStates.Add(new GetUpState());
        m_possibleStates.Add(new NonGameplayState());
    }

    protected override void Start()
    {
        Camera = Camera.main;
        //VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
        IsOnContactWithFloor();
        Animator.SetBool("OnHit", false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }
    
    public bool IsOnContactWithFloor()
    {
        Animator.SetBool("OnGround", true);
        return m_floorTrigger.IsOnFloor;
    }

    public bool IsInMidair()
    {
        Animator.SetBool("OnGround", false);
        return m_floorTrigger.IsOnFloor;
    }

    public bool IsOnContactWithRood()
    {
        if (m_roodTrigger.IsTouchingRood == true)
        { 
          Animator.SetBool("OnHit", true);
          return true;
        }
        Animator.SetBool("OnHit", false);
        return false;
    }

    //public void IsShakingCamera()
    //{
    //  VirtualCamera
    //}

    public void JumpTrigger()
    {
        Animator.SetTrigger("Jump");
    }

    public void AttackTrigger()
    {
        Animator.SetTrigger("Attack");
    }

    public void LandTrigger()
    {
        Animator.SetTrigger("Land");
    }

    public void CrashTrigger()
    {
        Animator.SetTrigger("Crash");
    }

    public void ReceiveDamage(EDamageType damageType)
    {
        if (damageType == EDamageType.Normal)
        {
            OnHitStimuliReceived = true;
        }
        if (damageType == EDamageType.Stunning)
        {
            OnStunStimuliReceived = true;
        }
    }

    public void UpdateAnimatorValues(UnityEngine.Vector2 moveVecValue)
    {
        // aller chercher la vitesse actuelle
        //Communiquer directement avec mon Animator
        //moveVecValue.Normalize();
        moveVecValue = new UnityEngine.Vector2(moveVecValue.x, moveVecValue.y);
        //Animator.SetFloat("MoveX", CurrentRelativeVelocity.x / GetCurrentMaxSpeed());
        //Animator.SetFloat("MoveY", CurrentRelativeVelocity.y / GetCurrentMaxSpeed());
        Animator.SetBool("OnGround", m_floorTrigger.IsOnFloor);

        if (moveVecValue == null)
        {
            Animator.SetFloat("MoveX", 0);
            Animator.SetFloat("MoveY", 0);
        }
    }

    public void OnEnabledHitBox( bool isEnabled = true)
    {
        Debug.Log("punch hitbox : "+ isEnabled);
        m_ArmHitbox?.SetActive(isEnabled);
    }
}