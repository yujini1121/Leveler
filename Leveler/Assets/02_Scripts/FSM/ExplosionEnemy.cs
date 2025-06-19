using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemy : DefaultEnemy
{
    [Header("자폭 설정")]
    public float explosionRadius = 3f;         // 폭발 범위
    public int explosionDamage = 30;           // 폭발 데미지
    public GameObject explosionEffectPrefab;   // 폭발 이펙트 프리팹 (선택사항)

    public override void AttackAction()
    {
        Debug.Log("[ExplosionEnemy] 자폭 실행!");

        // 1. 범위 내 플레이어 탐색
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Player"));
        foreach (var hit in hits)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(explosionDamage);
                Debug.Log($"[ExplosionEnemy] 플레이어에게 {explosionDamage} 데미지");
            }
        }

        // 2. 이펙트 재생 (선택)
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 3. 자폭 파괴
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}
