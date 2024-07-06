namespace DesktopLikeOperationVMT
{
    public class Settings
    {
        public static int HeadIndex = 0;
        public static int LeftHandIndex = 1;
        public static int RightHandIndex = 10;

        public static float MoveVelocity = 0.05f;
        public static float MouseSensitivity = 0.6f;
    }

    public enum TrackerEnables
    {
        DISABLE = 0,
        TRACKER = 1,
        CONTROLLER_L = 2,
        CONTROLLER_R = 3,
        TRACKING_REFERENCE = 4,
        CONTROLLER_L_COMPATIBLE = 5,
        CONTROLLER_R_COMPATIBLE = 6,
        TRACKER_COMPATIBLE = 7,
    }
}
