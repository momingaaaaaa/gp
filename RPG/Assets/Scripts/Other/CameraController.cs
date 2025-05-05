using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConttoller : MonoBehaviour
{
    public float zoomSpeed = 10;
    private Vector3 offset;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // 修复标签名称，确保标签为 "Player"。
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 检查 Camera.main 是否为 null，防止 null 引用错误
        if (Camera.main != null)
        {
            Camera.main.fieldOfView += scroll * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 37, 70);
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }
}
