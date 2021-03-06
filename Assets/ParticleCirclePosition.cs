﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCirclePosition
{
    public float radius = 0f, angle = 0f, time = 0f;
    public ParticleCirclePosition(float radius, float angle, float time)
    {
        this.radius = radius;   // 半径  
        this.angle = angle;     // 角度  
        this.time = time;      // 时间，用于半径上游离
    }
}
