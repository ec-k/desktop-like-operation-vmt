using UnityEngine;
using uOSC;

namespace DesktopLikeOperationVMT
{
    public class UserAction: IUserAction
    {
        const string _inputAdressPrefix = "/VMT/Input/";

        public void MoveForward(Transform target)
        {
            target.position += new Vector3(0f, 0f, Settings.MoveVelocity);
        }
        public void MoveBackward(Transform target)
        {
            target.position += new Vector3(0f, 0f, -Settings.MoveVelocity);
        }
        public void MoveUp(Transform target)
        {
            target.position += new Vector3(0f, Settings.MoveVelocity, 0f);
        }
        public void MoveDown(Transform target)
        {
            target.position += new Vector3(0f, -Settings.MoveVelocity, 0f);
        }
        public void MoveLeft(Transform target)
        {
            target.position += new Vector3(-Settings.MoveVelocity, 0f, 0f);
        }

        public void MoveRight(Transform target)
        {
            target.position += new Vector3(Settings.MoveVelocity, 0f, 0f);
        }

        public void RotateYaw(float mouse_x, Transform target)
        {
            var rotationAmount = mouse_x * Settings.MouseSensitivity;
            target.localRotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * target.localRotation;
        }
        public void RotatePitch(float mouse_y, Transform target)
        {
            var rotationAmount = mouse_y * Settings.MouseSensitivity;
            target.localRotation = Quaternion.AngleAxis(-rotationAmount, target.right) * target.localRotation;
        }

        public Message ClickTrigger(bool isHeld, bool isLeft)
        {
            var address = _inputAdressPrefix + "Trigger";
            const int triggerIndex = 0;
            float value = isHeld ? 1f : 0f;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;

            return new(address, deviceIndex, triggerIndex, 0f, value);
        }

        Message LogOscMessage(Message msg)
        {
            Debug.Log(msg);
            return msg;
        }

        public Message ClickGrip(bool isHeld, bool isLeft)
        {
            var address = _inputAdressPrefix + "Trigger";
            const int triggerIndex = 1;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;
            
            return new(address, deviceIndex, triggerIndex, 0f, value);
        }

        public Message ClickButtonA(bool isHeld, bool isLeft)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 1;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;
            
            return new(address, deviceIndex, buttonIndex, 0f, value);
        }

        public Message ClickButtonB(bool isHeld, bool isLeft)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 3;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;

            return new(address, deviceIndex, buttonIndex, 0f, value);
        }

        public Message ClickSystem(bool isHeld, bool isLeft)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 0;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;

            return new(address, deviceIndex, buttonIndex, 0f, value);
        }
    }
}
