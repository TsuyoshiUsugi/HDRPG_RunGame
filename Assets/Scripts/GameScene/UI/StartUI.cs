using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�J�n���́u��[���A�X�^�[�g�I�v�̏������s���X�N���v�g
/// </summary>
public class StartUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _showUI;
    [SerializeField] float _performanceInterval = 1.0f;

    private void Awake()
    {
        if (_showUI.Count == 0 || _showUI == null) return;
        _showUI.ForEach(obj => obj.SetActive(false));

        StartCoroutine(nameof(ShowStartUI));
    }

    /// <summary>
    /// �X�^�[�g���ɕ\������UI���w�肵���Ԋu�ŕ\��
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowStartUI()
    {
        if (_showUI.Count == 0 || _showUI == null) yield break;
        
        foreach (GameObject go in _showUI)
        {
            if (go == null) yield break;

            go.SetActive(true);
            yield return new WaitForSeconds(_performanceInterval);
            go.SetActive(false);
        }
    }
}
