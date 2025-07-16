using System;
using System.Collections.Generic;

/// <summary>
/// Mediator ������ ������� �̺�Ʈ�� �߰��ϴ� Ŭ����.
/// �پ��� ��ü ���� ������ ������ ���� �̺�Ʈ ������ ó��.
/// </summary>
namespace Utils
{
    public class Mediator<TEnum> : IMediator where TEnum : Enum
    {
        // �̱���
        Dictionary<TEnum, List<IMediatorEvent>> _eventDict = new Dictionary<TEnum, List<IMediatorEvent>>();

        /// <summary>
        /// Ư�� �̺�Ʈ Ÿ�Կ� �̺�Ʈ ������ ���
        /// </summary>
        /// <param name="key">�̺�Ʈ Ÿ��</param>
        /// <param name="value">�̺�Ʈ ��� ��ü</param>
        public void Register(TEnum key, IMediatorEvent value)
        {
            List<IMediatorEvent> list;

            if (!_eventDict.TryGetValue(key, out list))
            {
                list = new List<IMediatorEvent> { value };
                _eventDict[key] = list;
            }
            else if (!list.Contains(value))
                list.Add(value);
        }

        /// <summary>
        /// Ư�� �̺�Ʈ Ÿ�Կ��� �̺�Ʈ �����ʸ� ����.
        /// �����ʰ� ��� ���ŵǸ� �ش� Ű�� Dictionary���� ���ŵ�.
        /// </summary>
        /// <param name="key">�̺�Ʈ Ÿ��</param>
        /// <param name="value">������ �̺�Ʈ ��� ��ü</param>
        public void Unregister(TEnum key, IMediatorEvent value)
        {
            if (_eventDict.TryGetValue(key, out List<IMediatorEvent> list))
            {
                list.Remove(value);
                if (list.Count == 0)
                    _eventDict.Remove(key);
            }
        }

        /// <summary>
        /// Ư�� �̺�Ʈ Ÿ���� ��� �����ʿ� �̺�Ʈ �߻� �˸�
        /// </summary>
        /// <param name="key">�̺�Ʈ Ÿ��</param>
        /// <param name="data">�̺�Ʈ�� �Բ� ������ ������</param>
        public void Notify(TEnum key, object data = null)
        {
            if (_eventDict.TryGetValue(key, out List<IMediatorEvent> list))
            {
                for (int i = 0; i < list.Count; i++)
                    list[i].HandleEvent(data);
            }
        }

        /// <summary>
        /// ��ϵ� ��� �̺�Ʈ ������ ����
        /// �� �̵� �� ���
        /// </summary>
        public void Clear()
        {
            _eventDict.Clear();
        }
    }
}