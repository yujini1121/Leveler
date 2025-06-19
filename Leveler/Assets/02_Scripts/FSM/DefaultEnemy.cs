using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
        public int damage = 10;
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

    protected FSM<DefaultEnemy, StateType> _fsm;

    public Rigidbody2D rb;
    public Transform player;

    public EnemyStatus enemyStatus;
    public IdleStateOption idleOption;
    public AttackStateOption attackOption;
    public ChaseStateOption chaseOption;
    public PatrolStateOption patrolOption;

    [Space(10)] public Vector3 initialPosition;
    [SerializeField] public StateType currentState;

    // ★ Animator 연결 필드 추가
    public Animator animator;

    protected void Start()
    {
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

    protected void Update()
    {
        _fsm.Update();
    }

    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }

    public virtual void AttackAction()
    {
        Debug.Log("기본 공격 실행됨!");
    }

    protected void PerformMeleeAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackOption.attackRange, LayerMask.GetMask("Player"));
        if (hit != null)
        {
            PlayerHealth target = hit.GetComponent<PlayerHealth>();
            if (target != null)
            {
                target.TakeDamage(attackOption.damage);
                Debug.Log($"[Enemy] 공격 성공! 플레이어에게 {attackOption.damage} 데미지");
            }
        }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        if (player == null) return;

        Handles.color = Color.yellow;
        Vector3 left = initialPosition - Vector3.right * patrolOption.patrolRange;
        Vector3 right = initialPosition + Vector3.right * patrolOption.patrolRange;
        Handles.DrawLine(left, right);
        Handles.DrawSolidDisc(left, Vector3.forward, 0.1f);
        Handles.DrawSolidDisc(right, Vector3.forward, 0.1f);

        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, chaseOption.chaseRange);

        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackOption.attackRange);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackOption.attackRange);
    }
#endif
}
