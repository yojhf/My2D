using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    // ������ �浹ü ����
    public class DetectionZone : MonoBehaviour
    {
        // ������ �ݶ��̴� ����Ʈ
        public List<Collider2D> detectedColliders = new List<Collider2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �浹ü�� �����Ǹ� ����Ʈ�� �߰�
            if(!detectedColliders.Contains(collision))
            {
                detectedColliders.Add(collision);
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �浹ü�� ������ ����Ʈ���� ����
            detectedColliders.Remove(collision);
        }

    }
}