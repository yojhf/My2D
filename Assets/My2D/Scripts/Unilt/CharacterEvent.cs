using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class CharacterEvent
    {
        // 캐릭터가 데미지를 입을 때 등록된 함수 실행
        public static UnityAction<GameObject, float> characterDamaged;
        // 캐릭터가 힐할 때 등록된 함수 실행
        public static UnityAction<GameObject, float> characterHealed;
    }
}