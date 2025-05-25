using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Following,
        Charging,
        Attack,
        Cooldown
    }
    //State curState = State.Roaming;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Roaming()
    {
        // 배회 로직

    }
}
