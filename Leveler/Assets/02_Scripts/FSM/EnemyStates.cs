using UnityEngine;

namespace EnemyState
{
    public class IdleState : BaseState<DefaultEnemy>
    {
        public IdleState(DefaultEnemy context, FSM<DefaultEnemy, StateType> fsm) : base(context, fsm) { }

        public override void Enter()
        {
            _context.currentState = StateType.Idle;
            _context.enemyStatus.timer = 0f;

            if (_context.animator != null)
                _context.animator.SetBool("isWalking", false);
        }

        public override void Excute()
        {
            if (_context.GetDistanceToPlayer() < _context.chaseOption.chaseRange)
                _fsm.ChangeState(StateType.Chase);
            else if (_context.enemyStatus.timer >= _context.idleOption.idleTime)
                _fsm.ChangeState(StateType.Patrol);

            _context.enemyStatus.timer += Time.deltaTime;
        }

        public override void Exit() { }
    }

    public class PatrolState : BaseState<DefaultEnemy>
    {
        public PatrolState(DefaultEnemy context, FSM<DefaultEnemy, StateType> fsm) : base(context, fsm) { }

        public override void Enter()
        {
            _context.currentState = StateType.Patrol;

            if (_context.animator != null)
                _context.animator.SetBool("isWalking", true);
        }

        public override void Excute()
        {
            if (_context.GetDistanceToPlayer() < _context.chaseOption.chaseRange)
            {
                _fsm.ChangeState(StateType.Chase);
                return;
            }

            Vector2 currentPos = _context.transform.position;
            Vector2 targetPos = _context.patrolOption.movingRight ? _context.patrolOption.rightPoint : _context.patrolOption.leftPoint;

            _context.transform.position = Vector2.MoveTowards(currentPos, targetPos, _context.enemyStatus.moveSpeed * Time.deltaTime);

            if (Vector2.Distance(currentPos, targetPos) < 0.1f)
            {
                _context.patrolOption.movingRight = !_context.patrolOption.movingRight;
                _fsm.ChangeState(StateType.Idle);
            }
        }

        public override void Exit() { }
    }

    public class ChaseState : BaseState<DefaultEnemy>
    {
        public ChaseState(DefaultEnemy context, FSM<DefaultEnemy, StateType> fsm) : base(context, fsm) { }

        public override void Enter()
        {
            _context.currentState = StateType.Chase;

            if (_context.animator != null)
                _context.animator.SetBool("isWalking", true);
        }

        public override void Excute()
        {
            _context.transform.position = Vector3.MoveTowards(
                _context.transform.position,
                _context.player.position,
                _context.enemyStatus.moveSpeed * Time.deltaTime
            );

            float dist = _context.GetDistanceToPlayer();
            if (dist < _context.attackOption.attackRange + 1f)
                _fsm.ChangeState(StateType.Attack);
            else if (dist > _context.chaseOption.chaseRange)
                _fsm.ChangeState(StateType.Patrol);
        }

        public override void Exit() { }
    }

    public class AttackState : BaseState<DefaultEnemy>
    {
        public AttackState(DefaultEnemy context, FSM<DefaultEnemy, StateType> fsm) : base(context, fsm) { }

        public override void Enter()
        {
            _context.currentState = StateType.Attack;
            _context.enemyStatus.timer = 0f;

            if (_context.animator != null)
                _context.animator.SetTrigger("triggerAttack");
        }

        public override void Excute()
        {
            if (_context.GetDistanceToPlayer() > _context.attackOption.attackRange)
            {
                _fsm.ChangeState(StateType.Chase);
                return;
            }

            if (_context.enemyStatus.timer >= _context.attackOption.attackCooldown)
            {
                _context.AttackAction();
                _context.enemyStatus.timer = 0f;
            }

            _context.enemyStatus.timer += Time.deltaTime;
        }

        public override void Exit() { }
    }
}
