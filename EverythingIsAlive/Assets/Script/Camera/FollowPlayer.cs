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
    private float cameraHeight;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    
    // Start is called before the first frame update
    void Start()
    {
        // 获取背景的边界
        Bounds bounds = Bg.GetComponent<SpriteRenderer>().bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        minY=bounds.min.y;
        maxY=bounds.max.y;
        
        // 获取相机尺寸
        cameraHeight = GetComponent<Camera>().orthographicSize * 2;
        cameraWidth = cameraHeight * GetComponent<Camera>().aspect;
        
        // 初始位置设置为中间或玩家位置
        transform.position = new Vector3(
            Player.transform.position.x,
            Player.transform.position.y,
            transform.position.z
        );

    }

    // Update is called once per frame
    void Update()
    {
        // 获取当前玩家X位置，Y不变
        float targetX = Player.transform.position.x;
        float targetY = Player.transform.position.y;

        // 计算相机半宽
        float halfCameraWidth = cameraWidth / 2;
        float halfCameraHeight = cameraHeight / 2;
        
        float clampedX = Mathf.Clamp(targetX, minX + halfCameraWidth, maxX - halfCameraWidth);
        float clampedY = Mathf.Clamp(targetY, minY + halfCameraHeight, maxY - halfCameraHeight);
        
        transform.position = new Vector3(
            clampedX,
            clampedY,
            transform.position.z
        );
    }
}
