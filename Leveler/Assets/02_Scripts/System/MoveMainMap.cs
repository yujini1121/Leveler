using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainMap : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint; // StartPoint ������Ʈ�� Transform�� ������ ����

    [SerializeField]
    private string goalTag = "Goal"; // Goal ������Ʈ�� �±� (Inspector���� ���� ����)

    // �ٸ� Collider2D�� �浹���� �� ȣ��Ǵ� �Լ� (Is Trigger�� üũ�� ���)
    private void OnTriggerEnter2D(Collider2D other) // 2D������ ����
    {
        // �浹�� ������Ʈ�� �±װ� GoalTag�� ��ġ�ϴ��� Ȯ��
        if (other.CompareTag(goalTag))
        {
            // StartPoint�� �Ҵ�Ǿ� �ִ��� Ȯ��
            if (startPoint != null)
            {
                // ���� ������Ʈ(�÷��̾�)�� ��ġ�� StartPoint�� ��ġ�� �̵�
                transform.position = startPoint.position;
                Debug.Log("Goal�� ��ҽ��ϴ�! StartPoint�� �̵��մϴ�.");
            }
            else
            {
                Debug.LogWarning("StartPoint�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� StartPoint ������Ʈ�� �Ҵ����ּ���.");
            }
        }
    }

    // ����� ������ StartPoint�� ����� �Ҵ�Ǿ����� Ȯ��
    private void OnValidate()
    {
        if (startPoint == null)
        {
            Debug.LogWarning("StartPoint�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� StartPoint ������Ʈ�� �Ҵ����ּ���.");
        }
    }
}

