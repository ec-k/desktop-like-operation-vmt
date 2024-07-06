using UnityEngine;
using uOSC;

namespace DesktopLikeOperationVMT
{
    public class UserAction: IUserAction
    {
        const string _inputAdressPrefix = "/VMT/Input/";
        const string _moveAddress = "/VMT/Room/Unity";

        public Message MoveForward(int index, TrackerEnables enable, Transform target)
        {
            target.position += new Vector3(0f, 0f, Settings.MoveVelocity);

            return new(_moveAddress, index, (int)enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }
        public Message MoveBackward(int index, TrackerEnables enable, Transform target)
        {
            target.position += new Vector3(0f, 0f, -Settings.MoveVelocity);

            return new(_moveAddress, index, (int)enable, 0f,
            (float)target.position.x,
            (float)target.position.y,
            (float)target.position.z,
            (float)target.rotation.x,
            (float)target.rotation.y,
            (float)target.rotation.z,
            (float)target.rotation.w
            );
        }
        public Message MoveUp(int index, TrackerEnables enable,Transform target)
        {
            target.position += new Vector3(0f, Settings.MoveVelocity, 0f);

            return new(_moveAddress, index, (int)enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }
        public Message MoveDown(int index, TrackerEnables enable,Transform target)
        {
            target.position += new Vector3(0f, -Settings.MoveVelocity, 0f);

            return new(_moveAddress, index, (int)enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }
        public Message MoveLeft(int index, TrackerEnables enable, Transform target)
        {
            target.position += new Vector3(-Settings.MoveVelocity, 0f, 0f);

            return new(_moveAddress, index, (int)enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }

        public Message MoveRight(int index,TrackerEnables enable, Transform target)
        {
            target.position += new Vector3(Settings.MoveVelocity, 0f, 0f);

            return new(_moveAddress, index, enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }

        public Message RotateYaw(int index, TrackerEnables enable,float mouse_x, Transform target)
        {
            var rotationAmount = mouse_x * Settings.MouseSensitivity;
            target.localRotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * target.localRotation;

            return new(_moveAddress, index, enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }
        public Message RotatePitch(int index, TrackerEnables enable, float mouse_y, Transform target)
        {
            var rotationAmount = mouse_y * Settings.MouseSensitivity;
            target.localRotation = Quaternion.AngleAxis(-rotationAmount, target.right) * target.localRotation;

            return new(_moveAddress, index, enable, 0f,
                (float)target.position.x,
                (float)target.position.y,
                (float)target.position.z,
                (float)target.rotation.x,
                (float)target.rotation.y,
                (float)target.rotation.z,
                (float)target.rotation.w
                );
        }

        public Message ClickTrigger(bool isHeld, bool isLeft = true)
        {
            var address = _inputAdressPrefix + "Trigger";
            const int triggerIndex = 0;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;
            
            return new(address, deviceIndex, triggerIndex, 0f, value);
        }

        public Message ClickGrip(bool isHeld, bool isLeft = true)
        {
            var address = _inputAdressPrefix + "Trigger";
            const int triggerIndex = 1;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;
            
            return new(address, deviceIndex, triggerIndex, 0f, value);
        }

        public Message ClickButtonA(bool isHeld, bool isLeft = true)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 1;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;
            
            return new(address, deviceIndex, buttonIndex, 0f, value);
        }

        public Message ClickButtonB(bool isHeld, bool isLeft = true)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 3;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;

            return new(address, deviceIndex, buttonIndex, 0f, value);
        }

        public Message ClickSystem(bool isHeld, bool isLeft = true)
        {
            var address = _inputAdressPrefix + "Button";
            const int buttonIndex = 0;
            int value = isHeld ? 1 : 0;
            int deviceIndex = isLeft ? Settings.LeftHandIndex : Settings.RightHandIndex;

            return new(address, deviceIndex, buttonIndex, 0f, value);
        }
    }
}
