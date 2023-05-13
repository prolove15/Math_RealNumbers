using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager_Concept3 : MonoBehaviour
{
    
    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    Controller_Concept3 controller_Cp;

    [SerializeField]
    Transform marksPanel_Tf;

    [SerializeField]
    Transform solutionPanel_Tf;

    [SerializeField]
    TMP_Text problemText_Cp;

    [SerializeField]
    TMP_Text evaluateText_Cp;

    [SerializeField]
    TMP_Text evaluatePanelText_Cp;

    [SerializeField]
    TMP_Text nextProblemBtnText_Cp;
    
    [SerializeField]
    Button nextProblemBtn_Cp;

    [SerializeField]
    Button answerBtn_Cp;
    
    [SerializeField]
    InputField answer1InputF_Cp;

    [SerializeField]
    Animator studentPanelAnim_Cp;
    
    [SerializeField]
    Animator infoPanelAnim_Cp;
    
    // Private fields
    List<Animator> markImgAnim_Cps = new List<Animator>();

    List<TMP_Text> solutionText_Cps = new List<TMP_Text>();

    //----------------------------------------------- properties
    Concept3 concept_Cp
    {
        get { return controller_Cp.concept_Cp; }
    }

    // Public properties
    public string problemText
    {
        get { return problemText_Cp.text; }
        set { problemText_Cp.text = value; }
    }

    public string evaluateText
    {
        get { return evaluateText_Cp.text; }
        set { evaluateText_Cp.text = value; }
    }

    public string evaluatePanelText
    {
        get { return evaluatePanelText_Cp.text; }
        set { evaluatePanelText_Cp.text = value; }
    }

    public string nextProblemBtnText
    {
        get { return nextProblemBtnText_Cp.text; }
        set { nextProblemBtnText_Cp.text = value; }
    }

    public bool nextProblemBtnInteract
    {
        get { return nextProblemBtn_Cp.gameObject.activeInHierarchy; }
        set { nextProblemBtn_Cp.gameObject.SetActive(value); }
    }

    public bool answerBtnInteract
    {
        get { return answerBtn_Cp.gameObject.activeInHierarchy; }
        set { answerBtn_Cp.gameObject.SetActive(value); }
    }

    public string answer1InputText
    {
        get { return answer1InputF_Cp.text; }
        set { answer1InputF_Cp.text = value; }
    }

    public string studentPanelAnimTrigger
    {
        set { studentPanelAnim_Cp.SetTrigger(value); }
    }

    public int infoPanelAnimFlag
    {
        get { return infoPanelAnim_Cp.GetInteger("flag"); }
        set { infoPanelAnim_Cp.SetInteger("flag", value); }
    }

    public int markImgCount
    {
        get { return markImgAnim_Cps.Count; }
    }

    public int solutionTextCount
    {
        get { return solutionText_Cps.Count; }
    }

    //----------------------------------------------- methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------- Init
    public void Init()
    {
        // insert markImgAnim_Cps using marksPanel_Tf
        for(int i = 0; i < marksPanel_Tf.childCount; i++)
        {
            markImgAnim_Cps.Add(marksPanel_Tf.GetChild(i).GetComponent<Animator>());
        }

        // insert solutionText_Cps using solutionPanel_Tf
        for(int i = 0; i < solutionPanel_Tf.childCount; i++)
        {
            solutionText_Cps.Add(solutionPanel_Tf.GetChild(i).GetComponent<TMP_Text>());
        }
    }

    //-------------------- Public methods
    // Set mark image animations
    public void SetMarkImageAnimations(int index, string flag)
    {
        // check valid
        if(index < 0 || index >= markImgCount)
        {
            return;
        }
        
        markImgAnim_Cps[index].SetTrigger(flag);
    }

    // Set solution text
    public void SetSolutionText(int index, string solution_pr)
    {
        // check valid
        if(index < 0 || index >= solutionTextCount)
        {
            Debug.Log("SetSolutionText, index is out of range, index = " + index.ToString());
            return;
        }

        solutionText_Cps[index].text = solution_pr;
    }

    // Set active solution text
    public void SetActiveSolutionText(int index, bool flag)
    {
        // check valid
        if(index < 0 || index >= solutionTextCount)
        {
            Debug.Log("SetActiveSolutionText, index is out of range, index = " + index.ToString());
            return;
        }

        solutionText_Cps[index].enabled = flag;
    }

    //-------------------- Callback from UI
    // Called when click answer button
    public void OnClickAnswerButton()
    {
        concept_Cp.OnClickAnswerButton();
    }

    // Called when click next problem button
    public void OnClickNextProblemButton()
    {
        concept_Cp.OnClickNextProblemButton();
    }

    // Called when click info button
    public void OnClickInfoButton()
    {
        concept_Cp.OnClickInfoButton();
    }

    // Called when click shutdown button
    public void OnClickShutdownButton()
    {
        controller_Cp.OnClickShutdownButton();
    }
}
