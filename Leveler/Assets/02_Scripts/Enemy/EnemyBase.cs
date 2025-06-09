using UnityEngine;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public enum EnemyStateType { Idle, Patrol, Chase, Attack }

public class EnemyBase : MonoBehaviour
{
    [ContextMenu("Force To Chase")]
    public void ForceChase() => SwitchState(EnemyStateType.Chase);


    public EnemyStateType currentStateType;
    private IEnemyState currentState;

    public Rigidbody2D rb;

    public Transform player;
    public float patrolRange = 3f;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    public float idleTime;

    public Vector3 initialPosition;
    private Dictionary<EnemyStateType, IEnemyState> stateMap;

    void Start()
    {
        initialPosition = transform.position;
        initialPosition.y = 2f;

        stateMap = new Dictionary<EnemyStateType, IEnemyState>()
        {
            { EnemyStateType.Idle, new IdleState(this) },
            { EnemyStateType.Patrol, new PatrolState(this) },
            { EnemyStateType.Chase, new ChaseState(this) },
            { EnemyStateType.Attack, new AttackState(this) }
        };

        SwitchState(EnemyStateType.Idle);
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

    #region Debug
    public void DebugForceState(EnemyStateType newState)
    {
        SwitchState(newState);
        Debug.Log($"[Debug] 강제로 상태 전환됨 → {newState}");
    }

    [ContextMenu("Force To Idle")]
    public void Debug_ForceIdle() => SwitchState(EnemyStateType.Idle);

    [ContextMenu("Force To Patrol")]
    public void Debug_ForcePatrol() => SwitchState(EnemyStateType.Patrol);

    [ContextMenu("Force To Chase")]
    public void Debug_ForceChase() => SwitchState(EnemyStateType.Chase);

    [ContextMenu("Force To Attack")]
    public void Debug_ForceAttack() => SwitchState(EnemyStateType.Attack);
    #endregion
    #region Draw Scene View Only
    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Patrol Range
        Handles.color = Color.yellow;
        Vector3 left = initialPosition - Vector3.right * patrolRange;
        Vector3 right = initialPosition + Vector3.right * patrolRange;
        Handles.DrawLine(left, right);
        Handles.DrawSolidDisc(left, Vector3.forward, 0.1f);
        Handles.DrawSolidDisc(right, Vector3.forward, 0.1f);

        // Chase Range
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, chaseRange);

        // Attack Range
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackRange);
    }
    #endregion
}
