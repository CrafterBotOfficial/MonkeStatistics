using GorillaLocomotion;
using MonkeStatistics.UI;
using UnityEngine;

namespace MonkeStatistics.UI;

internal class MenuFollower : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(GTPlayer.Instance.headCollider.transform.position.x, transform.position.y, GTPlayer.Instance.headCollider.transform.position.z);
        transform.LookAt(targetPosition);

        Vector3 targettedPosition = LocalWatchManager.Instance.Watch.position + Vector3.up * .2f;
        transform.position = targettedPosition;
    }
}
