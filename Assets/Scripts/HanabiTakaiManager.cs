using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HanabiTakaiManager : MonoBehaviour
{
    private static HanabiTakaiManager instance;
    //单例模式
    public static HanabiTakaiManager Instance { get => instance; }

    [Header("烟花大会现场")]
    //烟花
    public Hanabi hanabi;
    //观众
    public Audience audience;
    //烟花最大上升高度
    private Vector3 maxHeight;
    //当前是否正在正常播放烟花
    [SerializeField] private bool isAlways;

    [Header("烟花全开")]
    //全开的时间
    public float allInTime=25f;
    //持续时间
    public float delayTime=25f;
    //全开的数量
    public int allInRate=75;

    //单例模式
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    //被启用的时候开始播放（也就是点击开始后）
    public void StartFireWorks()
    {
        hanabi.GetComponent<VisualEffect>().enabled = true;
        isAlways = true;
        HanabiTakaiListen();
    }


    public void HanabiTakaiListen()
    {
        StartCoroutine(HanabiMax());
        StartCoroutine(HanabiAlways());
    }

    //最大速率，释放最多的烟花
    IEnumerator HanabiMax()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(allInTime);
            //先切换状态，从正常执行变成全速释放
            isAlways = false;
            //先扩大生成范围
            hanabi.SetExplosionCreateRange(new Vector3(Random.Range(-10, -20), 0, Random.Range(0, -1)),
                                            new Vector3(Random.Range(10, 20), 0, Random.Range(-1, -4)));
            //提高最大高度
            SetFlightHeight(new Vector3(0, Random.Range(18, 22), 0), new Vector3(0, Random.Range(29, 32), 0));
            //增加速率和烟花数量
            hanabi.SetRate(allInRate);
            //烟花全速后，等待5s恢复原本的形状
            yield return new WaitForSecondsRealtime(delayTime);
            hanabi.ResetHanabi();
            //重新启用正常执行的状态
            //因为自动更新颜色的协程是每隔10秒才会执行一次的
            SetGradient();
            isAlways = true;
        }
    }

    //烟花正常执行过程中
    IEnumerator HanabiAlways()
    {
        do
        {
            yield return new WaitForSecondsRealtime(3f);
            SetFlightHeight();
            SetGradient();
        } while ((isAlways));
    }

    //设置烟花爆炸时的随机颜色和透明度以及烟花余末的渐变
    public void SetGradient()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(GetGradientColorKeys(), GetGradientAlphaKeys());
        hanabi.SetExplosionGradient(gradient);
        hanabi.SetExplosionTrailGradient(SetExplosionTrailGradient());
    }

    //获取随机的渐变colorkey
    public GradientColorKey[] GetGradientColorKeys()
    {
        //开始和结束的随机颜色，应该不为纯白色或者纯黑色
        var startColor = new Color(Random.Range(1, 254), Random.Range(1, 254), Random.Range(1, 254));
        var endColor = new Color(Random.Range(1, 254), Random.Range(1, 254), Random.Range(1, 254));
        //渐变颜色
        //白色闪光
        GradientColorKey shineColorkey = new GradientColorKey(Color.white, 0);
        GradientColorKey startColorkey = new GradientColorKey(startColor, 0.05f);
        GradientColorKey endColorkey = new GradientColorKey(endColor, 0.85f);
        GradientColorKey destoryColorkey = new GradientColorKey(Color.white, 0.95f);
        GradientColorKey[] colorKeys = new GradientColorKey[] { shineColorkey, startColorkey, endColorkey, destoryColorkey };
        return colorKeys;
    }

    //获取渐变colorkey对应的alphakey
    public GradientAlphaKey[] GetGradientAlphaKeys()
    {
        //渐变透明度
        GradientAlphaKey shineAlphakey = new GradientAlphaKey(0, 0);
        GradientAlphaKey startAlphakey = new GradientAlphaKey(1, 0.05f);
        GradientAlphaKey endAlphakey = new GradientAlphaKey(1, 0.85f);
        GradientAlphaKey destoryAlphakey = new GradientAlphaKey(0, 0.95f);
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[] { shineAlphakey, startAlphakey, endAlphakey, destoryAlphakey };
        return alphaKey;
    }

    //设置烟花余末的渐变效果
    public Gradient SetExplosionTrailGradient()
    {
        Gradient gradient = new Gradient();
        var startColor = new Color(Random.Range(1, 254), Random.Range(1, 254), Random.Range(1, 254));
        var endColor = startColor;
        //颜色键
        GradientColorKey startColorKey = new GradientColorKey(startColor, 0f);
        GradientColorKey endColorKey = new GradientColorKey(endColor, 1f);
        GradientColorKey[] colorkeys = new GradientColorKey[] { startColorKey, endColorKey };
        //渐变键
        GradientAlphaKey startAlphaKey = new GradientAlphaKey(1, 0);
        GradientAlphaKey endAlphaKey = new GradientAlphaKey(0, 1);
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] { startAlphaKey, endAlphaKey };
        gradient.SetKeys(colorkeys, alphaKeys);
        return gradient;
    }

    //烟花正常上升的随机高度
    //方法返回最小，同时out最大
    public Vector3 GetRandomHigh(out Vector3 MaxHeight)
    {
        var minHeight = new Vector3(0, Random.Range(18, 20), 0);
        var maxHeight = new Vector3(0, Random.Range(22, 26), 0);
        MaxHeight = maxHeight;
        return minHeight;
    }

    //调整烟花上升高度(正常情况)
    public void SetFlightHeight()
    {
        var minheight = GetRandomHigh(out maxHeight);
        hanabi.SetHanabiCanArrive(minheight, maxHeight);
    }

    //全速烟花
    public void SetFlightHeight(Vector3 min, Vector3 max)
    {
        hanabi.SetHanabiCanArrive(min, max);
    }
}
