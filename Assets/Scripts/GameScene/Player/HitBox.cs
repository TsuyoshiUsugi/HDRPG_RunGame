using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 当たり判定をとるスクリプト
/// 現在接触しているものを保持し、公開する
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
