using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitialize : MonoBehaviour
{
    // 属性の設定
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("After Scene is loaded and game is running");
        // スクリーンサイズの指定
        Screen.SetResolution(540, 960, false);
    }
}
