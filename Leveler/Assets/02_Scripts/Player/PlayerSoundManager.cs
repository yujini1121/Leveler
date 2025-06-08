using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource effectSource;     // ����, ����, ��� �� OneShot��
    public AudioSource walkSource;       // �ȱ� ���� Loop �����

    [Header("Sound Clips")]
    public AudioClip jumpClip;
    public AudioClip attack1Clip;
    public AudioClip attack2Clip;
    public AudioClip walkClip;
    public AudioClip defenseClip;

    public void PlayJump()
    {
        PlayEffect(jumpClip);
    }

    public void PlayAttack1()
    {
        PlayEffect(attack1Clip);
    }

    public void PlayAttack2()
    {
        PlayEffect(attack2Clip);
    }

    public void PlayDefense()
    {
        PlayEffect(defenseClip);
    }

    //�ȱ� ��� (�ݺ�)
    public void PlayWalk()
    {
        if (!walkSource.isPlaying)
        {
            walkSource.clip = walkClip;
            walkSource.loop = true;
            walkSource.Play();
        }
    }

    public void StopWalk()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }

    //ȿ���� ���
    private void PlayEffect(AudioClip clip)
    {
        if (clip != null && effectSource != null)
        {
            effectSource.PlayOneShot(clip);
        }
    }
}
