using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MainCharacterControllerStateMachine : MonoBehaviour
{
    public Camera Camera { get; private set; }

    [field: SerializeField]
    public Rigidbody RB { get; private set; }

    [field: SerializeField]
    private Animator Animator { get; set; }
    
    [field: SerializeField]
    public float AccelarationValue { get; private set; }

    [field: SerializeField]
    public float MaxForwardVelocity { get; private set; }

    [field: SerializeField]
    public float MaxSideVelocity { get; private set; }

    [field: SerializeField]
    public float MaxBackwardVelocity { get; private set; }

    [field: SerializeField]
    public float JumpIntensity { get; private set; } = 100.0f;

    [SerializeField]
    private CharacterFloorTrigger m_floorTrigger;
    private CharacterState m_currentState;
    private List<CharacterState> m_possibleStates;

    private void Awake()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        //m_possibleStates.Add(new AttackState());
        //m_possibleStates.Add(new HitState());
        //m_possibleStates.Add(new OnGroundState());
        //m_possibleStates.Add(new GetUpState());
    }

    void Start()
    {
        Camera = Camera.main;

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
    }
    
    void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    private void Update()
    {
        m_currentState.OnUpdate();
        TryToTransitionState();
    }
   
    private void TryToTransitionState()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        // Je PEUX quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState == state)
            {
                continue;
            }

            if (state.CanEnter())
            {
                //Quitter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer dans le state state
                m_currentState.OnEnter();
                return;
            }
        }
    }
   
    public bool IsOnContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    } 

    public void UpdateAnimatorValues(UnityEngine.Vector2 moveVecValue)
    {
        // aller chercher la vitesse actuelle
        //Communiquer directement avec mon Animator
        //moveVecValue.Normalize();
        moveVecValue = new UnityEngine.Vector2(moveVecValue.x, moveVecValue.y / MaxForwardVelocity);
        Animator.SetFloat("moveX", moveVecValue.x);
        Animator.SetFloat("moveY", moveVecValue.y);
    }
}