using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    [SerializeField] private string[] dialogTextArray;
    [SerializeField] private Text text;
    [SerializeField] private GameObject background;
    private Animator myAnimator;
    private GameObject player;

    public readonly bool givesQuest;

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

        text.text = dialogTextArray[index];
    }

    public void IncrementIndex()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            index++;
        }

        if(index >= dialogTextArray.Length)
        {
            TalkingExit(false);
            //finish dialog
        }
    }

    public void IncrementIndexQuest()
    {
        if (Input.GetKeyDown("joystick button 0") && index < dialogTextArray.Length)
        {
            index++;
        }
        else if(Input.GetKeyDown("joystick button 0") && index == dialogTextArray.Length)
        {
            TalkingExit(false);
            //accept quest
        }
        else if(Input.GetKeyDown("joystick button 1") && index == dialogTextArray.Length)
        {
            TalkingExit(false);
            //decline quest
        }
    }

    public void TalkingExit(bool resetIndex)
    {
        text.enabled = false;
        background.SetActive(false);
        isDialogOver = false;

        if(resetIndex)
            index = 0;
    }
}
