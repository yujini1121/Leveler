using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatus
    {
        //Status
        public float moveSpeed = 2f;
        public float timer = 0f;

        //Idle
        public float idleTime;

        //Attack
        public float attackCooldown = 1.5f;
        public float attackRange = 2f;

        //Chase
        public float chaseRange = 5f;

        //Patrol
        public Vector3 leftPoint;
        public Vector3 rightPoint;
        public float waitDuration = 1f;
        public int moveDir = 0;
    }

    [System.Serializable]
    public class IdleStateOption
    {

    }
    
    private FSM<DefaultEnemy, StateType> _fsm;

    public Rigidbody2D rb;
    public Transform player;
    public EnemyStatus enemyStatus;

    [Space(10)] public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        initialPosition.y = 2f;

        _fsm = new FSM<DefaultEnemy, StateType>();

        var stateDict = new Dictionary<StateType, BaseState<DefaultEnemy>>
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
        Vector3 left = initialPosition + enemyStatus.leftPoint;
        Vector3 right = initialPosition + enemyStatus.rightPoint;
        Handles.DrawLine(left, right);
        Handles.DrawSolidDisc(left, Vector3.forward, 0.1f);
        Handles.DrawSolidDisc(right, Vector3.forward, 0.1f);

        // Chase Range
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, enemyStatus.chaseRange);

        // Attack Range
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, enemyStatus.attackRange);
    }
    #endregion
}