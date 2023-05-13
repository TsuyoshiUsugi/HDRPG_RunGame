using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ViewとModelをつなぐクラス
/// </summary>
public class StageSelectScenePresenter : MonoBehaviour
{
    [SerializeField] InputBase _input;

    // Start is called before the first frame update
    void Start()
    {
        RegisterEvent();
    }

    /// <summary>
    /// ViewとModelの各イベントを登録する
    /// </summary>
    void RegisterEvent()
    {

    }
}
