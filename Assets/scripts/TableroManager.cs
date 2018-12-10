using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableroManager : MonoBehaviour
{
    private int CurrentNavioID;
    public Dropdown NaviosDropDown;
    public List<GameObject> Piezas_2D, Piezas_3D;
    public GameObject Ranura2D, Ranura3D;
    private GameObject CurrentPieza;
    private GameObject Tablero_2D,Tablero_3D, InstantiatePoint2D, InstantiatePoint3D;

    public int Cuadrado;

    [System.Serializable]
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
    [System.Serializable]
    public class CoordernadasActuales
    {
        public enum Horientacion { V, H, MaxHorientacion }
        public enum Direccion { N, E, S, W, MaxDireccion }
        public Direccion direccion = 0;
        public Horientacion horientacion = Horientacion.V;
        public Pieza typePieza;
        public int X;
        public int Y;
        public GameObject Pf;
        public bool CanStay = true;
        public CoordernadasActuales() { }
        public CoordernadasActuales(int x, int y, GameObject pf, Pieza pieza)
        {
            X = x;
            Y = y;
            Pf = pf;
            typePieza = pieza;
        }
    }
    private CoordernadasActuales currentNavio = new CoordernadasActuales();
    private float CoolDownMov;

    private bool CanEdit = true;
    // Use this for initialization
    void Start()
    {
        InstantiatePoint2D = GameObject.Find("InstantiatePoint2D");
        InstantiatePoint3D = GameObject.Find("InstantiateTablero3D");
        Tablero_2D = InstantiatePoint2D.transform.GetChild(0).gameObject;
        Tablero_3D = InstantiatePoint3D.transform.parent.gameObject;
        CreatCoordenadasMap();
        for (int i = 0; i < NaviosStats.AllNavios.Count; i++)
        {
            List<string> AllNavioNames= new List<string>();
            AllNavioNames.Add(NaviosStats.AllNavios[i].Name);
            NaviosDropDown.AddOptions(AllNavioNames);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        CurrentNavioID = NaviosDropDown.value;
        if (CanEdit == true)
            if (currentNavio.Pf != null)
            {
                if (CoolDownMov > 0.1f)
                    MoveNavio();
                else
                    CoolDownMov += Time.deltaTime;
                if (currentNavio.CanStay)
                    if (Input.GetKeyDown(KeyCode.Return))
                    { 
                        for (int i = 0; i < currentNavio.Pf.transform.childCount; i++)
                        {
                            currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color -= Color.green;
                        }

                        GameObject NewNavio3D = Instantiate(Piezas_3D[(int)NaviosStats.AllNavios[CurrentNavioID].typePieza], Tablero_3D.transform, false);
                        Vector3 NewPos = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_3D;
                        NewPos.x += -5;
                        NewPos.z += 5;
                        NewPos.y += 6;
                        NewNavio3D.transform.localPosition = NewPos;

                        switch (currentNavio.typePieza)
                        {                             
                            case Pieza.Uno:                       
                                coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].FixCanStay = false;
                                break;
                            case Pieza.Dos:
                                switch (currentNavio.horientacion)
                                {
                                    case CoordernadasActuales.Horientacion.V:
                                        for (int i = currentNavio.Y - 1; i < currentNavio.Y + 2; i++)
                                            coordenadasMap[currentNavio.X].AllCordenadas[i].FixCanStay = false;
                                        break;
                                    case CoordernadasActuales.Horientacion.H:
                                        for (int i = currentNavio.X - 1; i < currentNavio.X + 2; i++)
                                            coordenadasMap[i].AllCordenadas[currentNavio.Y].FixCanStay = false;
                                        break;
                                }
                                break;
                            case Pieza.Tres:
                                switch (currentNavio.horientacion)
                                {
                                    case CoordernadasActuales.Horientacion.V:
                                        for (int i = currentNavio.Y - 2; i < currentNavio.Y + 3; i++)
                                            coordenadasMap[currentNavio.X].AllCordenadas[i].FixCanStay = false;
                                        break;
                                    case CoordernadasActuales.Horientacion.H:
                                        for (int i = currentNavio.X - 2; i < currentNavio.X + 3; i++)
                                            coordenadasMap[i].AllCordenadas[currentNavio.Y].FixCanStay = false;
                                        break;
                                }
                                break;
                        }
                        switch (currentNavio.direccion)
                        {
                            case CoordernadasActuales.Direccion.N:
                                NewNavio3D.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                break;
                            case CoordernadasActuales.Direccion.E:
                                NewNavio3D.transform.localRotation = Quaternion.Euler(0, 90, 0);
                                break;
                            case CoordernadasActuales.Direccion.S:
                                NewNavio3D.transform.localRotation = Quaternion.Euler(0, 180, 0);
                                break;
                            case CoordernadasActuales.Direccion.W:
                                NewNavio3D.transform.localRotation = Quaternion.Euler(0, 270, 0);
                                break;
                        }
                        currentNavio.Pf = null;
                    }
                if (Input.GetKeyDown(KeyCode.E))
                    try
                    {
                        ResetPos();
                        RootNavio(1);
                        ChangeColor(CanStay());
                    }
                    catch
                    {
                        print("No se puede rotar porque entra en conflicto en algun punto");
                        for (int i = 0; i < coordenadasMap.Count; i++)
                            for (int j = 0; j < coordenadasMap[i].AllCordenadas.Count; j++)
                                if (!coordenadasMap[i].AllCordenadas[j].CanStay && coordenadasMap[i].AllCordenadas[j].FixCanStay)
                                    coordenadasMap[i].AllCordenadas[j].CanStay = true;
                        RootNavio(-1);
                        ChangeColor(CanStay());
                    }
                if (Input.GetKeyDown(KeyCode.Q))
                    try
                    {
                        ResetPos();
                        RootNavio(-1);
                        ChangeColor(CanStay());
                    }
                    catch
                    {
                        print("No se puede rotar porque entra en conflicto en algun punto");
                        for (int i = 0; i < coordenadasMap.Count; i++)
                            for (int j = 0; j < coordenadasMap[i].AllCordenadas.Count; j++)
                                if (!coordenadasMap[i].AllCordenadas[j].CanStay && coordenadasMap[i].AllCordenadas[j].FixCanStay)
                                    coordenadasMap[i].AllCordenadas[j].CanStay = true;
                        RootNavio(1);
                        ChangeColor(CanStay());
                    }
            }
            else
             if (Input.GetKeyDown(KeyCode.Tab))
            {
                CurrentPieza = Instantiate(Piezas_2D[(int)NaviosStats.AllNavios[CurrentNavioID].typePieza], InstantiatePoint2D.transform, false);
                CurrentPieza.transform.localPosition = coordenadasMap[9].AllCordenadas[9].Coordenada_2D;
                currentNavio = new CoordernadasActuales(9, 9, CurrentPieza, NaviosStats.AllNavios[CurrentNavioID].typePieza);
                for (int i = 0; i < currentNavio.Pf.transform.childCount; i++)
                {
                    currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color += Color.green;
                }
                ChangeColor(CanStay());
            }


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

                NewPos3D.x += Ranura3D.transform.localScale.y  * (x + 1);
                NewPos3D.z += -Ranura3D.transform.localScale.z  * (y + 1);

                newCoordenadasList.Add(new CoordenadasMap.Coordenadas(y.ToString("00"), NewPos2D, NewPos3D));

                GameObject NewRanura2D = Instantiate(Ranura2D, Tablero_2D.transform, false);
                GameObject NewRanura3D = Instantiate(Ranura3D, InstantiatePoint3D.transform, false);
                NewRanura3D.transform.localPosition = NewPos3D;
                NewRanura2D.GetComponent<RectTransform>().localPosition = NewPos2D;

                if (((x % 2 == 0) && (y % 2 == 0)) || ((x % 2 != 0) && (y % 2 != 0)))
                {
                    Color newColor = Color.blue;
                    newColor.a = 0.4f;
                    NewRanura2D.GetComponent<Image>().color = newColor;
                    NewRanura3D.GetComponent<MeshRenderer>().material.color = Color.white;
                }
                else
                {
                    Color newColor = Color.blue + Color.grey;
                    newColor.a = 0.4f;
                    NewRanura2D.GetComponent<Image>().color = newColor;
                    NewRanura3D.GetComponent<MeshRenderer>().material.color = Color.grey;
                }

                if (y == 0)
                {
                    NewRanura2D.transform.GetChild(0).gameObject.SetActive(true);
                    NewRanura2D.transform.GetChild(0).GetComponent<Text>().text = Coordenada.ToString();

                    NewRanura3D.transform.GetChild(0).gameObject.SetActive(true);
                    NewRanura3D.transform.GetChild(0).GetComponent<TextMesh>().text = Coordenada.ToString();
                }
                if (x == 0)
                {
                    NewRanura2D.transform.GetChild(1).gameObject.SetActive(true);
                    NewRanura2D.transform.GetChild(1).GetComponent<Text>().text = y.ToString("00");

                    NewRanura3D.transform.GetChild(1).gameObject.SetActive(true);
                    NewRanura3D.transform.GetChild(1).GetComponent<TextMesh>().text = y.ToString("00");
                }
            }
            coordenadasMap.Add(new CoordenadasMap(Coordenada.ToString(), newCoordenadasList));
        }
    }
    public void MoveNavio()
    {
        CoolDownMov = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            ResetPos();
            float value = Input.GetAxis("Horizontal");
            switch (currentNavio.typePieza)
            {
                case Pieza.Uno:
                    if (value > 0)
                    {
                        if (currentNavio.X != coordenadasMap.Count - 1)
                        {
                            currentNavio.X++;
                            currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                        }
                    }
                    else
                    {
                        if (currentNavio.X != 0)
                        {
                            currentNavio.X--;
                            currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                        }
                    }
                    break;
                case Pieza.Dos:
                    if (value > 0)
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.X < coordenadasMap.Count - 1)
                                {
                                    currentNavio.X++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.X < coordenadasMap.Count - 2)
                                {
                                    currentNavio.X++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.X > 0)
                                {
                                    currentNavio.X--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.X > 1)
                                {
                                    currentNavio.X--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }

                    }
                    break;
                case Pieza.Tres:
                    if (value > 0)
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.X < coordenadasMap.Count - 1)
                                {
                                    currentNavio.X++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.X < coordenadasMap.Count - 3)
                                {
                                    currentNavio.X++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.X > 0)
                                {
                                    currentNavio.X--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.X > 2)
                                {
                                    currentNavio.X--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }

                    }
                    break;
            }
            ChangeColor(CanStay());

        }
        if (Input.GetAxis("Vertical") != 0)
        {
            ResetPos();
            float value = Input.GetAxis("Vertical");
            switch (currentNavio.typePieza)
            {
                case Pieza.Uno:
                    if (value < 0)
                    {
                        if (currentNavio.Y < coordenadasMap.Count - 1)
                        {
                            currentNavio.Y++;
                            currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                        }
                    }
                    else
                    {
                        if (currentNavio.Y > 0)
                        {
                            currentNavio.Y--;
                            currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                        }
                    }
                    break;
                case Pieza.Dos:
                    if (value < 0)
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.Y < coordenadasMap.Count - 2)
                                {
                                    currentNavio.Y++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.Y < coordenadasMap.Count - 1)
                                {
                                    currentNavio.Y++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }

                    }
                    else
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.Y > 1)
                                {
                                    currentNavio.Y--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.Y > 0)
                                {
                                    currentNavio.Y--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }

                    }
                    break;
                case Pieza.Tres:
                    if (value < 0)
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.Y < coordenadasMap.Count - 3)
                                {
                                    currentNavio.Y++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.Y < coordenadasMap.Count - 1)
                                {
                                    currentNavio.Y++;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (currentNavio.horientacion)
                        {
                            case CoordernadasActuales.Horientacion.V:
                                if (currentNavio.Y > 2)
                                {
                                    currentNavio.Y--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                            case CoordernadasActuales.Horientacion.H:
                                if (currentNavio.Y > 0)
                                {
                                    currentNavio.Y--;
                                    currentNavio.Pf.transform.localPosition = coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].Coordenada_2D;
                                }
                                break;
                        }
                    }
                    break;
            }
            ChangeColor(CanStay());

        }

    }
    public bool CanStay()
    {
        int count = 0;
        if (coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].CanStay && coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].FixCanStay)
        {
            coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].CanStay = false;
            count++;
        }

        switch (currentNavio.typePieza)
        {
            case Pieza.Uno:
                if (count == 1)
                    return true;
                break;
            case Pieza.Dos:
                switch (currentNavio.horientacion)
                {
                    case CoordernadasActuales.Horientacion.V:
                        for (int i = currentNavio.Y - 1; i < currentNavio.Y + 2; i++)
                            if (coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay && coordenadasMap[currentNavio.X].AllCordenadas[i].FixCanStay)
                            {
                                count++;
                                coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay = false;
                            }

                        break;
                    case CoordernadasActuales.Horientacion.H:
                        for (int i = currentNavio.X - 1; i < currentNavio.X + 2; i++)
                            if (coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay && coordenadasMap[i].AllCordenadas[currentNavio.Y].FixCanStay)
                            {
                                count++;
                                coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay = false;
                            }

                        break;
                }
                if (count == 3)
                    return true;
                break;
            case Pieza.Tres:
                switch (currentNavio.horientacion)
                {
                    case CoordernadasActuales.Horientacion.V:
                        for (int i = currentNavio.Y - 2; i < currentNavio.Y + 3; i++)
                            if (coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay && coordenadasMap[currentNavio.X].AllCordenadas[i].FixCanStay)
                            {
                                count++;
                                coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay = false;
                            }

                        break;
                    case CoordernadasActuales.Horientacion.H:
                        for (int i = currentNavio.X - 2; i < currentNavio.X + 3; i++)
                            if (coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay && coordenadasMap[i].AllCordenadas[currentNavio.Y].FixCanStay)
                            {
                                count++;
                                coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay = false;
                            }

                        break;
                }
                if (count == 5)
                    return true;
                break;
        }
        return false;
    }
    public void ChangeColor(bool can)
    {
        if (can != currentNavio.CanStay)
        {
            currentNavio.CanStay = can;
            if (currentNavio.CanStay)
                for (int i = 0; i < currentNavio.Pf.transform.childCount; i++)
                {
                    currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color -= Color.red;
                    currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color += Color.green;
                }
            else
                for (int i = 0; i < currentNavio.Pf.transform.childCount; i++)
                {
                    currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color -= Color.green;
                    currentNavio.Pf.transform.GetChild(i).GetComponent<Image>().color += Color.red;
                }
        }
    }
    public void ResetPos()
    {
        coordenadasMap[currentNavio.X].AllCordenadas[currentNavio.Y].CanStay = true;
        switch (currentNavio.typePieza)
        {
            case Pieza.Dos:
                switch (currentNavio.horientacion)
                {
                    case CoordernadasActuales.Horientacion.V:
                        for (int i = currentNavio.Y - 1; i < currentNavio.Y + 2; i++)
                            coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay = true;
                        break;
                    case CoordernadasActuales.Horientacion.H:
                        for (int i = currentNavio.X - 1; i < currentNavio.X + 2; i++)
                            coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay = true;
                        break;
                }
                break;
            case Pieza.Tres:
                switch (currentNavio.horientacion)
                {
                    case CoordernadasActuales.Horientacion.V:
                        for (int i = currentNavio.Y - 2; i < currentNavio.Y + 3; i++)
                            coordenadasMap[currentNavio.X].AllCordenadas[i].CanStay = true;
                        break;
                    case CoordernadasActuales.Horientacion.H:
                        for (int i = currentNavio.X - 2; i < currentNavio.X + 3; i++)
                            coordenadasMap[i].AllCordenadas[currentNavio.Y].CanStay = true;
                        break;
                }
                break;
        }
    }
    public void RootNavio(int i)
    {

        if (currentNavio.horientacion == 0)
            currentNavio.horientacion = CoordernadasActuales.Horientacion.H;
        else
            currentNavio.horientacion = CoordernadasActuales.Horientacion.V;


        currentNavio.direccion += i;

        if (currentNavio.direccion < 0)
            currentNavio.direccion = CoordernadasActuales.Direccion.MaxDireccion - 1;
        else if (currentNavio.direccion >= CoordernadasActuales.Direccion.MaxDireccion)
            currentNavio.direccion = 0;


        switch (currentNavio.direccion)
        {
            case CoordernadasActuales.Direccion.N:
                currentNavio.Pf.transform.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case CoordernadasActuales.Direccion.E:
                currentNavio.Pf.transform.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case CoordernadasActuales.Direccion.S:
                currentNavio.Pf.transform.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case CoordernadasActuales.Direccion.W:
                currentNavio.Pf.transform.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 270);
                break;
        }
        CanEdit = true;
    }
}
