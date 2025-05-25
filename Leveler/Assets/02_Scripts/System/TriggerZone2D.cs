using UnityEngine;

public class TriggerZone2D : MonoBehaviour
{
    public enum TriggerType { AddDeath, AddFallDeath, AddHit }
    public TriggerType triggerType;

    [SerializeField] private GameBalancer gameBalancer;

    private void Start()
    {
        Debug.Log("하이");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        switch (triggerType)
        {
            case TriggerType.AddDeath:
                gameBalancer.RegisterDeath(false);
                break;
            case TriggerType.AddFallDeath:
                gameBalancer.RegisterDeath(true);
                break;
            case TriggerType.AddHit:
                gameBalancer.RegisterMonsterHit();
                break;
        }

        Debug.Log($"[TriggerZone2D] {triggerType} 트리거 발동됨");
    }
}
