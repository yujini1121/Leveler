using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyBase enemy;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    private bool movingRight = true;

    private enum PatrolSubState { Waiting, Moving }
    private PatrolSubState subState = PatrolSubState.Moving;

    private float waitTimer = 0f;
    private float waitDuration = 1f;

    public PatrolState(EnemyBase enemy)
    {
        this.enemy = enemy;
        float r = enemy.patrolRange;
        Vector2 origin = enemy.initialPosition;
        leftPoint = origin - Vector2.right * r;
        rightPoint = origin + Vector2.right * r;
    }

    public void Enter()
    {
        // Chase → Patrol 전환 시 1초 멈춤
        subState = PatrolSubState.Waiting;
        waitTimer = 0f;
    }

    public void Update()
    {
        // 공통 플레이어 감지
        if (enemy.GetDistanceToPlayer() < enemy.chaseRange)
        {
            enemy.SwitchState(EnemyStateType.Chase);
            return;
        }

        switch (subState)
        {
            case PatrolSubState.Waiting:
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitDuration)
                {
                    waitTimer = 0f;
                    subState = PatrolSubState.Moving;
                }
                return;

            case PatrolSubState.Moving:
                Vector2 currentPos = enemy.transform.position;
                Vector2 target = movingRight ? rightPoint : leftPoint;

                enemy.transform.position = Vector2.MoveTowards(currentPos, target, enemy.moveSpeed * Time.deltaTime);

                if (Vector2.Distance(currentPos, target) < 0.1f)
                {
                    movingRight = !movingRight;
                    subState = PatrolSubState.Waiting;
                    waitTimer = 0f;
                }
                break;
        }
    }

    public void Exit() { }
}
