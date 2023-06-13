using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ���̓N���X�̊��N���X
/// </summary>
public abstract class InputBase : MonoBehaviour
{

    public abstract event Action OnRightButtonClicked;

    public abstract event Action OnMiddleButtonClicked;

    public abstract event Action OnLeftButtonClicked;

    public abstract event Action OnOptionButtonClicked;
}
