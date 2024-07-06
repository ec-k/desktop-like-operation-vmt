using System;
using UnityEngine;
using uOSC;


namespace DesktopLikeOperationVMT
{
    [RequireComponent(typeof(uOscClient))]
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] Transform _headTarget;
        [SerializeField] Transform _leftHandTarget;
        [SerializeField] Transform _rightHandTarget;

        ButtonInput _input;
        UserAction _action;
        uOscClient _client;

        const string _moveAddress = "/VMT/Room/Unity";


        void Start()
        {
            _input = new();
            _action = new();
            _client = GetComponent<uOscClient>();

            _leftHandTarget.SetPositionAndRotation(_headTarget.position, _headTarget.rotation);
            _rightHandTarget.SetPositionAndRotation(_headTarget.position, _headTarget.rotation);
        }

        void FixedUpdate()
        {
            if (_input.UpMoveButton) Move(_action.MoveForward);
            if (_input.DownMoveButton) Move(_action.MoveBackward);
            if (_input.LeftMoveButton) Move(_action.MoveLeft);
            if (_input.RightMoveButton) Move(_action.MoveRight);

            var mouse_x = _input.MouseHorizontalMovement;
            var mouse_y = _input.MouseVerticalMovement;
            Rotate(mouse_x, _action.RotateYaw);
            Rotate(mouse_y, _action.RotatePitch);

            SendTargetTransform(_client, Settings.HeadIndex, TrackerEnables.TRACKER, _headTarget);
            SendTargetTransform(_client, Settings.LeftHandIndex, TrackerEnables.CONTROLLER_L, _leftHandTarget);
            SendTargetTransform(_client, Settings.RightHandIndex, TrackerEnables.CONTROLLER_R, _rightHandTarget);
        }

        private void Update()
        {
            //if(_input.MouseLeftClick) PressButton(_client, _input.MouseLeftClick, true, _action.ClickTrigger);
            PressButton(_client, _input.MouseLeftClick, true, _action.ClickTrigger);
            //DebugButtonInput(_input.MouseLeftClick, "LeftClick");
            //if (_input.SystemToggleButton) PressButton(_client, _input.SystemToggleButton, false, _action.ClickSystem);
            PressButton(_client, _input.SystemToggleButton, false, _action.ClickSystem);
            //DebugButtonInput(_input.SystemToggleButton, "SystemToggleButton");
        }

        void DebugButtonInput(bool buttonInput, string text)
        {
            if(buttonInput)
                Debug.Log(text);
        }

        void SendTargetTransform(uOscClient client, int index, TrackerEnables enable, Transform target)
        {
            client.Send(_moveAddress, index, (int)enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
            );
        }

        void Move(Action<Transform> moveAction)
        {
            moveAction(_headTarget);
            moveAction(_leftHandTarget);
            moveAction(_rightHandTarget);
        }
        void Rotate(float mouseMoveAmount, Action<float, Transform> rotateAction)
        {
            rotateAction(mouseMoveAmount, _headTarget);
            rotateAction(mouseMoveAmount, _leftHandTarget);
            rotateAction(mouseMoveAmount, _rightHandTarget);
        }

        void PressButton(uOscClient client, bool isHeld, bool isLeft, Func<bool, bool, Message> buttonAction)
        {
            client.Send(buttonAction(isHeld, isLeft));
        }

    }
}
