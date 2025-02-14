using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    // �ɼ�â
    public class Settings : Popup
    {
        // �������� â ����
        [SerializeField] private CustomButton back;
        // �������� ���� â
        [SerializeField] private CustomButton privacy;

        private void OnEnable()
        {
            // �ɼ�â ��ư Ŭ�� �� ȣ��Ǵ� �Լ� ���
            back.onClick.AddListener(BackToMain);
            privacy.onClick.AddListener(PrivacyPolicy);
        }

        void BackToMain()
        {
            StopInteraction();
            Close();

            // �������� â ����
            MenuManager.Instance.ShowPopup<ExitGame>();
        }

        void PrivacyPolicy()
        {
            StopInteraction();
            Close();

            // �������� ���� â ����
            MenuManager.Instance.ShowPopup<GDPR>();
        }
    }
}