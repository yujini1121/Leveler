using UnityEngine;

public class SkeletonEnemy : DefaultEnemy
{
    public override void AttackAction()
    {
        Debug.Log("«ÿ∞Ò¿Ã ƒÆ¿ª »÷µŒ∏•¥Ÿ!");
        PerformMeleeAttack();
    }
}
