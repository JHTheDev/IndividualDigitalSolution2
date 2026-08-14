using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypingCombat : MonoBehaviour
{
    public Text wordOutput = null;
    

    public int number;
    private string remainingWord = string.Empty;
    private string currentWord = "muffins";


    void Awake()
    {
       
    }

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        //Get bak word
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }




    void Update()
    {
      
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord();
            }
        }
    }
    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }
     
    void GetKeyInput()
    {
       
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return false;
    }


}
