using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorCanvas : MonoBehaviour
{
    public GameObject PicturesDialogsWindows;
    public GameObject FoxIcons;
    public GameObject CatIcons;
    public Text FoxText;
    public Text CatText;
    private Replica _replica;
    private string _endOfPhrase;
    public GameObject tutorButton;

    void Start()
    {
        _endOfPhrase = "\n(Кликните в любом месте, чтобы продолжить)";
        //если обучение ещё не пройдено, то начинаем обучение
        if (PlayerPrefs.GetInt("TutorFinished") == 0)
        {
            DialogsOnTutor.GenerateDialogs();
            NextReplica();
            Time.timeScale = 0;
        }
    }

    public void InvisibleButtonForDealogs()
    {
        NextReplica();

        if (_replica.Id == 7 || _replica.Id == 14)
        {
            Time.timeScale = 1;
            PicturesDialogsWindows.SetActive(false);
        }

        if (_replica.Id == 7)
        {
            tutorButton.GetComponentInChildren<Text>().text = "Используйте клавиши WASD для передвижения" + _endOfPhrase;
            tutorButton.SetActive(true);
        }
    }

    private void NextReplica()
    {
        _replica = DialogsOnTutor.GetNextReplica();
        if (_replica.WhoSaid == "CAT")
        {
            CatText.text = _replica.WhatSay + _endOfPhrase;
            CatIcons.SetActive(true);
            FoxIcons.SetActive(false);
        }

        else
        {
            FoxText.text = _replica.WhatSay + _endOfPhrase;
            CatIcons.SetActive(false);
            FoxIcons.SetActive(true);
        }

        PicturesDialogsWindows.SetActive(true);
    }

    public void InvisibleButtonForTutButton()
    {
        tutorButton.SetActive(false);
    }
}