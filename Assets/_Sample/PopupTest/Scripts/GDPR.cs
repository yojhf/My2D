using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MySample
{
    // �������� ���� â : ����, ���, �������������� ��ũ
    public class GDPR : Popup
    {
        public void OnUserClickAccpet()
        {
            // �������� ���� ����
            Close();
        }

        public void OnUserClickCancel()
        {
            // �������� �ź� ����
            Close();
        }

        public void OnUserClickPrivacyPolicy()
        {
            //Application.OpenURL("home_page_url");
        }
    }
}