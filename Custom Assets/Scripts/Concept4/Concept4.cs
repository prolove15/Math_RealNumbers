using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concept4 : MonoBehaviour
{
    
    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    Controller_Concept4 controller_Cp;

    [SerializeField]
    int[] solutions;

    // Private fields
    List<int> randOrders = new List<int>();

    int m_problemIndex;

    bool answerCorrect;

    //----------------------------------------------- properties
    #region PrivateProperties

    UIManager_Concept4 uiManager_Cp
    {
        get { return controller_Cp.uiManager_Cp; }
    }

    int problemPanelIndex
    {
        get { return uiManager_Cp.problemPanelIndex; }
        set { uiManager_Cp.problemPanelIndex = value; }
    }

    string evaluateText
    {
        get { return uiManager_Cp.evaluateText; }
        set { uiManager_Cp.evaluateText = value; }
    }

    string evaluatePanelText
    {
        get { return uiManager_Cp.evaluatePanelText; }
        set { uiManager_Cp.evaluatePanelText = value; }
    }

    string nextProblemBtnText
    {
        get { return uiManager_Cp.nextProblemBtnText; }
        set { uiManager_Cp.nextProblemBtnText = value; }
    }

    bool nextProblemBtnInteract
    {
        get { return uiManager_Cp.nextProblemBtnInteract; }
        set { uiManager_Cp.nextProblemBtnInteract = value; }
    }

    bool answerBtnInteract
    {
        get { return uiManager_Cp.answerBtnInteract; }
        set { uiManager_Cp.answerBtnInteract = value; }
    }

    string answer1InputText
    {
        get { return uiManager_Cp.answer1InputText; }
        set { uiManager_Cp.answer1InputText = value; }
    }

    string studentPanelAnimTrigger
    {
        set { uiManager_Cp.studentPanelAnimTrigger = value; }
    }

    int infoPanelAnimFlag
    {
        get { return uiManager_Cp.infoPanelAnimFlag; }
        set { uiManager_Cp.infoPanelAnimFlag = value; }
    }

    int markImgCount
    {
        get { return uiManager_Cp.markImgCount; }
    }

    int problemIndex
    {
        get { return m_problemIndex; }
        set { m_problemIndex = value; }
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
        SubmitProblem(-1);
    }

    //-------------------- submit
    #region Submit

    void SubmitProblem(int problemIndex_pr)
    {
        // check problem index
        if(problemIndex_pr == -1)
        {
            GenerateProblemOrders();

            SetMarkImageAnimations(problemIndex_pr, "Hidden");
        }

        problemIndex = problemIndex_pr + 1;
        if(problemIndex == markImgCount)
        {
            GenerateProblemOrders();

            problemIndex = 0;

            SetMarkImageAnimations(-1, "Hidden");
        }

        if(problemIndex == markImgCount - 1)
        {
            nextProblemBtnText = "Restart";
        }
        else
        {
            nextProblemBtnText = "Next problem";
        }
        
        // submit problem
        SetProblemPanelIndex();

        answer1InputText = string.Empty;
    
        studentPanelAnimTrigger = "Think";

        answerBtnInteract = true;

        nextProblemBtnInteract = false;
        
        evaluateText = "Which is irrational number?";

        evaluatePanelText = "";

        answerCorrect = true;
    }

    #endregion

    //-------------------- private methods
    // Generate random problem orders
    void GenerateProblemOrders()
    {
        // 
        List<int> oldIndexArray_tp = new List<int>(new int[]{0, 1, 2, 3, 4, 5, 6});
        List<int> newIndexArray_tp = new List<int>();
        while(oldIndexArray_tp.Count > 0)
        {
            int randIndex_tp = Random.Range(0, oldIndexArray_tp.Count);

            newIndexArray_tp.Add(oldIndexArray_tp[randIndex_tp]);
            oldIndexArray_tp.RemoveAt(randIndex_tp);
        }

        // 
        randOrders.Clear();
        for(int i = 0; i < markImgCount; i++)
        {
            randOrders.Insert(i, newIndexArray_tp[i]);
        }

    }

    // Set problem panel index
    void SetProblemPanelIndex()
    {
        int value = randOrders[problemIndex];

        problemPanelIndex = value;
    }

    // Set mark image animations
    void SetMarkImageAnimations(int index, string flag)
    {
        for(int i = index + 1; i < markImgCount; i++)
        {
            uiManager_Cp.SetMarkImageAnimations(i, "Hidden");
        }

        uiManager_Cp.SetMarkImageAnimations(index, flag);
    }

    // Check answer
    void CheckAnswer(int value_pr)
    {
        // check answer is right or wrong
        bool result = true;
        if(value_pr != solutions[randOrders[problemIndex]])
        {
            result = false;
        }

        if(result)
        {
            ActionProblem();
        }
        else
        {
            ActionAnswerFalse();
        }
    }
    
    #region Action

    // Action when solution is false
    void ActionAnswerFalse()
    {
        StartCoroutine(CorouActionAnswerFalse());
    }

    IEnumerator CorouActionAnswerFalse()
    {
        studentPanelAnimTrigger = "False";

        evaluateText = "You are incorrect";

        evaluatePanelText = "Please try again";

        answerCorrect = false;

        answerBtnInteract = false;

        yield return new WaitForSeconds(1.5f);

        studentPanelAnimTrigger = "Think";

        answer1InputText = string.Empty;
    
        answerBtnInteract = true;
    }

    // Action when problem is true/false
    void ActionProblem()
    {
        if(answerCorrect)
        {
            ActionProblemTrue();
        }
        else
        {
            ActionProblemFalse();
        }
    }

    // Action when problem is true
    void ActionProblemTrue()
    {
        studentPanelAnimTrigger = "True";

        evaluateText = "You are correct.";

        evaluatePanelText = "Wonderful!";

        answerBtnInteract = false;

        nextProblemBtnInteract = true;
    
        SetMarkImageAnimations(problemIndex, "True");
    }

    // Action when problem is false
    void ActionProblemFalse()
    {
        studentPanelAnimTrigger = "True";

        evaluateText = "You are correct";

        evaluatePanelText = "Mediocre!";

        answerBtnInteract = false;

        nextProblemBtnInteract = true;
    
        SetMarkImageAnimations(problemIndex, "False");
    }

    #endregion

    //-------------------- Callback from UI
    // Called when click answer button
    public void OnClickAnswerButton()
    {
        int value_tp = 0;

        int.TryParse(answer1InputText, out value_tp);

        CheckAnswer(value_tp);
    }

    // Called when click next problem button
    public void OnClickNextProblemButton()
    {
        SubmitProblem(problemIndex);
    }

    // Called when click info button
    public void OnClickInfoButton()
    {
        if(infoPanelAnimFlag == 0 || infoPanelAnimFlag == 1)
        {
            infoPanelAnimFlag = -1;
        }
        else
        {
            infoPanelAnimFlag = 1;
        }
    }

}
