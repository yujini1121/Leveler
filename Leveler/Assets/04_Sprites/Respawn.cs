using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public Transform playerTransform; // ��������ų �÷��̾��� Transform

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (playerTransform != null && SavePoint.lastSavePointPosition != Vector3.zero)
        {
            playerTransform.position = SavePoint.lastSavePointPosition;
            Debug.Log("�÷��̾� ������: " + SavePoint.lastSavePointPosition);
            // �߰������� ������ ���� ȿ���� �� ���� �ֽ��ϴ�.
        }
        else
        {
            Debug.LogWarning("�÷��̾� Transform�� �������� �ʾҰų� ����� Save Point�� �����ϴ�.");
            // �⺻ ������ ��ġ�� �����ϰų� �ٸ� ó���� �� �� �ֽ��ϴ�.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
