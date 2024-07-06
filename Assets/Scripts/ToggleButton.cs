using UnityEngine;
using UnityEngine.UI;

namespace DesktopLikeOperationVMT
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] Sprite _activeIcon;
        [SerializeField] Sprite _inactiveIcon;
        [SerializeField] Color32 _inactiveIconColor = new(34, 34, 34, 255);
        [SerializeField] Color32 _inactiveBGColor = new(214, 214, 214, 255);
        [SerializeField] Color32 _activeIconColor = new(255, 255, 255, 255);
        [SerializeField] Color32 _activeBGColor = new(156, 39, 176, 255);

        public Button Button { get; private set; }

        Image _icon;
        Image _bg;


        private void Start()
        {
            _icon = transform.GetChild(0).GetComponent<Image>();
            _bg = GetComponent<Image>();
            Button = GetComponent<Button>();

            _icon.sprite = _inactiveIcon;
            _icon.color = _inactiveIconColor;
            _bg.color = _inactiveBGColor;
        }

        public void SetButtonImage(bool isSenderEnabled)
        {
            _icon.sprite = isSenderEnabled ? _activeIcon : _inactiveIcon;
            _icon.color = isSenderEnabled ? _activeIconColor : _inactiveIconColor;
            _bg.color = isSenderEnabled ? _activeBGColor : _inactiveBGColor;
        }
    }
}