using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject shopPrefab;
    private Animator myAnimator;
    private GameObject player;

    private float currentDistance;

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
        text.enabled = true;
        background.SetActive(true);
    }

    public void OpenShop()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            _ = Instantiate(shopPrefab);
            player.GetComponent<PlayerController>().IsTalking(true);
        }

    }

    public void TalkingExit(bool resetIndex)
    {
        text.enabled = false;
        background.SetActive(false);
    }
}
