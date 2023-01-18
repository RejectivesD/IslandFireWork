using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("暂停面板")]
    public GameObject pausePanel;
    bool isPaused;
    //开始按钮
    public GameObject startButton;

    private void Awake() {
        isPaused=false;
        pausePanel.SetActive(isPaused);
        startButton.GetComponent<Button>().onClick.AddListener(StartScene);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused=!isPaused;
            pausePanel.SetActive(isPaused);
            PauseScene();
        }
    }

    private void PauseScene(){
        if(isPaused){
            Time.timeScale=0f;
        }else{
            Time.timeScale=1f;
        }
    }
    
    //暴露给按钮的方法
    public void StartScene(){
        HanabiTakaiManager.Instance.StartFireWorks();
        startButton.SetActive(false);
    }
    //重新加载场景
    public void Resume(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale=1f;
    }
    public void Quit(){
        Application.Quit();
    }
}
