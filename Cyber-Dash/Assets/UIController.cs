using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public PlayerControl pc;
    public InputController IC;
    public GameObject PauseUI;
    public GameObject AudioUI;
    private InputAction Pause;

    public event Action <bool>OnPausePressed;

    public bool Paused = false;


    private void OnEnable()
    {
        Pause = pc.UISystem.Pause;
        Pause.Enable();
        OnPausePressed += PauseGame;
    }

    private void OnDisable()
    {
        Pause.Disable();
    }

    private void Awake()
    {
        pc = new PlayerControl();
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioUI.GetComponent<VolumeSettings>().SetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.triggered)
            OnPause();
        if(PauseUI.active)
            AudioUI.GetComponent<VolumeSettings>().Mute(true);
    }

    public void OnPause()
    {
            OnPausePressed?.Invoke(!Paused);
    }

    void PauseGame(bool p)
    {
        Paused = p;
        PauseUI.SetActive(p);
        if (!PauseUI.active)
            AudioUI.GetComponent<VolumeSettings>().Mute(false);
        IC.enabled = !p;
        Time.timeScale = Paused? 0 : 1;
    }
}
