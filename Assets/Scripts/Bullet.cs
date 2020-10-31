using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        // 设置两秒钟之后销毁子弹对象
        Destroy(gameObject, 2);
    }

    // 爆炸的方法
    void OnTriggerEnter2D(Collider2D col)
    {
        // 如果子弹碰撞到敌人
        if (col.tag == "Player")
        {
            // 找到敌人对应的脚本，调用其Hurt方法，敌人掉血
            col.gameObject.GetComponent<PlayerMove>().Hurt(10);

            // 调用爆炸方法
            Destroy(gameObject);


            // 销毁当前的子弹对象
            
        }
        // 如果碰撞到补充物体，BombPickup
        else if (col.tag == "Ground")
        {

            // 销毁碰撞的物体对象
            Destroy(gameObject);

            // 销毁当前的子弹对象
            
        }
    }
}