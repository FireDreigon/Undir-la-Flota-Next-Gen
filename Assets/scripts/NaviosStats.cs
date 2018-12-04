using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviosStats : MonoBehaviour
{
    public enum TypeAtk { TERR, AER, DUAL, MAXTYPE }
    public class Navios
    {  
        public string Name;
        public int ATK, DEF, VEL, SON;
        public TypeAtk typeAtk;
        public GameObject pf;
    }
    public List<Navios> AllNavios = new List<Navios>()
    {
        //Destructor
        new Navios
        {
            Name="",
            ATK=0,
            DEF=0,
            VEL=0,
            SON=0,
            typeAtk=TypeAtk.MAXTYPE
        },
        //Fragata
        new Navios
        {
            Name="",
            ATK=0,
            DEF=0,
            VEL=0,
            SON=0,
            typeAtk=TypeAtk.MAXTYPE
        },
        //Submarino
        new Navios
        {
            Name="",
            ATK=0,
            DEF=0,
            VEL=0,
            SON=0,
            typeAtk=TypeAtk.MAXTYPE
        },
        //Acorazado
        new Navios
        {
            Name="",
            ATK=0,
            DEF=0,
            VEL=0,
            SON=0,
            typeAtk=TypeAtk.MAXTYPE
        },
        //Portaviones
        new Navios
        {
            Name="",
            ATK=0,
            DEF=0,
            VEL=0,
            SON=0,
            typeAtk=TypeAtk.MAXTYPE
        }


    };
    
   
} 
