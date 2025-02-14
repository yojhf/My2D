using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MySample
{
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        public List<Popup> popupStack = new List<Popup>();
        // �˾� �޴��� �θ�ĵ���� ������Ʈ
        [SerializeField] private Canvas canvas;

        private void OnEnable()
        {
            // �˾��� ����ƽ �̺�Ʈ �Լ� ���
            Popup.OnClosePopup += ClosePopup;
            Popup.OnBeforeClosePopup += BeforeClosePopup;
        }

        private void OnDisable()
        {
            // �˾��� ����ƽ �̺�Ʈ �Լ� ����
            Popup.OnClosePopup -= ClosePopup;
            Popup.OnBeforeClosePopup -= BeforeClosePopup;
        }

        void ClosePopup(Popup _popupClose)
        {
            if (popupStack.Count > 0)
            {
                popupStack.Remove(_popupClose);

                if (popupStack.Count > 0)
                {
                    var popup = popupStack.Last<Popup>();

                    popup.Show();
                }
            }
        }

        void BeforeClosePopup(Popup _popupClose)
        { 
            // TODO ���̵� ȿ��  
        }

        // �˾�â ����
        public T ShowPopup<T>(Action _onShow = null, Action<PopupResult> _onClose = null) where T : Popup
        {
            // �̹� â�� ���ȴ��� üũ
            if (popupStack.OfType<T>().Any())
            {
                return popupStack.OfType<T>().First();
            }

            return (T)ShowPopup("Popups/" + typeof(T).Name, _onShow, _onClose);
        }

        public Popup ShowPopup(string _path, Action _onShow = null, Action<PopupResult> _onClose = null)
        {
            // ���ҽ� �ε�
            if (popupStack.Any(p => p.GetType().Name == _path.Split('/').Last()))
            {
                return popupStack.First(p => p.GetType().Name == _path.Split('/').Last());
            }

            // ������
            var popupPrefab = Resources.Load<Popup>(_path);

            if (popupPrefab == null)
            {
                Debug.Log("ã�� �˾��� ����");
                return null;
            }

            return ShowPopup(popupPrefab, _onShow, _onClose);
        }

        public Popup ShowPopup(Popup _popupPrefab, Action _onShow = null, Action<PopupResult> _onClose = null)
        {
            var popup = Instantiate(_popupPrefab, canvas.transform);

            if (popupStack.Count > 0)
            { 
                popupStack.Last().Hide();
            }

            popupStack.Add(popup);
            popup.Show<Popup>(_onShow, _onClose);

            var rectTransform = popup.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            return popup;
        }

        // Ư�� Ÿ��(T)�� �˾��� ã�� �Լ�
        public T GetPopupOpened<T>() where T : Popup
        {
            foreach (var popup in popupStack)
            {
                if (popup.GetType() == typeof(T))
                { 
                    return (T)popup;
                }
            }

            return null;
        }

        // �����ִ� ��� â �ݱ�
        public void CloseAllPopups()
        {
            for (int i = 0; i < popupStack.Count; i++)
            {
                popupStack[i].Close();
            }

            popupStack.Clear();
        }

        // â�� �ϳ��� ���� �ִ���
        public bool IsAnyPopupOpened()
        {
            return popupStack.Count > 0;
        }

        // �������� ���� �ִ� â
        public Popup GetLastPopup()
        { 
            return popupStack.Last();
        }

    }
}