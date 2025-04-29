using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;


    public void PlayerFollow()
    {
        if (target == null)
        {
            foreach (var player in FindObjectsByType<PlayerManager>(FindObjectsSortMode.None))
            {
                if (player.photonView.IsMine)
                {
                    target = player.transform;
                    break;
                }
            }
        }

        if (target == null) return;
        Vector3 desiredPosition = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, followSpeed * Time.deltaTime);
    }

}
