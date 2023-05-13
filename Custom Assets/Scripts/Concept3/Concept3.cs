using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concept3 : MonoBehaviour
{
    
    //----------------------------------------------- fields
    // SerializeField
    [SerializeField]
    Controller_Concept3 controller_Cp;

    // Private fields
    public int composite, curComposite; // temporary

    public int m_problemIndex; // temporary

    public int m_solutionIndex; // temporary

    public bool answerCorrect; // temporary

    List<int> solutions = new List<int>();

    //----------------------------------------------- properties
    #region PrivateProperties

    UIManager_Concept3 uiManager_Cp
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
        GenerateRandomProblemAndSolution();

        SetProblemText();

        answer1InputText = string.Empty;
    
        studentPanelAnimTrigger = "Think";

        answerBtnInteract = true;

        nextProblemBtnInteract = false;
        
        evaluateText = "Answer the value of prime.";

        evaluatePanelText = "";

        answerCorrect = true;

        SubmitSolution(-1);
    }

    // Submit solution
    void SubmitSolution(int curSolutionIndex)
    {
        // determine old composite
        int oldComposite_tp = composite;
        for(int i = 0; i < curSolutionIndex; i++)
        {
            oldComposite_tp /= solutions[i];
        }

        // 
        solutionIndex = curSolutionIndex + 1;

        if(curSolutionIndex >= 0)
        {
            curComposite = oldComposite_tp / solutions[curSolutionIndex];

            ShowSolutionText(oldComposite_tp, curSolutionIndex);

            SetSolutionText(curComposite, solutionIndex);
        }
        else if(curSolutionIndex == -1)
        {
            curComposite = composite;

            SetSolutionText(curComposite, solutionIndex);
        }

        for(int i = 0; i < solutionTextCount; i++)
        {
            uiManager_Cp.SetActiveSolutionText(i, i <= solutionIndex ? true : false);
        }
    }

    #endregion

    //-------------------- private methods
    // Generate random problem and solutin
    void GenerateRandomProblemAndSolution()
    {
        // generate random values
        List<int> primeArray_tp = new List<int>(new int[]{2, 3, 5, 7, 11});
        int primesNum_tp = Random.Range(2, primeArray_tp.Count + 1);
        int composite_tp = 1;

        solutions.Clear();
        
        for(int i = 0; i < primesNum_tp; i++)
        {
            int randComposite_tp = primeArray_tp[Random.Range(0, primeArray_tp.Count)];

            composite_tp *= randComposite_tp;

            solutions.Add(randComposite_tp);
        }

        // set random values
        composite = composite_tp;

        solutions.Sort();
    }

    // Set problem text
    void SetProblemText()
    {
        string value = "Composite Number: " + composite.ToString();

        problemText = value;
    }

    // Set solution text
    void SetSolutionText(int curComposite_pr, int index)
    {
        string solution_tp = string.Empty;

        if(index < solutions.Count - 1)
        {
            solution_tp = curComposite_pr.ToString() + " = " + curComposite_pr / solutions[index]
                + " * <b><i><u>prime</b></i></u>";
        }
        else if(index == solutions.Count - 1)
        {
            solution_tp = "<u>" + composite.ToString() + " = ";

            for(int i = 0; i < solutions.Count; i++)
            {
                solution_tp += (solutions[i].ToString() + " * ");
            }

            solution_tp = solution_tp.Remove(solution_tp.Length - 3);

            solution_tp += "</u>";
        }

        uiManager_Cp.SetSolutionText(index, solution_tp);

        if(index == solutions.Count - 1)
        {
            CheckAnswer(solutions[index]);
        }
    }
    
    // Show solution text
    void ShowSolutionText(int curComposite_pr, int curSolutionIndex_pr)
    {
        string solution_tp = curComposite_pr.ToString() + " = " + curComposite_pr / solutions[curSolutionIndex_pr]
            + " * " + solutions[curSolutionIndex_pr];

        uiManager_Cp.SetSolutionText(curSolutionIndex_pr, solution_tp);
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
    void CheckAnswer(int n_pr)
    {
        // check answer is right or wrong
        bool result = true;
        if(n_pr != solutions[solutionIndex])
        {
            result = false;
        }

        if(result)
        {
            if(solutionIndex < (solutions.Count - 1))
            {
                ActionSolutionTrue();
            }
            else if(solutionIndex == (solutions.Count - 1))
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

        yield return new WaitForSeconds(0f);

        answer1InputText = string.Empty;
    
        SubmitSolution(solutionIndex);
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
        int n_tp = 0;

        int.TryParse(answer1InputText, out n_tp);

        CheckAnswer(n_tp);
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
