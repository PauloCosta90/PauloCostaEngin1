using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private bool receiveHit;
    private bool giveHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OntriggerEnter()
    {

    }
}

public enum ECharacterAllience
{
    Evil,
    Good,
    Neutral,
    Count
}