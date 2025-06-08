using UnityEngine;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;

public enum EnemyStateType { Idle, Patrol, Chase, Attack }

public class EnemyBase : MonoBehaviour
{
    [ContextMenu("Force To Chase")]
    public void ForceChase() => SwitchState(EnemyStateType.Chase);


    public EnemyStateType currentStateType;
    private IEnemyState currentState;

    public Transform[] patrolPoints;
    public Transform player;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    public Rigidbody2D rb;

    private Dictionary<EnemyStateType, IEnemyState> stateMap;

    void Start()
    {
        stateMap = new Dictionary<EnemyStateType, IEnemyState>()
        {
            { EnemyStateType.Idle, new IdleState(this) },
            { EnemyStateType.Patrol, new PatrolState(this) },
            { EnemyStateType.Chase, new ChaseState(this) },
            { EnemyStateType.Attack, new AttackState(this) }
        };

        SwitchState(EnemyStateType.Patrol);
    }

    void Update()
    {
        currentState?.Update();
    }

    public void SwitchState(EnemyStateType newState)
    {
        currentState?.Exit();
        currentState = stateMap[newState];
        currentStateType = newState;
        currentState?.Enter();
    }

    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position); 
    }

    public void DebugForceState(EnemyStateType newState)
    {
        SwitchState(newState);
    }
}
