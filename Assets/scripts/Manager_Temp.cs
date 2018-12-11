using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager_Temp : MonoBehaviour
{
    public GameObject Ranura2D;
    private GameObject Tablero_2D, InstantiatePoint2D;
    public class CoordenadasMap
    {
        public string Name = "X";
        [System.Serializable]
        public class Coordenadas
        {
            public string Name = "Y";
            public Vector3 Coordenada_2D, Coordenada_3D;
            public bool CanStay = true, FixCanStay = true;
            public Coordenadas(string name, Vector3 coordenada_2D, Vector3 coordenada_3D)
            {
                Name = name;
                Coordenada_2D = coordenada_2D;
                Coordenada_3D = coordenada_3D;
            }
        }
        public List<Coordenadas> AllCordenadas = new List<Coordenadas>();
        public CoordenadasMap(string name, List<Coordenadas> listCoordenadas)
        {
            Name = name;
            AllCordenadas = listCoordenadas;
        }
    }
    private List<CoordenadasMap> coordenadasMap = new List<CoordenadasMap>();
    public int Cuadrado;

    public List<NaviosStats> myNavios;
    public NaviosStats currentNavio;
    // Use this for initialization
    void Start()
    {
        InstantiatePoint2D = GameObject.Find("InstantiatePoint2D");
        Tablero_2D = InstantiatePoint2D.transform.GetChild(0).gameObject;
        CreatCoordenadasMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreatCoordenadasMap()
    {
        for (int x = 0; x < Cuadrado; x++)
        {
            char Coordenada = 'A';
            for (int i = 0; i < x; i++)
                Coordenada++;
            List<CoordenadasMap.Coordenadas> newCoordenadasList = new List<CoordenadasMap.Coordenadas>();
            for (int y = 0; y < Cuadrado; y++)
            {
                Coordenada += (char)y;
                Vector3 NewPos2D = Vector3.zero;
                Vector3 NewPos3D = Vector3.zero;

                NewPos2D.x += (1.47f + Ranura2D.GetComponent<RectTransform>().sizeDelta.x / 2) * (x + 1);
                NewPos2D.y += -(1.47f + Ranura2D.GetComponent<RectTransform>().sizeDelta.y / 2) * (y + 1);

                newCoordenadasList.Add(new CoordenadasMap.Coordenadas(y.ToString("00"), NewPos2D, NewPos3D));

                GameObject NewRanura2D = Instantiate(Ranura2D, Tablero_2D.transform, false);
                NewRanura2D.GetComponent<RectTransform>().localPosition = NewPos2D;

                if (((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0)))
                {
                    Color newColor = Color.blue;
                    newColor.a = 0.4f;
                    NewRanura2D.GetComponent<Image>().color = newColor;
                }
                else
                {
                    Color newColor = Color.blue + Color.grey;
                    newColor.a = 0.4f;
                    NewRanura2D.GetComponent<Image>().color = newColor;
                }

                if (y == 0)
                {
                    NewRanura2D.transform.GetChild(0).gameObject.SetActive(true);
                    NewRanura2D.transform.GetChild(0).GetComponent<Text>().text = Coordenada.ToString();
                }
                if (x == 0)
                {
                    NewRanura2D.transform.GetChild(1).gameObject.SetActive(true);
                    NewRanura2D.transform.GetChild(1).GetComponent<Text>().text = y.ToString("00");
                }
            }
            coordenadasMap.Add(new CoordenadasMap(Coordenada.ToString(), newCoordenadasList));
        }
    }
}
