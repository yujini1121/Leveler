using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyBase enemy;
    private int currentPoint = 0;

    public PatrolState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter() { }

    public void Update()
    {
        if (enemy.patrolPoints.Length == 0) return;

        Vector2 currentPos = enemy.transform.position;
        Vector2 targetPos = enemy.patrolPoints[currentPoint].position;

        enemy.transform.position = Vector2.MoveTowards(currentPos, targetPos, enemy.moveSpeed * Time.deltaTime);

        if (Vector2.Distance(currentPos, targetPos) < 0.1f)
            currentPoint = (currentPoint + 1) % enemy.patrolPoints.Length;

        if (enemy.GetDistanceToPlayer() < enemy.chaseRange)
            enemy.SwitchState(EnemyStateType.Chase);
    }

    public void Exit() { }
}
