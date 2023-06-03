using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグ用のスクリプト
/// </summary>
public class DebugManager : MonoBehaviour
{
    [SerializeField] Text _fpsViewer; 

    int _frameCount;
    float _prevTime;
    float _fps;

    void Awake()
    {
        _frameCount = 0;
        _prevTime = 0.0f;
    }

    void Update()
    {
        _frameCount++;
        float time = Time.realtimeSinceStartup - _prevTime;

        if (time >= 0.5f)
        {
            _fps = _frameCount / time;
            _fpsViewer.text = $"FPS : {_fps}";

            _frameCount = 0;
            _prevTime = Time.realtimeSinceStartup;
        }
    }

}
