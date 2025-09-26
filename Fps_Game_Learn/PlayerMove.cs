using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jump_force = 2f;
    [SerializeField] private int max_jump_times = 2;
    [SerializeField] private float rayLength = 2f;
    [SerializeField] private Transform camera;
    // Start is called before the first frame update
    private Rigidbody rb;
    private int jump_times;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        jump_times = max_jump_times;
    }
    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    void Jump()
    {
        bool IsGrounded = GroundCheckWithSphereCast();
        if(IsGrounded ) { jump_times = max_jump_times; }
        Vector3 rayOrigin = transform.position;
        RaycastHit hit;
        if (Input.GetButtonDown("Jump")&&jump_times>0)
        {
            rb.AddForce(Vector3.up * jump_force*jump_times/max_jump_times, ForceMode.Impulse);
            jump_times--;
        }
    }
    bool GroundCheckWithSphereCast()
    {
        float radius = 0.4f; 
        Vector3 origin = transform.position;
        origin.y -= GetComponent<CapsuleCollider>().height / 2 - radius; 
        RaycastHit hit;
        return (Physics.SphereCast(origin, radius, Vector3.down, out hit, rayLength));

    }
    void Move()
    {
        // ��ȡ����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ���������������ƶ�����
        Vector3 cameraForward = camera.forward;
        Vector3 cameraRight = camera.right;

        // ��������Ĵ�ֱ������ֻ����ˮƽ����
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // ������������������ƶ�����
        Vector3 movement = (cameraForward * verticalInput) + (cameraRight * horizontalInput);
        movement = movement.normalized * move_speed * Time.fixedDeltaTime;

        // Ӧ���ƶ�
        rb.MovePosition(rb.position + movement);
    }
}
