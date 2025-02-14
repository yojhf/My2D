using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{ 
    public class MainMenu : MonoBehaviour
    {
        public CustomButton settingButton;

        // Start is called before the first frame update
        void Start()
        {
            // 버튼 이벤트에 함수 등록
            settingButton.onClick.AddListener(SettingButtonClicked);
        }

        public void SettingButtonClicked()
        {
            MenuManager.Instance.ShowPopup<Settings>();
        }
    }
    
}
