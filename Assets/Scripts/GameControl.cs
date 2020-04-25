using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Animator anim;
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject helpText;
    public GameObject buttons;
    public GameObject aboutText;
    public GameObject winner;
    public GameObject restart;

    void Start()
    {
        anim.enabled = false;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        winner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttons.SetActive(true);
            aboutText.SetActive(false);
        }
    }

    public void Play()
    {
        anim.enabled = true;
        winner.SetActive(false);
        restart.SetActive(false);
        StartCoroutine(SwitchCanvas());
    }

    private IEnumerator SwitchCanvas()
    {
        yield return new WaitForSeconds(1.2f);
        gameUI.SetActive(true);
        menuUI.SetActive(false);
        helpText.SetActive(true);
    }

    public void About()
    {
        anim.enabled = false;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        winner.SetActive(false);
        restart.SetActive(false);
        buttons.SetActive(false);
        aboutText.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
