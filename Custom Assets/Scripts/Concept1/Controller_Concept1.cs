using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Concept1 : MonoBehaviour
{

    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    BgdManager bgdManager_Cp;

    [SerializeField]
    public UIManager_Concept1 uiManager_Cp;

    [SerializeField]
    public Concept1 concept_Cp;

    //----------------------------------------------- properties

    //----------------------------------------------- methods
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Init
    void Init()
    {
        bgdManager_Cp.Init();

        uiManager_Cp.Init();

        concept_Cp.Init();

        ReadyToPlay();
    }

    // Ready to play
    void ReadyToPlay()
    {
        StartCoroutine(CorouReadyToPlay());
    }

    IEnumerator CorouReadyToPlay()
    {
        bgdManager_Cp.CurtainUp();

        yield return new WaitForSeconds(0.5f);

        Play();
    }

    // Play
    void Play()
    {
        
    }

    // Quit
    void Quit()
    {
        StartCoroutine(CorouQuit());
    }

    IEnumerator CorouQuit()
    {
        bgdManager_Cp.CurtainDown();

        yield return new WaitForSeconds(0.5f);

        Application.Quit();
        yield break;
    }

    //-------------------- Callback from UI
    public void OnClickShutdownButton()
    {
        Quit();
    }
}
