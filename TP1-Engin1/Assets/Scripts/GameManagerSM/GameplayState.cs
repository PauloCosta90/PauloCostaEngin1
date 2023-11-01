using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IState
{
    //[SerializeField]
    protected Camera m_camera;
    //[SerializeField]
    //protected GameManagerSM m_manager;

    public GameplayState(Camera camera)
    {
        m_camera = camera;
        //m_manager = manager;
    }

    public void OnStart()
    {
       
    }

    public virtual void OnEnter()
    {
        m_camera.enabled = true;
        Debug.Log("On enter gameplay");
    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {
        //CinemachineCore.Instance.GetActiveBrain(1).ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
        m_camera.enabled = false;
       //m_manager.DesiredState = null;
        //m_manager.CharacterController.On
        Debug.Log("On exit gameplay");
    }

    public virtual bool CanEnter(IState currentState)
    {
        return Input.GetKey(KeyCode.G);
    }

    public virtual bool CanExit()
    {
        return Input.GetKey(KeyCode.G);
    }
}
