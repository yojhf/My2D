using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    // ���� ���� â : ��(��������), �ƴϿ�
    public class ExitGame : Popup
    {
        // ��������
        [SerializeField] private CustomButton yes;

        private void OnEnable()
        {
            // �ɼ�â ��ư Ŭ�� �� ȣ��Ǵ� �Լ� ���
            yes.onClick.AddListener(StopGame);
        }

        void StopGame()
        {
            // ��������
            Application.Quit();
            Debug.Log("��������");
        }
    }
}