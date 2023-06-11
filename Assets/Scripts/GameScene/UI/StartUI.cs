using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ開始時のUI処理を行うスクリプト
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
                $"ステージ：{WorldDataLoader.Instance.LoadedWorldDatas[WorldDataLoader.Instance.CurrentStageNum].StageName}";
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
    /// スタート時に表示するUIを指定した間隔で表示
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
