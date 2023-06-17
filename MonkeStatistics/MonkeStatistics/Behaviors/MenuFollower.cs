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

            Vector3 LerpedPosition = Vector3.Lerp(transform.position, Target.position + Vector3.up * 0.2f, 10f);
            transform.position = LerpedPosition;
        }
    }
}
