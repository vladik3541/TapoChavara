using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsView : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public int fpsWidth = 60;
    private float deltaTime;
    private void Awake()
    {
        Application.targetFrameRate = fpsWidth;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFpsWidth(int index)
    {
        switch(index)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 90;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
        }
    }
}
