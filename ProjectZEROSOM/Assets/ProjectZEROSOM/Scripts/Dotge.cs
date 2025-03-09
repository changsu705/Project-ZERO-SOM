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

    [Header("Dodge Settings")]
    public float forceAmount = 5f; // �߰��� ���� ũ��
    public float threshold = 0.5f; // �ּ� ���ӵ� �� (�� �� �̻��� �� �� �߰�)
    public float dodgeCooldown = 1f; // ���� ��Ÿ�� (��)
    private bool canDodge = true; // ���� ���� ����

    private UnityEngine.XR.InputDevice hmdDevice;
    public Rigidbody rb;
    private ContinuousMoveProviderBase locomotionSystem; // ���ڸ�� �ý���
    private float locomotionSpeed; // ���ڸ�� �̵� �ӵ�

    void Start()
    {
        // Rigidbody ��������
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            GameObject xrOrigin = GameObject.Find("XR Origin (XR Rig)");
            if (xrOrigin != null)
            {
                rb = xrOrigin.GetComponent<Rigidbody>();
            }
            Debug.Log(xrOrigin);
            return;
        }

        // HMD ��ġ ã��
        var devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);

        if (devices.Count > 0)
        {
            hmdDevice = devices[0];
            if (!hmdDevice.isValid)
            {
                Debug.Log("���� ����Ƽ�� ã���� ����");
            }
        }
        else
        {
            Debug.Log("����� ��ġ ã���� ����");
        }

        locomotionSystem = FindObjectOfType<ContinuousMoveProviderBase>();
        if (locomotionSystem == null)
        {
            Debug.LogWarning("Continuous Move Provider�� ������ �߰ߵ��� ����. ���ڸ�� ���� �۵��� ���� ����.");
        }
        Debug.Log("����� �׽�Ʈ");
    }

    void Update()
    {
        if(leftTriggerAction.action?.ReadValue<float>() > 0.8f)
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
                    StartCoroutine(ApplyForce(angularVelocity));
                    Debug.Log($"���� ����! ���ӵ� ���Ⱚ X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
            }
            else
            {
                Debug.Log("�ص�� ���ӵ��� �޾ƿ� �� ����.");
            }
            }
    }

    IEnumerator ApplyForce(Vector3 angularVelocity)
    {
        canDodge = false;
        if (rb == null) yield break;

        if (locomotionSystem != null)
        {
            locomotionSystem.enabled = false;
            rb.isKinematic = false;
        }

        angularVelocity.y = 0;

        // forceDirection ���� ���� ���ӵ��� ����ȭ�Ͽ� ���
        Vector3 forceDirection = angularVelocity.normalized;

        // Rigidbody�� �� �߰�
        rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
        Debug.Log($"���� ����! ���� ���� X: {forceDirection.x}, Y: {forceDirection.y}, Z: {forceDirection.z} \n���� �� {forceAmount}");

        locomotionSpeed = locomotionSystem.moveSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < dodgeCooldown)
        {
            elapsedTime += Time.deltaTime;

            // ���� ���� �ӵ��� ���ڸ�� �ӵ����� �������� ��� ���ڸ�� Ȱ��ȭ
            if (rb.velocity.magnitude <= locomotionSpeed)
            {
                if (locomotionSystem != null && !locomotionSystem.enabled)
                {
                    locomotionSystem.enabled = true;
                    rb.isKinematic = true;
                }
                else
                {
                    locomotionSystem.enabled = true;
                    rb.isKinematic = true;
                    Debug.Log("���ڸ�ǿ����߻� �۵����� ��� �������� ���ڸ������ �������� .");
                }
                break; // ���� Ż��
            }
            yield return null;
        }

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // ��Ÿ�� �� �ٽ� ���� ����
    }
}
