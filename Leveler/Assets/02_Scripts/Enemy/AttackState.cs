using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyBase enemy;
    private float attackCooldown = 1.5f;
    private float timer = 0f;

    public AttackState(EnemyBase enemy) => this.enemy = enemy;

    public void Enter() => timer = 0f;

    public void Update()
    {
        timer += Time.deltaTime;

        if (enemy.GetDistanceToPlayer() > enemy.attackRange)
            enemy.SwitchState(EnemyStateType.Chase);

        if (timer >= attackCooldown)
        {
            Debug.Log("Attack!");
            timer = 0f;
        }
    }

    public void Exit() { }
}

