using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsController : MonoBehaviour
{
    [SerializeField]
    private List<SpecialEffectsSystem> SpecialEffectsSystem = new List<SpecialEffectsSystem>();

    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_jumpAudioClip;
    [SerializeField]
    private AudioClip m_landingAudioClip;
    [SerializeField]
    private AudioClip m_attackAudioClip;

    [SerializeField]
    private ParticleSystem m_attackParticle;
    [SerializeField]
    private ParticleSystem m_damageParticle;

    public void PlaySVFXSystem(EActionType actionType)
    {
        switch (actionType)
        {
            case EActionType.Attack:
                m_attackParticle.Play();
                break;

            case EActionType.Hit:
                m_attackParticle.Play();
                break;

            case EActionType.Count:
                break;
        }
    }

    public void PlaySound(EActionType actionType)
    {
        switch(actionType)
        {
            case EActionType.Jump:
                m_audioSource.clip = m_jumpAudioClip;
                break;

            case EActionType.Landing:
                m_audioSource.clip = m_landingAudioClip;
                break;

            case EActionType.Attack:
                m_audioSource.clip = m_attackAudioClip;
                break;

            case EActionType.Hit:
                m_audioSource.clip = m_attackAudioClip;
                break;

            case EActionType.Count:
                break;
        }
        m_audioSource.Play();
    }
}

public enum EActionType
{
    Landing,
    Jump,
    Attack,
    Hit,
    Count
}

[System.Serializable]
public struct SpecialEffectsSystem
{
    public EActionType actionType;
    public List<AudioClip> clipList;
    public List<ParticleSystem> particleList;
}