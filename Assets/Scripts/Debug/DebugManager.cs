using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �f�o�b�O�p�̃X�N���v�g
/// 
/// �@�\
/// FPS�\��
/// scene���J�n���Ă���̎��ԕ\��
/// �Z�[�u�f�[�^�S����
/// 
/// </summary>
public class DebugManager : MonoBehaviour
{
    [SerializeField] Text _fpsViewer; 
    [SerializeField] Text _timerViewer;
    [SerializeField] bool _deleteData = false;

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

        if (_deleteData) DeleteSave();
    }

    void Update()
    {
        DebugFrame();
        DebugTime();
    }

    private void DebugFrame()
    {
        if (_fpsViewer == null) return;

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
        if (_timerViewer == null) return;

        _timer += Time.deltaTime;

        _timerViewer.text = $"Time: {_timer}";
    }

    void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("�S�f�[�^������");
    }
}
