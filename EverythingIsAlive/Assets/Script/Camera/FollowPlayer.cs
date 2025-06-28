using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    //玩家对象
    public GameObject Player;
    //背景对象（框定范围）
    public GameObject Bg;
    //相机宽度
    private float cameraWidth;
    private float minX;
    private float maxX;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // 获取背景的边界
        Bounds bounds = Bg.GetComponent<SpriteRenderer>().bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        //Debug.Log("minX: " + minX);
        //Debug.Log("maxX: " + maxX);
        // 获取相机尺寸
        float cameraHeight = GetComponent<Camera>().orthographicSize * 2;
        cameraWidth = cameraHeight * GetComponent<Camera>().aspect;
        //Debug.Log("cameraWidth"+cameraWidth);
        // 初始位置设置为中间或玩家位置
        transform.position = new Vector3(
            Player.transform.position.x,
            transform.position.y,
            transform.position.z
        );

    }

    // Update is called once per frame
    void Update()
    {
        // 获取当前玩家X位置，Y不变
        float targetX = Player.transform.position.x;

        // 计算相机半宽
        float halfCameraWidth = cameraWidth / 2;

        // 限制相机X轴位置，使其不超出背景边界
        float clampedX = Mathf.Clamp(targetX, minX + halfCameraWidth, maxX - halfCameraWidth);

        // 只更新X轴位置
        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            transform.position.z
        );
    }
}
