using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�e�[�W�J�n����UI�������s���X�N���v�g
/// </summary>
public class StartUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _showUIs;
    [SerializeField] int _performanceInterval = 1000;
    [SerializeField] Text _stageName;

    CancellationToken _ct;
    public event Action OnEndShowStartUI;

    private void Awake()
    {
        _ct = this.GetCancellationTokenOnDestroy();

        if (WorldDataLoader.Instance.LoadedWorldDatas.Count != 0)
        {
            _stageName.text =
                $"�X�e�[�W�F{WorldDataLoader.Instance.LoadedWorldDatas[WorldDataLoader.Instance.CurrentStageNum].StageName}";
        }

        _showUIs.ForEach(obj => obj.SetActive(false));
    }

    /// <summary>
    /// �X�^�[�g���ɕ\������UI���w�肵���Ԋu�ŕ\��
    /// </summary>
    /// <returns></returns>
    public async UniTask ShowStartUI()
    {
        await SceneLoadManager.Instance.OnStartScene();

        if (_showUIs.Count == 0 || _showUIs == null) return;
        
        
        foreach (GameObject go in _showUIs)
        {
            if (go == null) return;

            go.SetActive(true);
            await UniTask.Delay(_performanceInterval, cancellationToken:_ct);
            go.SetActive(false);
        }

        if (OnEndShowStartUI != null) OnEndShowStartUI();
    }
}
