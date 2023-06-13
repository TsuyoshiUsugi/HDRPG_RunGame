using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̍U���G�t�F�N�g��\������N���X
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        TryGetComponent(out _animator);
    }

    /// <summary>
    /// �����Ɏw�肵�����O�̃A�j���[�^�[��bool��true�ɂ���
    /// </summary>
    /// <param name="effectName"></param>
    public async void ShowEffect(string effectName)
    {
        _animator.SetBool(effectName, true);
        await UniTask.Delay(1);
        _animator.SetBool(effectName, false);
    }
}
