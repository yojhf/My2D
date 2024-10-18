using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{

    // 지정된 충돌체 감지
    public class DetectionZone : MonoBehaviour
    {
        // 감지된 콜라이더 리스트
        public List<Collider2D> detectedColliders = new List<Collider2D>();

        // 충돌체가 없으면 호출되는 델리게이트 함수
        public UnityAction  noColRemain;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 충돌체가 감지되면 리스트에 추가
            if(!detectedColliders.Contains(collision))
            {
                detectedColliders.Add(collision);
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // 충돌체가 나가면 리스트에서 삭제
            detectedColliders.Remove(collision);

            if(detectedColliders.Count == 0)
            {
                noColRemain?.Invoke();
            }
        }

    }
}