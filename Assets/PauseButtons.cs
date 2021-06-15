using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PauseButtons : MonoBehaviour
{
    //private bool IfPause = false;
    public void PauseButton()
    {
        GameObject root = GameObject.Find("UI");
        GameObject Background = root.transform.Find("Pause/Background").gameObject;
        GameObject ContinueButton = root.transform.Find("Pause/ContinueButton").gameObject;
        GameObject QuitButton = root.transform.Find("Pause/QuitButton").gameObject;
        Background.SetActive(true);
        ContinueButton.SetActive(true);
        QuitButton.SetActive(true);
        SetPause();
    }

    public void ContinueButton()
    {
        GameObject root = GameObject.Find("UI");
        GameObject Background = root.transform.Find("Pause/Background").gameObject;
        GameObject ContinueButton = root.transform.Find("Pause/ContinueButton").gameObject;
        GameObject QuitButton = root.transform.Find("Pause/QuitButton").gameObject;
        Background.SetActive(false);
        ContinueButton.SetActive(false);
        QuitButton.SetActive(false);
        CancelPause();
    }

    public void QuitButton()
    {
        StartCoroutine(Wait());
    }

    public void SetPause()
    {
        Globle.IfPause = true;
    }

    public void CancelPause()
    {
        Globle.IfPause = false;
    }

    public bool GetPause()
    {
        return Globle.IfPause;
    }

    IEnumerator Wait()
    {
        GameObject root = GameObject.Find("UI");
        GameObject EndBackground = root.transform.Find("End/Background").gameObject;
        GameObject Background = root.transform.Find("Pause/Background").gameObject;
        GameObject ContinueButton = root.transform.Find("Pause/ContinueButton").gameObject;
        GameObject QuitButton = root.transform.Find("Pause/QuitButton").gameObject;
        EndBackground.SetActive(true);
        Background.SetActive(false);
        ContinueButton.SetActive(false);
        QuitButton.SetActive(false);
        yield return new WaitForSecondsRealtime(3.0f);
        EditorApplication.isPlaying = false;
    }

}

