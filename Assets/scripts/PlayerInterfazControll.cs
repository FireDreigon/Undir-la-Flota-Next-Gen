using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class PlayerInterfazControll : MonoBehaviour
{ 
    public enum Options { Base, Mov, MaxOptions}
    public Options CurrentOption;
    public Image Lifebarr, EnergiBarr;
    public Button Base, Mov_Back, Son_Front, Atk_TL, SAtak_TR;
    public Manager_Temp PlayerControll;
    public bool AnimBtnNow;
    public Animator AnimBtn;
    public float AnimBtnTime;

    public void Update()
    {
        if (AnimBtnNow)
        {
            AnimBtnTime += Time.deltaTime;
            if (AnimBtnTime>=AnimBtn.recorderStopTime)
            {
                AnimBtnNow = false;
                AnimBtn.SetBool(0, AnimBtnNow);
            }

        }
    }
    public void NewCurrentNavio()
    {
        Lifebarr.fillAmount = (float) PlayerControll.currentNavio.HP / PlayerControll.currentNavio.MaxHP;
    }   
    public void ChangeOption(Options newOption)
    {
        CurrentOption = newOption;

        switch (CurrentOption)
        {
            case Options.Base:
                Mov_Back.GetComponentInChildren<Text>().text = "Mov";
                Mov_Back.onClick.AddListener(() => ChangeOption(Options.Mov));
                Son_Front.GetComponentInChildren<Text>().text = "Son";
                Son_Front.onClick.AddListener(() => ChangeOption(Options.Mov));
                Atk_TL.GetComponentInChildren<Text>().text = "Atk";
                Atk_TL.onClick.AddListener(() => ChangeOption(Options.Mov));
                SAtak_TR.GetComponentInChildren<Text>().text = "S.Atk";
                SAtak_TR.onClick.AddListener(() => ChangeOption(Options.Mov));
                break;
            case Options.Mov:
                Mov_Back.GetComponentInChildren<Text>().text = "Back";
                Mov_Back.onClick.AddListener(() => Forward(-1));
                Son_Front.GetComponentInChildren<Text>().text = "Front";
                Son_Front.onClick.AddListener(() => Forward(1));
                Atk_TL.GetComponentInChildren<Text>().text = "T.Left";
                Atk_TL.onClick.AddListener(() => Root(-1));
                SAtak_TR.GetComponentInChildren<Text>().text = "T.Right";
                SAtak_TR.onClick.AddListener(() => Root(1));
                break;
        } 

        print(Mov_Back.onClick);
    }
    public void AnimButton()
    { 
        AnimBtnNow=true;
        AnimBtn.SetBool(0, AnimBtnNow);
    } 
    public void Atk()
    {

    }
    public void SAtk()
    {

    }
    public void Sonar()
    {

    }
    public void Forward(int i)
    {
       
    } 
    public void Root(int i)
    {

    }
}
