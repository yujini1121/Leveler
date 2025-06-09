using UnityEngine;

namespace EnemyState
{
    public class IdleState : BaseState<TestMonster>
    {
        public IdleState(TestMonster target, FSM<TestMonster, StateType> fsm) : base(target, fsm) {}

        public override void Enter()
        {

        }

        public override void Excute()
        {
            if (_target.GetDistanceToPlayer() < _target.chaseRange)
                _fsm.ChangeState(StateType.Chase);
            else //if (timer >= idleTime)
                _fsm.ChangeState(StateType.Patrol);
        }

        public override void Exit()
        {

        }
    }

    public class AttackState : BaseState<TestMonster>
    {
        public AttackState(TestMonster target, FSM<TestMonster, StateType> fsm) : base(target, fsm) { }

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

    public class PatrolState : BaseState<TestMonster>
    {
        public PatrolState(TestMonster target, FSM<TestMonster, StateType> fsm) : base(target, fsm) { }

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

    public class ChaseState : BaseState<TestMonster>
    {
        public ChaseState(TestMonster target, FSM<TestMonster, StateType> fsm) : base(target, fsm) { }

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