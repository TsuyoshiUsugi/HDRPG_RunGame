using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialize
{
    /// <summary>
    /// Unity�̃C���X�y�N�^�[��ɕ\���ł��鎫���^�̃N���X
    /// �V���A���C�Y�\�ȃ��X�g�Ɏ����^����N���X�̊e�f�[�^��o�^���邱�Ƃŉ\�ɂ��Ă���
    /// �g���Ƃ��͌p������[System.Serializeble]��t���邱��
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="Type"></typeparam>
    public class SerializeDictonary<TKey, TValue, Type> where Type : KeyAndValue<TKey, TValue>
    {
        [SerializeField] List<Type> _list;
        Dictionary<TKey, TValue> _dict;

        public Dictionary<TKey, TValue> GetTable ()
        {
            if (_dict == null)
            {
                _dict = ConvertListToDictionary(_list);
            }
            return _dict;
        }

        public List<Type> GetList()
        {
            return _list;
        }

        static Dictionary<TKey, TValue> ConvertListToDictionary(List<Type> list)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue> ();
            foreach(KeyAndValue<TKey, TValue> pair in list)
            {
                dic.Add (pair.Key, pair.Value);
            }

            return dic;
        }
    }

    [System.Serializable]
    public class KeyAndValue<Tkey, TValue>
    {
        public Tkey Key;
        public TValue Value;

        public KeyAndValue(Tkey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public KeyAndValue(KeyValuePair<Tkey, TValue> pair)
        {
            Key = pair.Key;
            Value = pair.Value;
        }
    }

}

