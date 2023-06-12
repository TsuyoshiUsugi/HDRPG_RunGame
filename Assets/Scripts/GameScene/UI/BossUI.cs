using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Boss‚ÌHPUI‚Ì•\Ž¦‚ð‚·‚éƒNƒ‰ƒX
/// </summary>
public class BossUI : MonoBehaviour
{
    [SerializeField] GameObject _bossUI;
    [SerializeField] Text _bossName;
    // Start is called before the first frame update
    void Start()
    {
        _bossName.text = $"Boss:{GameSceneManager.Instance.BossName}";
        _bossUI.SetActive(false);
    }

    public void ShowBossUI()
    {
        _bossUI.SetActive(true);
    }
}
