using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// View��Model���Ȃ��N���X
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
    /// View��Model�̊e�C�x���g��o�^����
    /// </summary>
    void RegisterEvent()
    {

    }
}
