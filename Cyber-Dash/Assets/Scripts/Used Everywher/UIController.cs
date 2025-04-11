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
    public GameObject PlayerUI;
    public GameObject AudioUI;
    private InputAction Pause;

    public Scrollbar Instruct;
    private GameObject ExitButotn;

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
        Instruct.onValueChanged.AddListener(ExitInstruct);
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
    }

    public void OnPause()
    {
            OnPausePressed?.Invoke(!Paused);
            AudioUI.GetComponent<VolumeSettings>().Mute(PauseUI.active);
        if(PauseUI.active && !AudioUI.active)
        {
            PauseButton();
        }
    }

    void PauseGame(bool p)
    {
        Paused = p;
        PauseUI.SetActive(p);
        PlayerUI.SetActive(!p);
        if (!PauseUI.active)
            AudioUI.SetActive(false);
        IC.enabled = !p;
        Time.timeScale = Paused? 0 : 1;
    }

    public void AudioSlider()
    {
        if(IC.Controller)
        {
            IC.EV.firstSelectedGameObject = transform.GetChild(1).transform.GetChild(2).transform.GetChild(3).gameObject;
            IC.EV.firstSelectedGameObject.GetComponent<Slider>().Select();
        }
    }

    public void GetNewButton(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
        }
    }

    public void GetNewSlider(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Slider>().Select();
        }
    }

    public void GetNewSCrollBar(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Scrollbar>().Select();
        }
    }

    public void PauseButton()
    {
            if (IC.Controller)
            {
                IC.EV.firstSelectedGameObject = transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject;
                IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
            }
        
    }

    public void exitB(GameObject g)
    {
        ExitButotn = g;
    }

    public void ExitInstruct(float f)
    {
        ExitButotn.SetActive(f < .05f ? true : false);
    }

}
