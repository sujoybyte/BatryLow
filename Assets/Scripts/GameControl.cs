using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Animator animWire = null;
    [SerializeField] private GameObject menuUI = null, gameUI = null, helpText = null, aboutText = null;
    [SerializeField] private GameObject buttons = null;
    [SerializeField] private Button playButton = null;
    [SerializeField] private GameObject winner = null, restart = null;

    private void Start()
    {
        animWire.enabled = false;
        menuUI.SetActive(true);
        playButton.interactable = true;
        gameUI.SetActive(false);
        winner.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttons.SetActive(true);
            aboutText.SetActive(false);
        }
    }

    public void Play()
    {
        playButton.interactable = false;
        animWire.enabled = true;
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

    public void Win()
    {
        Debug.Log("bffbrf");
        winner.SetActive(true);
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        restart.SetActive(false);
    }

    public void About()
    {
        animWire.enabled = false;
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
