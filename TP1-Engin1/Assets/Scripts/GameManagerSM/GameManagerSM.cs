using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSM : GenericStateMachine<IState>
{
    [SerializeField]
    protected Camera m_gameplayCamera;
    [SerializeField]
    protected Camera m_cinematicCamera;

    public IState DesiredState { get; set; } = null;

    [field: SerializeField]
    public MainCharacterControllerStateMachine CharacterController { get; private set; }

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
        base.Start();
    }

    public void OnCinematicEnd()
    {
        DesiredState = m_possibleStates[0];
    }
    
    public bool CanTransitionOut()
    {
        return DesiredState != null;
    }
    

    //void ShakeOnDamage()
    //{

    //}
}
