### JTW 2D平台跳跃游戏作业报告

高治铭 521021910056

##### 1.玩法改动

原来的玩法设计过于依赖美术资源，重在通过roguelike机制设计给玩家提供乐趣，但是本游戏体量较小，关卡数较少，无法展现出机制改变对游戏本身的增益，所以最终作品重点在关卡内的游戏设计；



##### 2.玩法设计

本作包含了3个关卡和一个shop，玩家可以自由选择游玩顺序或者重复游玩，3个关卡分别为Dark_scene，Bright_scene和Reward，Reward关卡较为简单，击败敌人可以获得金币，以帮助玩家积攒金币；

玩家每次进入关卡，生命值都为20，结束关卡后生命值恢复到20；

玩家首次进入关卡会获得初始值10金币，玩家通过跳跃收集金币，但是金币是可以积攒的，每次通关后获得的金币保留，玩家可以通过关卡选择界面选择购买狙击枪，并在关卡内使用；

Dark_scene设计在地牢中，光线较为阴暗，原计划在此部分中完成的2D Lighting，但是经过尝试URP渲染管线，程序出现诸多bug，而且本作中物品较多使用后需要调整材质，过于繁琐，故舍弃；

Bright_scene一半在土地中，一半在草地上，并设计有宝箱，可以使玩家获取更多金币；

Reward是奖励关卡，在天空背景下，关卡内有很多蝙蝠上下飞舞，玩家会跳得更高，击败蝙蝠后获得金币（主要是提供给购买狙击枪后的玩家，以让其获得射击敌人的快感）；

关卡选择界面，玩家通过平台跳动选择传送门，进入对应关卡，也可以拾取狙击枪，花费20金币购买狙击枪，显著提高战斗力和射击快感；



##### 3.敌人设计：

（1）父类 Enemy（见 Enemy.cs）所有Enemy都应该继承自此类

在此部分中，Enemy实现了受伤红闪效果，和受伤流血粒子效果；

红闪效果通过对enemy的SpriteRenderer设置ResetColor为红色，并且使用Invoke函数使得红闪触发一段时间

```
void FlashColor(float time)
{
    sr.color = Color.red;
    Invoke("ResetColor", time);
}
```

受伤流血粒子效果：

在Unity中设置particle system为BloodEffect,在被击中时调用该函数

```
Instantiate(bloodEffect, transform.position, Quaternion.identity);
```

<img src="C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326231631390.png" alt="image-20240326231631390" style="zoom:50%;" />

（2）Demon Bat and Ghost

这两个敌人的特点是自动在一定范围内随机巡逻，首先设置左下角位置和右上角位置，在这两个位置中获得随机位置

```
Vector2 GetRandomPos()
{
    Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
    return rndPos;
}
```

然后通过MoveTowards控制敌人的移动，设置waitTime为在一个点的停留时间，该停留时间减为0后，前往下一个位置



![image-20240326221544135](C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326221544135.png)

```
transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
{
    if (waitTime <= 0)
    {
        movePos.position = GetRandomPos();
        waitTime = startWaitTime;
    }
    else
    {
        waitTime -= Time.deltaTime;
    }
}
```

Bat在此基础上增加了掉落金币功能，即在Bat死亡后增加一句触发Coin

```
Instantiate(coin, transform.position, Quaternion.identity);
```

（3）Beast

这个敌人设计为守护金币的敌人，玩家一旦拾取Beast下方的Coin,会触发Beast的Burning动画，化成一个火柱，对玩家造成伤害

![image-20240326233836109](C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326233836109.png)



##### 4.道具设计

（1）Coin         ![image-20240326234520505](C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326234520505.png)

Coin设计为金币，会原地转动，Player靠近时会自动拾取，并在左上角的UI中加一，并触发拾取音效，Coin会用来购买狙击枪，Coin被拾取的判断逻辑为检查碰撞体的Tag和碰撞体的类型名字

```
if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
```

通过在传送门触发时实现以下逻辑来维护Coincount在不同关卡的数值

```
PlayerPrefs.GetInt("CoinCount")
PlayerPrefs.SetInt("CoinCount")
```

（2）Magic       <img src="C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326234540786.png" alt="image-20240326234540786" style="zoom:67%;" />

Magic设计为回血，触发回血音效，使玩家恢复一定的数值

（3）Treasure   <img src="C:\Users\26439\AppData\Roaming\Typora\typora-user-images\image-20240326234749505.png" alt="image-20240326234749505" style="zoom:80%;" />

Treasure设计为宝箱，玩家靠近时宝箱会自动弹开，并掉落三个金币，仍然使用Instantiate函数来实现

（4）Fan and Spikes

