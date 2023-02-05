using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject fadeOut;
    
    void Update()
    {
        if (!Input.anyKey) return;
        var anim = fadeOut.GetComponent<Animator>();
        anim.Play("FadeOut");
        StartCoroutine(GoToIntroScene());
    }

    IEnumerator GoToIntroScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Intro");
    }
}
