using GorillaLocomotion;
using UnityEngine;

namespace MonkeStatistics.Behaviors
{
    internal class MenuFollower : MonoBehaviour
    {
        internal Transform Target;
        private void FixedUpdate()
        {
            Vector3 TargetPosition = new Vector3(Player.Instance.headCollider.transform.position.x, transform.position.y, Player.Instance.headCollider.transform.position.z);
            transform.LookAt(TargetPosition);

            Vector3 TargettedPosition = Target.position + Vector3.up * .2f;
            transform.position = TargettedPosition;
        }
    }
}
