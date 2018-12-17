using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerControll : MonoBehaviour
{
    [System.Serializable]
    public class Navio
    {
        public NaviosStats.NaviosType navioType;
        public TableroManager.CoordernadasActuales currentCoordenadas;
        public Navio(NaviosStats.NaviosType newnNavioType, TableroManager.CoordernadasActuales newCooredenadas)
        {
            navioType = newnNavioType;
            currentCoordenadas = newCooredenadas;
        }
    }
    public List<Navio> myNavios = new List<Navio>();
    public Navio currentNavio;
    public TableroManager tableroManager;
    public PlayerInterfazControll interfazControll;
    public int currentEnergy;

    // Use this for initialization
    void Start()
    {
        print(tableroManager.PlayerInterfaz.transform.GetChild(0));
        interfazControll = transform.GetComponentInChildren<PlayerInterfazControll>();
        interfazControll.PlayerControll = this;
        interfazControll.Lifebarr = tableroManager.PlayerInterfaz.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        interfazControll.EnergiBarr = tableroManager.PlayerInterfaz.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        interfazControll.Mov_Back = tableroManager.PlayerInterfaz.transform.GetChild(2).GetChild(0).transform.GetComponent<Button>();
        interfazControll.Son_Front = tableroManager.PlayerInterfaz.transform.GetChild(2).GetChild(1).transform.GetComponent<Button>();
        interfazControll.SAtak_TR = tableroManager.PlayerInterfaz.transform.GetChild(2).GetChild(2).transform.GetComponent<Button>();
        interfazControll.Atk_TL = tableroManager.PlayerInterfaz.transform.GetChild(2).GetChild(3).transform.GetComponent<Button>();
        interfazControll.Base = tableroManager.PlayerInterfaz.transform.GetChild(2).GetChild(4).transform.GetComponent<Button>();
        tableroManager.PlayerInterfaz.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (myNavios.Count >= 5 && currentNavio == null)
        {
            currentNavio = myNavios[0];
            interfazControll.NewCurrentNavio();
            tableroManager.PlayerInterfaz.SetActive(true);
            tableroManager.InstantiatePoint2D.transform.localPosition = new Vector2(-902, -130);
            tableroManager.InstantiatePoint2D.transform.localScale = Vector3.one * 6.136372f;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

            }
        }

    }
    public void Mov()
    {

    }
}
