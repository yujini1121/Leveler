using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyBase enemy;

    public ChaseState(EnemyBase enemy) => this.enemy = enemy;

    public void Enter() { }

    public void Update()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.player.position, enemy.moveSpeed * Time.deltaTime);

        float dist = enemy.GetDistanceToPlayer();
        if (dist < enemy.attackRange + 1f)
            enemy.SwitchState(EnemyStateType.Attack);
        else if (dist > enemy.chaseRange)
            enemy.SwitchState(EnemyStateType.Patrol);
    }

    public void Exit() { }
}
