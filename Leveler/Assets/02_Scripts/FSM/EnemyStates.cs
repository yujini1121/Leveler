using UnityEngine;

namespace EnemyState
{
    public class IdleState : BaseState<DefaultEnemy>
    {
        public IdleState(DefaultEnemy target, FSM<DefaultEnemy, StateType> fsm) : base(target, fsm) {}

        public override void Enter()
        {

        }

        public override void Excute()
        {
            if (_target.GetDistanceToPlayer() < _target.enemyStatus.chaseRange)
                _fsm.ChangeState(StateType.Chase);
            else //if (timer >= idleTime)
                _fsm.ChangeState(StateType.Patrol);
        }

        public override void Exit()
        {

        }
    }

    public class AttackState : BaseState<DefaultEnemy>
    {
        public AttackState(DefaultEnemy target, FSM<DefaultEnemy, StateType> fsm) : base(target, fsm) { }

        public override void Enter()
        {

        }

        public override void Excute()
        {

        }

        public override void Exit()
        {

        }
    }

    public class PatrolState : BaseState<DefaultEnemy>
    {
        public PatrolState(DefaultEnemy target, FSM<DefaultEnemy, StateType> fsm) : base(target, fsm) { }

        public override void Enter()
        {

        }

        public override void Excute()
        {

        }

        public override void Exit()
        {

        }
    }

    public class ChaseState : BaseState<DefaultEnemy>
    {
        public ChaseState(DefaultEnemy target, FSM<DefaultEnemy, StateType> fsm) : base(target, fsm) { }

        public override void Enter()
        {

        }

        public override void Excute()
        {

        }

        public override void Exit()
        {

        }
    }
}