Fan（风扇：玩家在该道具上获得更大起跳速度）以及Spikes(地刺：玩家触碰受到伤害)的功能和实现与HW2中相似，这里不再赘述；

（5）Door

传送门，通过SceneManager来控制触发时加载到其他场景



##### 5.角色设计

（1）常规动作

```

```

角色常规的左右移动和跳跃不再赘述，碰到Enemy触发受伤动画，对于玩家平A的攻击框，通过协程的方式来控制其出现时间和持续时间

```
IEnumerator startHitBox()
{
    yield return new WaitForSeconds(starttime);
    poly.enabled = true;
    StartCoroutine(disableHitBox());
}
IEnumerator disableHitBox()
{
    yield return new WaitForSeconds(time);
    poly.enabled = false;
}
```

（2）狙击枪

玩家在购买狙击枪之后才能使用狙击枪，按G展示，按H隐藏，通过SetActive（）实现，否则无法触发枪击效果；玩家移动鼠标，枪会根据玩家鼠标位置旋转来进行瞄准，先将屏幕坐标转换为世界坐标，在通过X，Y坐标差计算arctan值，然后控制Gun旋转对应角度；

```
mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
gunDirection = (mousePos - transform.position).normalized;
float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
transform.eulerAngles = new Vector3(0, 0, angle);
```

点击鼠标左键触发子弹以及设计音效，通过子弹飞行距离来控制子弹的有效射程；



##### 6.UI、相机及场景设计

（1）Level-关卡选择界面

这部分采用了视差滚动的效果，通过控制远景和中景的不同移动速度，来形成视差的效果

```
farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
middleBackground.position += new Vector3(amountToMove.x * 0.5f, amountToMove.y * 0.5f, 0f);
```

狙击枪拾取通过触发时将Buymenu.SetActive设置为true来展示购买页面，暂停页面的设置类似；

（2）血量条设置

通过将血量条的image设置为Filled,来实现按百分比来显示血条

```
healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;
healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
```

（3）相机跟随和限制移动

将Player位置设置为target,即相机位置，通过Clamp函数来使相机位置被控制在一定范围内移动，而不超出画面，通过Lerp()线性插值来实现位置间的平滑移动。

```
    targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
   targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
    transform.position = Vector3.Lerp(transform.position, targetPos, smooth);//线性插值
```



##### 7.问题和解决

（1）角色卡墙问题

角色在跳跃时处在平台边缘，会因为摩擦力卡在平台边缘上难以移动；

经查阅，常见解决方法有3种：

A.为角色和平台设置Physical Material 2D,减小Friciton来达到减小摩擦力的效果，但经测验效果不佳，仍然会卡墙；

B.将平台设置为多边形来解决直角卡墙问题，但考虑到本项目的Prefab已经制作好，重新更改工作量较大，不采纳；

C.将Box矩形导圆角，来避免卡在直角边上，这种方法操作较简单，最终采纳，效果尚可；

（2）狙击枪展示问题

开始时直接将Gun.cs脚本设计在Gun上，此时设计Gun的SetAcive时，发现可以将本来展示的Gun隐藏，但无法将其再展示

查找后，发现将其变为子对象，然后将脚本add在父对象上可以解决该问题，所以最终实现上有一个hold对象。



##### 8.参考资料和美术资源

参考教程：

1.[Unity 视差效果与无限地图_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV1Jz4y1u7qC/?spm_id_from=333.337.search-card.all.click&vd_source=1973213f4b9a70f83b4067856a95dbb1)

2.[【Unity 2D游戏开发教程】第46课 如何在Unity中实现鼠标点击发射子弹功能 Shooting by Mouse Click_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV17a4y177Fk/?spm_id_from=333.999.0.0&vd_source=1973213f4b9a70f83b4067856a95dbb1)

3.[Unity2d人物跳跃卡墙问题【unity3d吧】_百度贴吧 (baidu.com)](https://tieba.baidu.com/p/6017739613)

4.[【Unity】场景切换保留数据的解决方案_unity切换场景后保留原场景数据-CSDN博客](https://blog.csdn.net/BrosyveryOK/article/details/125650392)

美术资源

sprite：

[Download the latest indie games - itch.io](https://itch.io/) elementals_wind_hashhashin_FREE_v1.1

enemy及场景:

1.[Download the latest indie games - itch.io](https://itch.io/) Monster_Creatures_Fantasy(Version 1.3)

2.[Download the latest indie games - itch.io](https://itch.io/) EVil Wizard 2

BGM及音效:

1.[Download the latest indie games - itch.io](https://itch.io/) 16-Bit Starter Pack

2.Download the latest indie games - itch.io The Devil Tower

