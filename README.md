# homework7
**选择作业： 参考 http://i-remember.fr/en 这类网站，使用粒子流编程控制制作一些效果， 如“粒子光环”**
* 说明：  
需在项目创建两个空对象，位置都为 (0, 0, 0)，将ParticleHalo.cs脚本拖到两个对象，并在右侧面板中将其中一个对象的脚本参数修改如下：  
count = 10000; minRadius = 5.0f; clockwise = false; speed = 1.0f;  
另外，要将摄像机的Position设为(0, 20, 0)，Rotation设为 (90, 0, 0)；  
运行后，将组件 ParticleSystem 的 Renderer 属性中的 Material 设为 Default-Particle ，把游戏界面放最大看效果更加。
* 设计过程

  * 在一个圆环内随机分布一定数量的粒子；
  * 给粒子设置不同的旋转速度；
  * 让粒子在半径方向上游离，显得更加自然真实；
  * 设置透明度渐变，即光效；
  * 加两层旋转，分别为顺时针和逆时针

* 效果视频：  （不清晰，无法看到真实效果）
http://v.youku.com/v_show/id_XMzYzMTYyODQ1Ng.html?spm=a2h0j.11185381.listitem_page1.5~A
