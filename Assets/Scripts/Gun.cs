using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;                // 子弹 Prefab
    public float speed = 20f;
    private float time = 0;// 子弹飞行速度


    private PlayerMove playerCtrl;        // 玩家控制脚本
    public Transform player;// 动画管理器


    void Awake()
    {
        // 获取对象根目录（也就是对顶层的对象）的动画管理器和玩家控制脚本
        playerCtrl = transform.root.GetComponent<PlayerMove>();
    }


    void Update()
    {
        time += Time.deltaTime;
        // 开火按钮按下
        if (Input.GetKeyDown(KeyCode.J) && time>=0.8f)
        {
            // 播放音频
            GetComponent<AudioSource>().Play();

            // 玩家对象面向右边
            if (player.transform.localScale.x > 0)
            {
                // 初始化子弹
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                // velocity直接给物体一个固定的移动速度
                bulletInstance.velocity = new Vector2(speed, 0);
            }
            else
            {
                // 子弹向左边旋转 
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-speed, 0);
            }
            time = 0;
        }
    }
}