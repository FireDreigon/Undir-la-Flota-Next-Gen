using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int currentEnergy;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myNavios.Count >= 5 && currentNavio == null)
            currentNavio = myNavios[0];
    }
    public void Mov()
    {

    }
}
