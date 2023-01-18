using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    [Header("观众基础设置")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TakePose();
    }

    //让观众执行伸懒腰的动作
    public void TakePose()
    {
        StartCoroutine(IntervalTakePose());
    }

    private void OnDisable()
    {
        StopCoroutine(IntervalTakePose());
    }

    //使用协程让观众每隔一段时间就执行这个方法
    IEnumerator IntervalTakePose()
    {
        while (true)
        {
            //每隔真实时间10s执行一次
            yield return new WaitForSecondsRealtime(15.0f);
            animator.SetTrigger("Pose");
        }
    }
}
