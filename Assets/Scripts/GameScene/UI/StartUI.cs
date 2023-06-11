using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�e�[�W�J�n����UI�������s���X�N���v�g
/// </summary>
public class StartUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _showUIs;
    [SerializeField] float _performanceInterval = 1.0f;
    [SerializeField] Text _stageName;

    public event Action OnEndShowStartUI;

    private void Awake()
    {
        if (WorldDataLoader.Instance.LoadedWorldDatas.Count != 0)
        {
            _stageName.text =
                $"�X�e�[�W�F{WorldDataLoader.Instance.LoadedWorldDatas[WorldDataLoader.Instance.CurrentStageNum].StageName}";
        }

        if (_showUIs.Count == 0 || _showUIs == null)
        {
            StartCoroutine(nameof(ShowStartUI));
            return;
        }
        
        _showUIs.ForEach(obj => obj.SetActive(false));

        StartCoroutine(nameof(ShowStartUI));
    }

    /// <summary>
    /// �X�^�[�g���ɕ\������UI���w�肵���Ԋu�ŕ\��
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowStartUI()
    {
        if (_showUIs.Count == 0 || _showUIs == null) yield break;
        
        foreach (GameObject go in _showUIs)
        {
            if (go == null) yield break;

            go.SetActive(true);
            yield return new WaitForSeconds(_performanceInterval);
            go.SetActive(false);
        }

        if (OnEndShowStartUI != null) OnEndShowStartUI();
    }
}
