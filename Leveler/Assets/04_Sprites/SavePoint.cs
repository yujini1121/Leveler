using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    public static Vector3 lastSavePointPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastSavePointPosition = transform.position;
            Debug.Log("Save Point Ȱ��ȭ: " + lastSavePointPosition);
            // �߰������� SavePoint�� Ȱ��ȭ�Ǿ����� �ð������� ǥ���� ���� �ֽ��ϴ�.
        }
    }

    // ���� ���� �� �ʱ� SavePoint�� �����ϰ� �ʹٸ� �� �Լ��� ȣ���� �� �ֽ��ϴ�.
    public static void SetInitialSavePoint(Vector3 position)
    {
        lastSavePointPosition = position;
        Debug.Log("�ʱ� Save Point ����: " + lastSavePointPosition);
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
