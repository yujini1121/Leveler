using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public static Vector3 lastSavePosition; // ������ ���� ��ġ (static���� �����Ͽ� ��� �ν��Ͻ����� ����)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lastSavePosition = transform.position;
            Debug.Log("Save Point Activated! Position saved: " + lastSavePosition);

            // SavePoint ������Ʈ�� ��Ȱ��ȭ
            gameObject.SetActive(false);
            // �ʿ��ϴٸ� �ð����� �ǵ�� (��: ��ƼŬ ȿ��)�� �߰��� �� �ֽ��ϴ�.
        }
    }
}





