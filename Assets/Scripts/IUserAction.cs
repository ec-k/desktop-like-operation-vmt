using UnityEngine;
using uOSC;

namespace DesktopLikeOperationVMT
{
    public interface IUserAction
    {
        public void MoveForward(Transform target);
        public void MoveBackward(Transform target);
        public void MoveUp(Transform target);
        public void MoveDown(Transform target);
        public void MoveLeft(Transform target);
        public void MoveRight(Transform target);
        public void RotateYaw(float mouse_x, Transform target);
        public void RotatePitch(float mouse_y, Transform target);
        public Message ClickTrigger(bool isHeld, bool isLeft = true);
        public Message ClickGrip(bool isHeld, bool isLeft = true);
        public Message ClickButtonA(bool isHeld, bool isLeft = true);
        public Message ClickButtonB(bool isHeld, bool isLeft = true);
        public Message ClickSystem(bool isHeld, bool isLeft = true);
    }
}
