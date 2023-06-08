using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �f�o�b�O�p�̃X�N���v�g
/// </summary>
public class DebugManager : MonoBehaviour
{
    [SerializeField] Text _fpsViewer; 
    [SerializeField] Text _timerViewer;

    //�^�C�}�[
    float _timer = 0;

    //FPS�\���֌W
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
        DebugFrame();
        DebugTime();
    }

    private void DebugFrame()
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

    private void DebugTime()
    {
        _timer += Time.deltaTime;

        _timerViewer.text = $"Time: {_timer}";
    }
}
