﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace NagareNS
{
    struct NagarePoint
    {
        Vector3 position;
        Vector3 direction;
    }
}

public class Nagare : MonoBehaviour
{
    enum State
    {
        OFF,
        READY,
        ON,
    }

    //公開メンバ
    public float lifeTime = 5.0f;
    //非公開メンバ
    private State state = State.OFF;
    private Vector3 nagareVector;
    private Vector3 nagareDirection;
    private float nagareScalar;

    //メソッド
    public Vector3 GetNagareVector() { return nagareVector = nagareDirection.normalized; }

    // Use this for initialization
    void Start()
    { }

    //生成処理（流れを引いている、まだ流れていない）
    public void Create(Vector3 setDirection, float setPower)
    {
        state = State.READY;
        nagareDirection = setDirection; //向き
        nagareScalar = setPower;        //大きさ
        lifeTime *= setPower;           //寿命（強さ２倍なら寿命二倍）
        //lifeTime *= 1 - setPower;   //強さ２倍なら寿命半分
    }

    //初期化完了（流れを引き終わり、流れ始める）
    public void Activate()
    {
        state = State.ON;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.ON)
        {
            return;
        }

        //寿命
        if (LifeTimer() == false)
        {
            //自殺処理とか
            Destroy(this.gameObject);
            return;
        }

        //流れ弱くなるてきな？
        nagareScalar *= 0.999f;

    }

    //寿命カウント
    bool LifeTimer()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            return false;
        }
        return true;
    }
}
