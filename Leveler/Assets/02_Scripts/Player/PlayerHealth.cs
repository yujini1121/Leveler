using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력 설정")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthSlider;

    private bool isDead = false;

    private PlayerSoundManager soundManager;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        soundManager = GetComponent<PlayerSoundManager>();
        rb = GetComponent<Rigidbody2D>();
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        UpdateHealthUI();

        // 플레이어가 몬스터에게 맞았을 때
        GameBalancer.Instance?.RegisterMonsterHit(); // monsterHits 증가 → 난이도 증가

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        soundManager?.PlayDeath();
        Debug.Log("[PlayerHealth] 플레이어 사망");

        // 사망했을 때 (일반 사망 처리, 낙사 시 true)
        GameBalancer.Instance?.RegisterDeath(false);

        RespawnAtLastSavePoint();
    }

    private void RespawnAtLastSavePoint()
    {
        if (SavePoint.lastSavePosition != Vector3.zero)
        {
            transform.position = SavePoint.lastSavePosition;
            Debug.Log($"[PlayerHealth] SavePoint 위치로 리스폰: {SavePoint.lastSavePosition}");
        }
        else
        {
            Debug.LogWarning("[PlayerHealth] 저장 위치가 없습니다. 리스폰 불가.");
        }

        currentHealth = maxHealth;
        isDead = false;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;

            // 난이도 점수 텍스트도 같이 업데이트 (UIManager 없이 직접 접근 시)
            if (GameBalancer.Instance != null && GameBalancer.Instance.TryGetComponent(out TextMeshProUGUI diffText))
            {
                diffText.text = $"난이도: {GameBalancer.Instance.DifficultyScore:F2}";
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }
}
