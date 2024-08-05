using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    //W�� A�� S�� D��
    //CŰ: �Ʒ��� �����̽�Ű: ��
    //Mouse������

    [SerializeField] float moveSpeed = 5f;//�̵��ӵ�
    [SerializeField] float mouseSensitivity = 100f;//���콺 ����
    private Vector3 rotateValue;//���콺 ȸ����

    Camera cam;

    void Start()
    {
        cam = Camera.main;//�̱����� ���ؼ� �����ȿ� �Է�
    }

    void Update()
    {
        moveing();
        mouseRotation();
    }

    //x30 + 30 
    //���ʹϾ� * ���ʹϾ� = ������ �ǰԵ�

    private void moveing()
    {
        //3d���� �̵��ϴ� ��� //�ٶ󺸴� ������ ���� ������
        if (Input.GetKey(KeyCode.W))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;//�߿� ���� ����ϱ�
            cam.transform.position += cam.transform.forward * moveSpeed * Time.deltaTime;
            //cam.transform.position += Vector3.forward * cam.transform.rotation * moveSpeed * Time.deltaTime;
            //���� ó�� Vector�� ���� ���ϸ� ������ �߻���
            //Quaternion value = cam.transform.rotation * cam.transform.rotation;
            //���: transform.position += new Vector3(0, 0, 1);

            cam.transform.position += transform.TransformDirection(Vector3.left) * moveSpeed * Time.deltaTime;

            //Vector3.MoveTowards()
        }
        if (Input.GetKey(KeyCode.A))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.left * moveSpeed * Time.deltaTime;
            cam.transform.position += -cam.transform.right * moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.back * moveSpeed * Time.deltaTime;
            cam.transform.position += -cam.transform.forward * moveSpeed * Time.deltaTime;
            //���: transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.right * moveSpeed * Time.deltaTime;
            cam.transform.position += cam.transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            cam.transform.position +=  Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.C))
        {
            cam.transform.position +=  Vector3.down * moveSpeed * Time.deltaTime;
        }

        //3d���� �̵��ϴ� ��� //���������ǿ� ���� ������
        //if (Input.GetKey(KeyCode.W))
        //{
        //    cam.transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        //    Quaternion value = cam.transform.rotation * cam.transform.rotation;
        //    ���: transform.position += new Vector3(0, 0, 1);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    cam.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    cam.transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        //    ���: transform.position += new Vector3(0, 0, -1);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    cam.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //}
    }

    private void mouseRotation()
    {
        //���콺 �����Ӱ� ī�޶� ������ �ݴ��� ���콺 y => x. x => y
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue.x -= mouseY;
        rotateValue.y += mouseX;

        rotateValue.x = Mathf.Clamp(rotateValue.x, -45, 60);//�߿�

        cam.transform.rotation = Quaternion.Euler(rotateValue);
    }
}
