using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    private FSM<TestMonster, StateType> _fsm;

    public Rigidbody2D rb;

    public Transform player;
    public float patrolRange = 3f;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        initialPosition.y = 2f;

        _fsm = new FSM<TestMonster, StateType>();

        var stateDict = new Dictionary<StateType, BaseState<TestMonster>>
        {
            { StateType.Idle, new EnemyState.IdleState(this, _fsm) },
            { StateType.Attack, new EnemyState.AttackState(this, _fsm) },
            { StateType.Chase, new EnemyState.ChaseState(this, _fsm) },
            { StateType.Patrol, new EnemyState.PatrolState(this, _fsm) }
        };

        _fsm.SetStates(stateDict);
        _fsm.ChangeState(StateType.Idle);
    }

    private void Update()
    {
        _fsm.Update();
    }

    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }

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