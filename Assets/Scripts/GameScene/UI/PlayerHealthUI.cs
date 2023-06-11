using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̌��݂�HP��\������
/// </summary>
public class PlayerHealthUI : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] GameObject _playerUIParent;
    [SerializeField] GameObject _hpImagePrefab;
    [SerializeField] List<GameObject> _hpImages = new List<GameObject>();
    [SerializeField] Vector3 _hpImageScale = Vector3.one;

    // Start is called before the first frame update
    void Awake()
    {
        _hpImages.ForEach(image => image.SetActive(false));
    }

    /// <summary>
    /// HP�摜��\������B����Ȃ���ΐ���
    /// </summary>
    /// <param name="currentHp"></param>
    public void ShowHp(int currentHp)
    {
        for (int i = 0; i < currentHp; i++)
        {
            if (_hpImages.Count - 1 < i)
            {
                var obj = Instantiate(_hpImagePrefab);
                obj.transform.SetParent(_playerUIParent.transform);
                obj.GetComponent<RectTransform>().localScale = _hpImageScale;
                _hpImages.Add(obj);
                continue;
            }

            _hpImages[i].SetActive(true);
        }

        for (int i = 0; i < _hpImages.Count; i++)
        {
            if (i < currentHp) continue;

            _hpImages[i].SetActive(false);
        }
    }
}
