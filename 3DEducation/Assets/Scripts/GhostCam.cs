using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    //W앞 A죄 S뒤 D우
    //C키: 아래로 스페이스키: 위
    //Mouse움직임

    [SerializeField] float moveSpeed = 5f;//이동속도
    [SerializeField] float mouseSensitivity = 100f;//마우스 감도
    private Vector3 rotateValue;//마우스 회전값

    Camera cam;

    void Start()
    {
        cam = Camera.main;//싱글톤을 통해서 변수안에 입력
    }

    void Update()
    {
        moveing();
        mouseRotation();
    }

    //x30 + 30 
    //쿼터니언 * 쿼터니언 = 덧셈이 되게됨

    private void moveing()
    {
        //3d에서 이동하는 방법 //바라보는 각도에 따른 움직임
        if (Input.GetKey(KeyCode.W))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.forward * moveSpeed * Time.deltaTime;//중요 공식 기억하기
            cam.transform.position += cam.transform.forward * moveSpeed * Time.deltaTime;
            //cam.transform.position += Vector3.forward * cam.transform.rotation * moveSpeed * Time.deltaTime;
            //위에 처럼 Vector를 먼저 더하면 에러가 발생됨
            //Quaternion value = cam.transform.rotation * cam.transform.rotation;
            //요약: transform.position += new Vector3(0, 0, 1);

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
            //요약: transform.position += new Vector3(0, 0, -1);
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

        //3d에서 이동하는 방법 //월드포지션에 따른 움직임
        //if (Input.GetKey(KeyCode.W))
        //{
        //    cam.transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        //    Quaternion value = cam.transform.rotation * cam.transform.rotation;
        //    요약: transform.position += new Vector3(0, 0, 1);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    cam.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    cam.transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        //    요약: transform.position += new Vector3(0, 0, -1);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    cam.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //}
    }

    private void mouseRotation()
    {
        //마우스 움직임과 카메라 각도는 반대임 마우스 y => x. x => y
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue.x -= mouseY;
        rotateValue.y += mouseX;

        rotateValue.x = Mathf.Clamp(rotateValue.x, -45, 60);//중요

        cam.transform.rotation = Quaternion.Euler(rotateValue);
    }
}
