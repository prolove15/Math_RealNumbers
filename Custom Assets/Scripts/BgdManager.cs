using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgdManager : MonoBehaviour
{
    
    //----------------------------------------------- fields
    [SerializeField]
    Animator curtainAnim_Cp;

    //----------------------------------------------- methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Init
    public void Init()
    {
        if(!curtainAnim_Cp.gameObject.activeInHierarchy)
        {
            curtainAnim_Cp.gameObject.SetActive(true);
        }
    }

    // Curtain up
    public void CurtainUp()
    {
        curtainAnim_Cp.SetTrigger("Up");
    }

    // Curtain down
    public void CurtainDown()
    {
        curtainAnim_Cp.SetTrigger("Down");
    }
}
