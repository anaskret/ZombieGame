using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    [SerializeField] private string[] dialogTextArray;
    [SerializeField] private string[] questAcceptedTextArray;
    [SerializeField] private string[] questCompletedTextArray;
    [SerializeField] private Text text;
    [SerializeField] private GameObject background;
    private Animator myAnimator;
    private GameObject player;

    public bool givesQuest;

    public bool questAccepted = false;
    public bool questCompleted = false;

    private float currentDistance;
    private int index = 0;

    private bool isDialogOver = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();

        text.enabled = false;
        background.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
    }

    public void PrintCurrentText()
    {
        if (dialogTextArray == null || isDialogOver)
            return;

        text.enabled = true;
        background.SetActive(true);

        if (questAccepted && !questCompleted)
        {
            text.text = questAcceptedTextArray[index];
        }
        else if (questCompleted)
        {
            text.text = questCompletedTextArray[index];
        }
        else
        {
            text.text = dialogTextArray[index];
        }
    }

    public void IncrementIndex()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            index++;
        }

        if((index >= dialogTextArray.Length && !givesQuest || !questAccepted)
           || (index >= questAcceptedTextArray.Length && (questAccepted && !questCompleted))
           || (index >= questCompletedTextArray.Length && (questCompleted)))
        {
            HideTextField();
            isDialogOver = true;
            //finish dialog
        }
    }

    public void IncrementIndexQuest()
    {
        if (Input.GetKeyDown("joystick button 0") && index+1 < dialogTextArray.Length)
        {
            index++;
        }
        else if(Input.GetKeyDown("joystick button 0") && index+1 == dialogTextArray.Length)
        {
            //quest accepted
            HideTextField();
            questAccepted = true;
            gameObject.GetComponent<QuestController>().QuestAccepted();
        }
        else if(Input.GetKeyDown("joystick button 1") && index+1 == dialogTextArray.Length)
        {
            //quest declined
            HideTextField();
        }
    }

    public void HideTextField()
    {
        text.enabled = false;
        background.SetActive(false);

    }

    public void ResetText()
    { 
        isDialogOver = false;
        index = 0; 
    }
}
