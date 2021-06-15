using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class StartButtons : MonoBehaviour
{
    public void StartGame()
    {
        GameObject Background = GameObject.Find("Start/Background");
        GameObject Start = GameObject.Find("Start/Start");
        GameObject Quit = GameObject.Find("Start/Quit");
        //GameObject Prompt = GameObject.Find("Start/Prompt");
        Background.SetActive(false);
        Start.SetActive(false);
        Quit.SetActive(false);
        //Prompt.SetActive(true);
        StartCoroutine(Wait1());
        //GameObject StartCanvas = GameObject.Find("Start");
    }
    IEnumerator Wait1()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        GameObject root = GameObject.Find("UI");
        GameObject Prompt = root.transform.Find("Start/Prompt").gameObject;
        Prompt.SetActive(false);
        GameObject GameUI = root.transform.Find("GameUI").gameObject;
        GameUI.SetActive(true);
        GameObject ARCamera = root.transform.Find("ARCamera").gameObject;
        ARCamera.SetActive(true);
        GameObject ImageTarget = root.transform.Find("ImageTarget").gameObject ;
        ImageTarget.SetActive(true);
        GameObject PauseButton = root.transform.Find("Pause/PauseButton").gameObject;
        PauseButton.SetActive(true);
    }

    public void QuitGame()
    {
        StartCoroutine(Wait2());

    }

    IEnumerator Wait2()
    {
        GameObject root = GameObject.Find("UI");
        GameObject Start = root.transform.Find("Start").gameObject;
        Start.SetActive(false);
        GameObject EndBackground = root.transform.Find("End/Background").gameObject;
        EndBackground.SetActive(true);
        yield return new WaitForSecondsRealtime(30f);
        EditorApplication.isPlaying = false;
    }
}