using UnityEngine;


namespace DesktopLikeOperationVMT
{
    [RequireComponent(typeof(uOSC.uOscClient))]
    public class SimpleTracker : MonoBehaviour
    {
        public Transform target;
        public int index = 0;

        uOSC.uOscClient client;
        void Start()
        {
            client = GetComponent<uOSC.uOscClient>();
        }

        void Update()
        {
            const int enable = (int)TrackerEnables.TRACKER;
            const float timeoffset = 0f;

            client.Send("/VMT/Room/Unity", (int)index, (int)enable, (float)timeoffset,
                (float)target.transform.position.x,
                (float)target.transform.position.y,
                (float)target.transform.position.z,
                (float)target.transform.rotation.x,
                (float)target.transform.rotation.y,
                (float)target.transform.rotation.z,
                (float)target.transform.rotation.w
            );
        }
    }
}
