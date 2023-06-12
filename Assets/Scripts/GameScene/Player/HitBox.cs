using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// �����蔻����Ƃ�X�N���v�g
/// ���ݐڐG���Ă�����̂�ێ����A���J����
/// </summary>
public class HitBox : MonoBehaviour
{

    [SerializeField] bool _showBox;
    List<GameObject> _hitObjs =  new List<GameObject>();
    public List<GameObject> HitObjs => _hitObjs;

    private void Start()
    {
        TryGetComponent(out MeshRenderer mesh);
        if (_showBox) mesh.enabled = true;
        if (!_showBox) mesh.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() == null) return;
        _hitObjs.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() == null) return;
        _hitObjs.Remove(other.gameObject);
    }
}
