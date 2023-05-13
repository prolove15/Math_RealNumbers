using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concept1 : MonoBehaviour
{

    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    Controller_Concept1 controller_Cp;

    // Private fields
    int a, b, n1, n2;

    int m_problemIndex;

    int m_solutionIndex;

    bool answerCorrect;

    //----------------------------------------------- properties
    #region PrivateProperties

    UIManager_Concept1 uiManager_Cp
    {
        get { return controller_Cp.uiManager_Cp; }
    }

    string problemText
    {
        get { return uiManager_Cp.problemText; }
        set { uiManager_Cp.problemText = value; }
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

    string answer2InputText
    {
        get { return uiManager_Cp.answer2InputText; }
        set { uiManager_Cp.answer2InputText = value; }
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

    int solutionTextCount
    {
        get { return uiManager_Cp.solutionTextCount; }
    }

    int markImgCount
    {
        get { return uiManager_Cp.markImgCount; }
    }

    int problemIndex
    {
        get { return m_problemIndex; }
        set
        {
            m_problemIndex = value;
        }
    }

    int solutionIndex
    {
        get { return m_solutionIndex; }
        set
        {
            m_solutionIndex = value;
        }
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
            SetMarkImageAnimations(problemIndex_pr, "Hidden");
        }

        problemIndex = problemIndex_pr + 1;
        if(problemIndex == markImgCount)
        {
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
        GenerateRandomProblem(out a, out b);

        n1 = a;
        n2 = b;

        SetProblemText(a, b);

        answer1InputText = string.Empty;
        answer2InputText = string.Empty;
    
        studentPanelAnimTrigger = "Think";

        answerBtnInteract = true;

        nextProblemBtnInteract = false;
        
        evaluateText = "Answer the value of n1 and n2.";

        evaluatePanelText = "";

        answerCorrect = true;

        SubmitSolution(-1);
    }

    // Submit solution
    void SubmitSolution(int curSolutionIndex)
    {
        solutionIndex = curSolutionIndex + 1;

        if(curSolutionIndex >= 0)
        {
            ShowSolutionText(curSolutionIndex, n1, n2);

            GetSolution(n1, n2, out n1, out n2);

            SetSolutionText(solutionIndex, n1, n2);
        }
        else if(curSolutionIndex == -1)
        {
            SetSolutionText(solutionIndex, n1, n2);
        }

        for(int i = 0; i < solutionTextCount; i++)
        {
            uiManager_Cp.SetActiveSolutionText(i, i <= solutionIndex ? true : false);
        }
    }

    #endregion

    //-------------------- private methods
    // Generate random a and b
    void GenerateRandomProblem(out int a_pr, out int b_pr)
    {
        a_pr = Random.Range(3, 200);
        b_pr = Random.Range(2, a_pr);
    }

    // Generate n1 and n2
    bool GetSolution(int n1_pr, int n2_pr, out int n1_next_pr, out int n2_next_pr)
    {
        bool isValid = true;
        n1_next_pr = 0;
        n2_next_pr = 0;
        
        // check validation
        if(n1_pr <= 1 || n2_pr <= 1 || n1_pr < n2_pr)
        {
            isValid = false;

            return isValid;
        }

        // calculate new n1 and n2
        n1_next_pr = n2_pr;
        n2_next_pr = n1_pr % n2_pr;

        return isValid;
    }

    // Set problem text
    void SetProblemText(int a_pr, int b_pr)
    {
        string value = "HCF(" + a_pr.ToString() + ", " + b_pr.ToString() + ") = ?";

        problemText = value;
    }

    // Set solution text
    void SetSolutionText(int index, int n1_pr, int n2_pr)
    {
        string solution_tp = string.Empty;

        if(n2_pr > 1)
        {
            solution_tp = "<b><i><u>n1</u></i></b> = <b><i><u>n2</u></i></b> * " + (n1_pr / n2_pr) + " + " + (n1_pr % n2_pr);
        }
        else if(n2_pr == 1)
        {
            solution_tp = "HCF(" + a.ToString() + ", " + b.ToString() + ") = " + n2_pr.ToString();
        }
        else if(n2_pr == 0)
        {
            solution_tp = "HCF(" + a.ToString() + ", " + b.ToString() + ") = " + n1_pr.ToString();
        }

        uiManager_Cp.SetSolutionText(index, solution_tp);

        if(n2_pr == 0 || n2_pr == 1)
        {
            CheckAnswer(n1_pr, n2_pr);
        }
    }
    
    // Show solution text
    void ShowSolutionText(int index, int n1_old_pr, int n2_old_pr)
    {
        string solution_tp = n1_old_pr.ToString() + " = " + n2_old_pr.ToString() + " * "
            + (n1_old_pr / n2_old_pr).ToString() + " + " + (n1_old_pr % n2_old_pr);

        uiManager_Cp.SetSolutionText(index, solution_tp); 
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
    void CheckAnswer(int n1_pr, int n2_pr)
    {
        // check answer is right or wrong
        bool result = true;
        if(n1_pr != n1 || n2_pr != n2)
        {
            result = false;
        }

        if(result)
        {
            if(n2_pr > 1)
            {
                ActionSolutionTrue();
            }
            else if(n2_pr == 0 || n2_pr == 1)
            {
                ActionProblem();
            }
        }
        else
        {
            ActionSolutionFalse();
        }
    }
    
    #region Action

    // Action when solution is true
    void ActionSolutionTrue()
    {
        StartCoroutine(CorouActionSolutionTrue());
    }

    IEnumerator CorouActionSolutionTrue()
    {
        studentPanelAnimTrigger = "True";

        evaluateText = "You are correct";

        evaluatePanelText = "Go ahead";

        // answerBtnInteract = false;

        yield return new WaitForSeconds(0f);

        answer1InputText = string.Empty;
        answer2InputText = string.Empty;
    
        SubmitSolution(solutionIndex);
    
        // answerBtnInteract = true;
    }

    // Action when solution is false
    void ActionSolutionFalse()
    {
        StartCoroutine(CorouActionSolutionFalse());
    }

    IEnumerator CorouActionSolutionFalse()
    {
        studentPanelAnimTrigger = "False";

        evaluateText = "You are incorrect";

        evaluatePanelText = "Please try again";

        answerCorrect = false;

        answerBtnInteract = false;

        yield return new WaitForSeconds(1.5f);

        studentPanelAnimTrigger = "Think";

        answer1InputText = string.Empty;
        answer2InputText = string.Empty;
    
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

        evaluateText = "Congratulations";

        evaluatePanelText = "Good work";

        answerBtnInteract = false;

        nextProblemBtnInteract = true;
    
        SetMarkImageAnimations(problemIndex, "True");
    }

    // Action when problem is false
    void ActionProblemFalse()
    {
        studentPanelAnimTrigger = "True";

        evaluateText = "Congratulations";

        evaluatePanelText = "Hurry up";

        answerBtnInteract = false;

        nextProblemBtnInteract = true;
    
        SetMarkImageAnimations(problemIndex, "False");
    }

    #endregion

    //-------------------- Callback from UI
    // Called when click answer button
    public void OnClickAnswerButton()
    {
        int n1_tp = 0, n2_tp = 0;

        int.TryParse(answer1InputText, out n1_tp);
        int.TryParse(answer2InputText, out n2_tp);

        CheckAnswer(n1_tp, n2_tp);
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
