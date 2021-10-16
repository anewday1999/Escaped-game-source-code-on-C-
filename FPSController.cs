using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    public Text fpsText;
     public int target = 60;

    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
    void Start()
    {
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }

IEnumerator displayFps(float timeWait)
    {
        fpsText.text = fps;
        yield return new WaitForSeconds(timeWait);
    }

void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
        StartCoroutine(displayFps(0.5f));
    }
}
