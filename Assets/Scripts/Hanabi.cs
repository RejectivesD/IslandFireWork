using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Object = UnityEngine.Object;

public class Hanabi : MonoBehaviour
{
    public VisualEffect visualEffect;

    [Header("烟花显示")]
    [SerializeField] private bool isVisual = true;
    //properties，方便外部获取
    public bool IsVisual
    {
        get => isVisual;
        set => isVisual = value;
    }
    [Space]
    #region VFX暴露的参数名
    [Header("参数名字")]
    private string _rateName = "Hanabi count";
    private string _explosionGradientName = "explosion gradient";
    private string _explosionGradientTrailName = "explosion trail";
    private string _explosionMinSpeed = "explosion minSpeed";
    private string _explosionMaxSpeed = "explosion maxSpeed";
    private string _createStartRange = "CreateStartRange";
    private string _createEndRange = "CreateEndRange";
    private string _hanabiMinHeight = "HanabiMinHeight";
    private string _hanabiMaxHeight = "HanabiMaxHeight";
    private string _explosionTexture="explosionType";
    #endregion

    #region 最初的烟花参数数值
    [Header("最初参数数值")]
    [SerializeField] private int _initHanabiRate;
    public int InitHanabiRate => _initHanabiRate;
    [SerializeField] private Gradient _initExplosionGradient;
    public Gradient InitExplosionGradient => _initExplosionGradient;
    [SerializeField] private Gradient _initExplosionTrailGradient;
    public Gradient InitExplosionTrailGradient => _initExplosionTrailGradient;
    [SerializeField] float initExplosionMinSpeed;
    public float InitExplosionMinSpeed => initExplosionMinSpeed;
    [SerializeField] float initExplosionMaxSpeed;
    public float InitExplosionMaxSpeed => initExplosionMaxSpeed;
    [SerializeField] Vector3 initcreateStartRange;
    public Vector3 InitcreateStartRange => initcreateStartRange;
    [SerializeField] Vector3 initcreateEndRange;
    public Vector3 InitcreateEndRange => initcreateEndRange;
    [SerializeField] Vector3 inithanabiMinHeight;
    public Vector3 InithanabiMinHeight => inithanabiMinHeight;
    [SerializeField] Vector3 inithanabiMaxHeight;
    public Vector3 InithanabiMaxHeight => inithanabiMaxHeight;
    [SerializeField]Texture2D initExplosionTexture;
    public Texture2D ExplosionTexture=>initExplosionTexture;
    #endregion

    private void Awake()
    {
        //获取最初的速率，爆炸颜色、爆炸拖尾颜色、爆炸范围、生成范围，烟花上升高度
        _initHanabiRate = visualEffect.GetInt(_rateName);
        _initExplosionGradient = visualEffect.GetGradient(_explosionGradientName);
        _initExplosionTrailGradient = visualEffect.GetGradient(_explosionGradientTrailName);
        //爆炸范围
        initExplosionMinSpeed = visualEffect.GetFloat(_explosionMinSpeed);
        initExplosionMaxSpeed = visualEffect.GetFloat(_explosionMaxSpeed);
        //生成区域（x轴）
        initcreateStartRange = visualEffect.GetVector3(_createStartRange);
        initcreateEndRange = visualEffect.GetVector3(_createEndRange);
        //烟花上升距离
        inithanabiMinHeight = visualEffect.GetVector3(_hanabiMinHeight);
        inithanabiMaxHeight = visualEffect.GetVector3(_hanabiMaxHeight);
        //爆炸后小烟花的形状
        initExplosionTexture=(Texture2D)visualEffect.GetTexture(_explosionTexture);
        DontDestroyOnLoad(this);
    }

    #region 外部设置烟花的参数
    //设置速率
    public void SetRate(int rate)
    {
        visualEffect.SetInt(_rateName, rate);
    }
    //设置爆炸时的渐变
    public void SetExplosionGradient(Gradient explosion)
    {
        visualEffect.SetGradient(_explosionGradientName, explosion);
    }
    //小烟花渐变
    public void SetExplosionTrailGradient(Gradient trail)
    {
        visualEffect.SetGradient(_explosionGradientTrailName, trail);
    }
    //爆炸范围
    public void SetExplosionSpeed(float minSpeed, float maxSpeed)
    {
        visualEffect.SetFloat(_explosionMinSpeed, minSpeed);
        visualEffect.SetFloat(_explosionMaxSpeed, maxSpeed);
    }

    //生成范围（沿x轴），可以调整
    public void SetExplosionCreateRange(Vector3 startX, Vector3 endX)
    {
        visualEffect.SetVector3(_createStartRange, startX);
        visualEffect.SetVector3(_createEndRange, endX);
    }

    //烟花上升距离(设置y轴)
    public void SetHanabiCanArrive(Vector3 minHeights, Vector3 maxHeights)
    {
        visualEffect.SetVector3(_hanabiMinHeight, minHeights);
        visualEffect.SetVector3(_hanabiMaxHeight, maxHeights);
    }
    #endregion
    
    #region 重置烟花
    public void ResetHanabi(){
        SetRate(InitHanabiRate);
        SetExplosionGradient(InitExplosionGradient);
        SetExplosionTrailGradient(InitExplosionTrailGradient);
        SetExplosionSpeed(InitExplosionMinSpeed,InitExplosionMaxSpeed);
        SetExplosionCreateRange(InitcreateStartRange,InitcreateEndRange);
        SetHanabiCanArrive(InithanabiMinHeight,InithanabiMaxHeight);
    }
    #endregion
}
