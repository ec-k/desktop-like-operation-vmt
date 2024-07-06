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
            if (_input.UpMoveButton) Move(_client, _action.MoveForward);
            if (_input.DownMoveButton) Move(_client, _action.MoveBackward);
            if (_input.LeftMoveButton) Move(_client, _action.MoveLeft);
            if (_input.RightMoveButton) Move(_client, _action.MoveRight);

            var mouse_x = _input.MouseHorizontalMovement;
            var mouse_y = _input.MouseVerticalMovement;
            Rotate(_client, mouse_x, _action.RotateYaw);
            Rotate(_client, mouse_y, _action.RotatePitch);

            PressButton(_client, _input.MouseLeftClick, true, _action.ClickTrigger);
            //PressButton(_client, _input.SystemToggleButton, true, _action.ClickSystem);
            PressButton(_client, _input.SystemToggleButton, false, _action.ClickSystem);
        }

        void Move(uOscClient client, Func<int, TrackerEnables, Transform, Message> moveAction)
        {
            client.Send(moveAction(Settings.HeadIndex, TrackerEnables.TRACKER, _headTarget));
            client.Send(moveAction(Settings.LeftHandIndex, TrackerEnables.CONTROLLER_L, _leftHandTarget));
            client.Send(moveAction(Settings.RightHandIndex, TrackerEnables.CONTROLLER_R, _rightHandTarget));
        }
        void Rotate(uOscClient client, float mouseMoveAmount, Func<int, TrackerEnables, float, Transform, Message> rotateAction)
        {
            client.Send(rotateAction(Settings.HeadIndex, TrackerEnables.TRACKER, mouseMoveAmount, _headTarget));
            client.Send(rotateAction(Settings.LeftHandIndex, TrackerEnables.CONTROLLER_L, mouseMoveAmount, _leftHandTarget));
            client.Send(rotateAction(Settings.RightHandIndex, TrackerEnables.CONTROLLER_R, mouseMoveAmount, _rightHandTarget));
        }

        void PressButton(uOscClient client, bool isHeld, bool isLeft, Func<bool, bool, Message> buttonAction)
        {
            client.Send(buttonAction(isHeld, isLeft));
        }

    }
}
