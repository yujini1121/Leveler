using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public enum EnemyType { G, S, P, B }

    public EnemyType enemyType;

    [Header("Death Sounds")]
    public AudioClip Gdeath;
    public AudioClip Sdeath;
    public AudioClip Pdeath;
    public AudioClip Bdeath;

    [Header("Attack Sounds")]
    public AudioClip GSattack; // Goblin과 Skeleton이 같은 공격 소리를 쓰는 경우
    public AudioClip Pattack;
    public AudioClip Battack;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayDeathSound()
    {
        switch (enemyType)
        {
            case EnemyType.G:
                audioSource.PlayOneShot(Gdeath);
                break;
            case EnemyType.S:
                audioSource.PlayOneShot(Sdeath);
                break;
            case EnemyType.P:
                audioSource.PlayOneShot(Pdeath);
                break;
            case EnemyType.B:
                audioSource.PlayOneShot(Bdeath);
                break;
        }
    }

    public void PlayAttackSound()
    {
        switch (enemyType)
        {
            case EnemyType.G:
            case EnemyType.S:
                audioSource.PlayOneShot(GSattack);
                break;
            case EnemyType.P:
                audioSource.PlayOneShot(Pattack);
                break;
            case EnemyType.B:
                audioSource.PlayOneShot(Battack);
                break;
        }
    }
}
