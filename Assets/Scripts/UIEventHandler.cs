using UnityEngine;

namespace DesktopLikeOperationVMT
{
    public class UIEventHandler : MonoBehaviour
    {
        [SerializeField] ToggleButton _systemToggleButton;
        [SerializeField] InputHandler _inputHandler;
        //void Update()
        //{
        //    _systemToggleButton.Button.AddListener +=
        //}

        public void ButtonClick()
        {
            _inputHandler.ToggleState();
            _systemToggleButton.SetButtonImage(_inputHandler.IsActive);
        }


    }
}
