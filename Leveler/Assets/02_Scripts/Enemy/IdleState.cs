using UnityEngine;

public class IdleState : IEnemyState
{
    private EnemyBase enemy;
    private float idleTime = 2f;
    private float timer = 0f;

    public IdleState(EnemyBase enemy) => this.enemy = enemy;

    public void Enter() => timer = 0f;

    public void Update()
    {
        timer += Time.deltaTime;
        if (enemy.GetDistanceToPlayer() < enemy.chaseRange)
            enemy.SwitchState(EnemyStateType.Chase);
        else if (timer >= idleTime)
            enemy.SwitchState(EnemyStateType.Patrol);
    }

    public void Exit() { }
}
