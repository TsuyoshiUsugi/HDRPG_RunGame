using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���s��(���S�����Ƃ��ɕ\������UI)
/// </summary>
public class FailedUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _showUIs;

    // Start is called before the first frame update
    void Start()
    {
        if (_showUIs.Count == 0 || _showUIs == null) return;
        _showUIs.ForEach(obj => obj.SetActive(false));
    }

    public void ShowFailedUI()
    {
        _showUIs.ForEach(obj => obj.SetActive(true));
    }
}
