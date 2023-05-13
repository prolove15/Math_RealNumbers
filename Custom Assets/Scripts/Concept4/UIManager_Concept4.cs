using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager_Concept4 : MonoBehaviour
{
    
    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    Controller_Concept4 controller_Cp;

    [SerializeField]
    Transform marksPanel_Tf;

    [SerializeField]
    Transform problemsPanel_Tf;

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

    List<GameObject> problem_GOs = new List<GameObject>();

    //----------------------------------------------- properties

    #region Properties

    Concept4 concept_Cp
    {
        get { return controller_Cp.concept_Cp; }
    }

    // Public properties
    public int problemPanelIndex
    {
        get
        {   
            int value = 0;

            for(int i = 0; i < problem_GOs.Count; i++)
            {
                if(problem_GOs[i].activeInHierarchy)
                {
                    value = i;
                    break;
                }
            }

            return value;
        }
        set
        {
            for(int i = 0; i < problem_GOs.Count; i++)
            {
                problem_GOs[i].SetActive(i == value ? true : false);
            }
        }
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

    #endregion

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

        // insert problem_GOs using problemPanel_Tf
        for(int i = 0; i < problemsPanel_Tf.childCount; i++)
        {
            problem_GOs.Add(problemsPanel_Tf.GetChild(i).gameObject);

            problem_GOs[i].transform.localScale = Vector3.one;
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
