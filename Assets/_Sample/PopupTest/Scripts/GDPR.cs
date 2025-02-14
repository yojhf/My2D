using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MySample
{
    // 개인정보 동의 창 : 동의, 취소, 개인정보페이지 링크
    public class GDPR : Popup
    {
        public void OnUserClickAccpet()
        {
            // 개인정보 동의 저장
            Close();
        }

        public void OnUserClickCancel()
        {
            // 개인정보 거부 저장
            Close();
        }

        public void OnUserClickPrivacyPolicy()
        {
            //Application.OpenURL("home_page_url");
        }
    }
}