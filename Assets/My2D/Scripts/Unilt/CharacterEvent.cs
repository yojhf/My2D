using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class CharacterEvent
    {
        // ĳ���Ͱ� �������� ���� �� ��ϵ� �Լ� ����
        public static UnityAction<GameObject, float> characterDamaged;
        // ĳ���Ͱ� ���� �� ��ϵ� �Լ� ����
        public static UnityAction<GameObject, float> characterHealed;
    }
}