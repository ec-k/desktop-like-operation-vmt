using UnityEngine;

namespace DesktopLikeOperationVMT
{
    public class ButtonInput
    {
        public bool UpMoveButton => Input.GetKey(KeyCode.W);
        public bool DownMoveButton => Input.GetKey(KeyCode.S);
        public bool LeftMoveButton => Input.GetKey(KeyCode.A);
        public bool RightMoveButton => Input.GetKey(KeyCode.D);

        public bool MouseLeftClick => Input.GetMouseButton(0);

        public float MouseHorizontalMovement => Input.GetAxis("Mouse X");
        public float MouseVerticalMovement => Input.GetAxis("Mouse Y");

        public bool SystemToggleButton => Input.GetKey(KeyCode.Z);
        public bool ButtonA => Input.GetKey(KeyCode.X);
        public bool ButtonB => Input.GetKey(KeyCode.C);
    }
}
