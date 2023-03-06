using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{ 
    public GameObject EndingPanel;
    public TMP_Text TextBox;
    public string[] Dialogues;
    
    private bool _inRange = false;

    private int _currentDialogue = 0;
    
    
    private void Start()
    {
        EndingPanel?.SetActive(false);
    }

    private void Update()
    {
        if (!_inRange)
            return;
        
        if (Input.GetButtonDown("Submit"))
        {
            if (_currentDialogue >= Dialogues.Length)
                return;

            EndingPanel?.SetActive(true);
            TextBox.text = Dialogues[_currentDialogue];
            _currentDialogue ++;
            _currentDialogue = Mathf.Clamp(_currentDialogue, 0, Dialogues.Length);
            
            if (_currentDialogue >= Dialogues.Length)
                Invoke("GoToNextLevel", 2f);
        }


    }

    void GoToNextLevel()
    {
        GameManager.Instance.GoToNextLevel();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        _inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _inRange = false;
        EndingPanel?.SetActive(false);

    }
}
