using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableCollisionOnGrab : MonoBehaviour
{
    public Collider swordCollider; // 검의 콜라이더
    public Collider playerCollider; // 플레이어의 콜라이더
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }


    void OnGrab(SelectEnterEventArgs args)
    {
        if (playerCollider != null && swordCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, swordCollider, true);
        }
    }


    void OnRelease(SelectExitEventArgs args)
    {
        if (playerCollider != null && swordCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, swordCollider, false);
        }
    }
}
