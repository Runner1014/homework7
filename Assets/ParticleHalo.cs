using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHalo : MonoBehaviour {

    private ParticleSystem particleSys;             // 粒子系统  
    private ParticleSystem.Particle[] particleArr;  // 粒子数组  
    private ParticleCirclePosition[] circle;        // 粒子位置数组  

    private int level = 10; // 粒子不同速度分层数

    public int count = 16000;       // 粒子数量（逆时针10000 ）
    public float size = 0.03f;       // 粒子大小  
    public float minRadius = 4.0f;  // 最小半径（逆时针5.0f）
    public float maxRadius = 9.0f; // 最大半径  
    public bool clockwise = true;   // 顺时针（true）| 逆时针（false）
    public float speed = 2.0f;        // 旋转速度（逆时针1.0f）
    public float pingPong = 0.02f;   // 游离范围

    public Gradient colorGradient;  // 梯度颜色控制器，用来设置粒子颜色透明度

    // Use this for initialization
    void Start()
    {   
        // 初始化粒子数组  
        particleArr = new ParticleSystem.Particle[count];
        circle = new ParticleCirclePosition[count];

        // 初始化粒子系统  
        this.gameObject.AddComponent<ParticleSystem>();
        particleSys = this.GetComponent<ParticleSystem>();
        particleSys.startSpeed = 0;            // 发射时粒子初始速度设为0，其运动由程序控制  
        particleSys.startSize = size;          // 设置粒子大小  
        particleSys.loop = false;
        particleSys.maxParticles = count;      // 设置最大粒子数量  
        particleSys.Emit(count);               // 发射粒子  
        particleSys.GetParticles(particleArr);

        // 初始化梯度颜色控制器，透明度改变，颜色不变
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[7];
        alphaKeys[0].time = 0.0f; alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].time = 0.2f; alphaKeys[1].alpha = 0.8f;
        alphaKeys[2].time = 0.4f; alphaKeys[2].alpha = 0.3f;
        alphaKeys[3].time = 0.6f; alphaKeys[3].alpha = 0.8f;
        alphaKeys[4].time = 0.8f; alphaKeys[4].alpha = 1.0f;
        alphaKeys[5].time = 0.9f; alphaKeys[5].alpha = 0.4f;
        alphaKeys[6].time = 1.0f; alphaKeys[6].alpha = 0.9f;
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].time = 0.0f; colorKeys[0].color = Color.white;
        colorKeys[1].time = 1.0f; colorKeys[1].color = Color.white;
        colorGradient.SetKeys(colorKeys, alphaKeys);

        RandomlySpread();   // 在一定范围内随机分布各粒子位置
    }

    private void RandomlySpread()
    {
        for (int i = 0; i < count; i++)
        {
            // 随机产生每个粒子距离中心的半径，同时使粒子集中在平均半径附近  
            float midRadius = (maxRadius + minRadius) / 2;
            float minRate = UnityEngine.Random.Range(1.0f, midRadius / minRadius);
            float maxRate = UnityEngine.Random.Range(midRadius / maxRadius, 1.0f);
            float radius = UnityEngine.Random.Range(minRadius * minRate, maxRadius * maxRate);

            // 随机产生每个粒子的角度  
            float angle = UnityEngine.Random.Range(0.0f, 360.0f);
            // 转成弧度制
            float radian = angle / 180 * Mathf.PI;

            // 随机产生每个粒子的游离起始时间  
            float time = UnityEngine.Random.Range(0.0f, 360.0f);

            circle[i] = new ParticleCirclePosition(radius, angle, time);
            // 根据弧度制角度确定粒子位置
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(radian), 0f, circle[i].radius * Mathf.Sin(radian));
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            // 角度改变（不同level或不同半径的角度增加量不同）
            float angleChange = (i % level + 1) * (speed / circle[i].radius / level);
            if (clockwise)  // 顺时针旋转  
                circle[i].angle = (circle[i].angle - angleChange + 360.0f) % 360.0f;
            else            // 逆时针旋转  
                circle[i].angle = (circle[i].angle + angleChange + 360.0f) % 360.0f;

            //转为弧度制
            float radian = circle[i].angle / 180 * Mathf.PI;

            // 粒子在半径方向上游离 （在一定范围内来回运动）
            circle[i].time += Time.deltaTime;
            circle[i].radius += Mathf.PingPong(circle[i].time / minRadius / maxRadius, pingPong) - pingPong / 2.0f;

            // 根据半径和角度设置粒子位置
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(radian), 0f, circle[i].radius * Mathf.Sin(radian));

            // 根据粒子的角度改变粒子的透明度
            particleArr[i].startColor = colorGradient.Evaluate(circle[i].angle / 360.0f);
        }

        // 重设粒子系统
        particleSys.SetParticles(particleArr, particleArr.Length);
    }
}
