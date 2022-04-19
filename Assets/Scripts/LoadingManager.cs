using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;

public class LoadingManager : MonoBehaviour
{
    public Slider slider;
    public async void Loading()
    {
        slider.gameObject.SetActive(true);
        slider.transform.DOLocalMoveY(-500, 0.1f);

        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        float timer = 0;
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            await Task.Delay(1);
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(slider.value, op.progress, timer);
                if (slider.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, timer);
                if (Mathf.Approximately(slider.value, 1))
                {
                    op.allowSceneActivation = true;
                    slider.transform.DOLocalMoveY(-1000, 0.1f);
                    await Task.Delay(1000);
                    break;
                }
            }
        }
    }
}
