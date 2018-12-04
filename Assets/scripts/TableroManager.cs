using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableroManager : MonoBehaviour
{ 
    public enum Pieza {Uno,Dos,Tres,MaxPiezas}
    public Pieza TypePieza;
    public List<GameObject> Piezas;
    public GameObject Ranura;
    private GameObject CurrentPieza;
    private GameObject InstantiatePoint;
    // Use this for initialization
    void Start()
    {
        InstantiatePoint = GameObject.Find("InstantiatePoint");
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                Vector3 NewPos = Vector3.zero;
                NewPos.x += (1.47f + Ranura.GetComponent<RectTransform>().sizeDelta.x / 2) * (x + 1);
                NewPos.y += -(1.47f + Ranura.GetComponent<RectTransform>().sizeDelta.y / 2) * (y + 1);
                GameObject NewRanura = Instantiate(Ranura, InstantiatePoint.transform, false);
                NewRanura.GetComponent<RectTransform>().localPosition = NewPos;

                if (((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0)))
                {
                    Color newColor = Color.blue;
                    newColor.a = 0.4f;
                    NewRanura.GetComponent<Image>().color = newColor;
                }
                else
                {
                    Color newColor = Color.blue + Color.grey;
                    newColor.a = 0.4f;
                    NewRanura.GetComponent<Image>().color = newColor;
                }
                    
                print("Imprimo");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CurrentPieza = Instantiate(Piezas[(int)TypePieza],InstantiatePoint.transform,false);
        else if (Input.GetMouseButton(0))
            CurrentPieza.transform.position = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
            Destroy(CurrentPieza); 

        if(CurrentPieza!=null)
        {

        }
    }
}
