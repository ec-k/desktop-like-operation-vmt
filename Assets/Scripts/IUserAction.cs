using UnityEngine;
using uOSC;

namespace DesktopLikeOperationVMT
{
    public interface IUserAction
    {
        public Message MoveForward(int index, TrackerEnables enable, Transform target);
        public Message MoveBackward(int index, TrackerEnables enable, Transform target);
        public Message MoveUp(int index, TrackerEnables enable, Transform target);
        public Message MoveDown(int index, TrackerEnables enable, Transform target);
        public Message MoveLeft(int index, TrackerEnables enable, Transform target);
        public Message MoveRight(int index, TrackerEnables enable, Transform target);
        public Message RotateYaw(int index, TrackerEnables enable, float mouse_x, Transform target);
        public Message RotatePitch(int index, TrackerEnables enable, float mouse_y, Transform target);
        public Message ClickTrigger(bool isHeld, bool isLeft = true);
        public Message ClickGrip(bool isHeld, bool isLeft = true);
        public Message ClickButtonA(bool isHeld, bool isLeft = true);
        public Message ClickButtonB(bool isHeld, bool isLeft = true);
        public Message ClickSystem(bool isHeld, bool isLeft = true);
    }
}
