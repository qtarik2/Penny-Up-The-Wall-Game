using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public int sceneIndex;
    public float transitionTime = 1f;

    public Animator anim;
    public Image logo;
    public Slider loadingSlider;
    public Text percentageSlider;
    public Image insLogo;
    public List<GameObject> toDes;

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            anim.Play("LoaderExit");
            if (percentageSlider != null)
                return;
            StartCoroutine(parentLogo());
        }
        else { StartCoroutine(LoadNextLevel()); }
    }

    IEnumerator LoadNextLevel()
    {
        anim.Play("LoaderEntry");

        yield return new WaitForSeconds(transitionTime);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingSlider.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = Mathf.Lerp(loadingSlider.value,progress,Mathf.Sin(0.1f));
            float fois = progress * 100;
            string per = fois.ToString("F0");
            percentageSlider.text = per + "%";

            yield return null;
        }
    }

    IEnumerator parentLogo()
    {
        yield return new WaitForSeconds(1f);
        Destroy(anim);
        toDes.ForEach(d => Destroy(d));
        logo.enabled = true;
    }

}
