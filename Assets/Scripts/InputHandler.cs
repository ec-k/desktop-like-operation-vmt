using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using uOSC;
using static UnityEngine.GraphicsBuffer;


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

        public bool IsActive { get; private set; }

        public void ToggleState()
        {
            IsActive = !IsActive;
            if(!IsActive)
                OnStop();
        }

        void OnStop()
        {
            DisableTracker(Settings.HeadIndex);
            DisableTracker(Settings.LeftHandIndex);
            DisableTracker(Settings.RightHandIndex);
        }
        void DisableTracker(int index)
        {
            _client.Send(_moveAddress, index, (int)0, 0f,
                0f, 0f, 0f,
                0f, 0f, 0f, 0f
                );
        }

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
            if (!IsActive) return;

            if (_input.UpMoveButton) Move(_action.MoveForward);
            if (_input.DownMoveButton) Move(_action.MoveBackward);
            if (_input.LeftMoveButton) Move(_action.MoveLeft);
            if (_input.RightMoveButton) Move(_action.MoveRight);

            var mouse_x = _input.MouseHorizontalMovement;
            var mouse_y = _input.MouseVerticalMovement;
            Rotate(mouse_x, _action.RotateYaw);
            Rotate(mouse_y, _action.RotatePitch);

            SendTargetTransform(_client, Settings.HeadIndex, TrackerEnables.TRACKER, _headTarget);
            SendTargetTransform(_client, Settings.LeftHandIndex, TrackerEnables.CONTROLLER_L_COMPATIBLE, _leftHandTarget);
            SendTargetTransform(_client, Settings.RightHandIndex, TrackerEnables.CONTROLLER_R_COMPATIBLE, _rightHandTarget);
        }

        private void Update()
        {
            if(!IsActive) return;

            var bundle = new Bundle(Timestamp.Now);
            bundle.Add(_action.ClickTrigger(_input.MouseLeftClick, true));
            bundle.Add(_action.ClickSystem(_input.SystemToggleButton, false));
            bundle.Add(_action.ClickButtonA(_input.ButtonA, false));
            bundle.Add(_action.ClickButtonB(_input.ButtonB, false));

            _client.Send(bundle);
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
