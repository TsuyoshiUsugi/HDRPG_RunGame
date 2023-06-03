using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーン中のポーズ処理を行うものに付けるインターフェース
/// </summary>
public interface IPosable
{
    void Pose(bool isPoseing);
}
