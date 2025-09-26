using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsContorller : MonoBehaviour
{
    [SerializeField] private float mouse_speed = 100f;
    [SerializeField] private Transform playerBody;
    private float x_rotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * mouse_speed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_speed * Time.deltaTime;

        // ��ֱ��ת�����¿���
        x_rotation -= mouseY;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f); // ���ƽǶ�

        // Ӧ����ת
        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);

        // ˮƽ��ת�����ҿ���- ��ת�����������
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
