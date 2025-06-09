using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DefaultEnemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatus
    {
        public float moveSpeed = 2f;
        public float timer = 0f;
    }

    [System.Serializable]
    public class IdleStateOption
    {
        public float idleTime;
    }

    [System.Serializable]
    public class AttackStateOption
    {
        public float attackCooldown = 1.5f;
        public float attackRange = 2f;
    }

    [System.Serializable]
    public class ChaseStateOption
    {
        public float chaseRange = 5f;
    }

    [System.Serializable]
    public class PatrolStateOption
    {
        public float patrolRange = 3f;
        [HideInInspector] public Vector3 leftPoint;
        [HideInInspector] public Vector3 rightPoint;
        public bool movingRight = true;
    }

    private FSM<DefaultEnemy, StateType> _fsm;

    public Rigidbody2D rb;
    public Transform player;

    public EnemyStatus enemyStatus;
    public IdleStateOption idleOption;
    public AttackStateOption attackOption;
    public ChaseStateOption chaseOption;
    public PatrolStateOption patrolOption;

    [Space(10)] public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        initialPosition.y = 2f;
        patrolOption.leftPoint = initialPosition - Vector3.right * patrolOption.patrolRange;
        patrolOption.rightPoint = initialPosition + Vector3.right * patrolOption.patrolRange;
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
        Vector3 left = initialPosition - Vector3.right * patrolOption.patrolRange;
        Vector3 right = initialPosition + Vector3.right * patrolOption.patrolRange;
        Handles.DrawLine(left, right);
        Handles.DrawSolidDisc(left, Vector3.forward, 0.1f);
        Handles.DrawSolidDisc(right, Vector3.forward, 0.1f);

        // Chase Range
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, chaseOption.chaseRange);

        // Attack Range
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackOption.attackRange);
    }
    #endregion
}