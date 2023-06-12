using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss��HPUI�̕\��������N���X
/// </summary>
public class BossUI : MonoBehaviour
{
    [SerializeField] GameObject _bossUI;
    // Start is called before the first frame update
    void Start()
    {
        _bossUI.SetActive(false);
    }

    public void ShowBossUI()
    {
        _bossUI.SetActive(true);
    }
}
