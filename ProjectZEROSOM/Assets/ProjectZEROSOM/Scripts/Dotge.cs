using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Dotge : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField]
    private InputActionProperty leftTriggerAction; // ���� Ʈ���� �Է�
    public Transform transformMainCamera;

    [Header("Dodge Settings")]
    public float dodgeSpeed = 5f; // ���� �ӵ�
    public float threshold = 0.5f; // �ּ� ���ӵ� �� (�� �� �̻��� �� ����)
    public float dodgeCooldown = 3f; // ���� ��Ÿ�� (��)
    private bool canDodge = true; // ���� ���� ����
    public float dodgeDuration = 0.5f; // ���� ���ӽð�
    private float currentSpeed = 0f;

    private UnityEngine.XR.InputDevice hmdDevice;
    private ContinuousMoveProviderBase locomotionSystem; // ���ڸ�� �ý���
    public CharacterController characterController;
    private bool isdevices = false;

    void Start()
    {
        locomotionSystem = FindObjectOfType<ContinuousMoveProviderBase>();
        if (locomotionSystem == null)
        {
            Debug.LogWarning("Continuous Move Provider�� ������ �߰ߵ��� ����. ���ڸ�� ���� �۵��� ���� ����.");
        }
        Debug.Log("����� �׽�Ʈ");
    }

    void Update()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);
        if (isdevices == false)
        {
            if (devices.Count > 0)
            {
                Debug.Log("HMD ����̽� ������: " + devices[0].name);
                hmdDevice = devices[0];
                Debug.Log("hmdDevice name: " + hmdDevice);
                isdevices = true;
            }
        }

        if (leftTriggerAction.action?.ReadValue<float>() > 0.8f)
        {
            Debug.Log("����Ʈ ��Ʈ�ѷ� Ʈ���Ŵ���");
        }
        if (canDodge && leftTriggerAction.action?.ReadValue<float>() > 0.8f)
        {
            if (hmdDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity))
            {
                // Ư�� �� �̻��� ���� �� �߰�
                if (angularVelocity.magnitude > threshold)
                {
                    StartCoroutine(ApplyDodge(angularVelocity));
                    Debug.Log($"���� ����! ���ӵ� ���Ⱚ X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
                else
                {
                    Debug.Log($"���ӵ� ���Ⱚ X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
            }
        }
    }

    IEnumerator ApplyDodge(Vector3 angularVelocity)
    {
        canDodge = false;
        Vector3 LocalangularVelocity = transformMainCamera.TransformDirection(angularVelocity);

        // angularVelocity y �� ����
        LocalangularVelocity.y = 0;
        // forceDirection ���� ���� ���ӵ��� ����ȭ�Ͽ� ���
        Vector3 forceDirection = LocalangularVelocity.normalized;
        Debug.Log($"���� ���� X: {forceDirection.x}, Y: {forceDirection.y}, Z: {forceDirection.z}");
        forceDirection = Quaternion.Euler(0, -90, 0) * forceDirection;


        float elapsedTime = 0f;
        while (elapsedTime < dodgeDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / dodgeDuration;
            float speedFactor = Mathf.Lerp(0, 1, Mathf.Sin(progress * Mathf.PI));

            currentSpeed = dodgeSpeed * speedFactor;
            Vector3 moveDirection = forceDirection * currentSpeed * Time.deltaTime;
            characterController.Move(moveDirection);
            yield return null;

            CollisionFlags flags = characterController.Move(moveDirection);
            if ((flags & CollisionFlags.Sides) != 0) // ���� �浹�ߴ��� Ȯ��
            {
                break;
            }
        }

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // ��Ÿ�� �� �ٽ� ���� ����
    }
}
