using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class movement : MonoBehaviour
{
    [SerializeField] private string searchText;
    [SerializeField] private int numPosters;
    [SerializeField] private GameObject prefabPoster;
    private float timer = 0.0f;
    private int pos = -1;
    private bool moveF = true;
    public float speed = 8f;
    private List<PackController> packList;
    private List<GameObject> cubeList;
    private int _currentCube;
    // Start is called before the first frame update
    void Start()
    {
        cubeList = new List<GameObject>();
        packList = new List<PackController>();
        StartCoroutine(GetImg());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pos == 1)
            {
                pos = 2;
            }
            else
            {
                pos = 0;
            }
        }
        
        switch (pos)
        {
            case 0:
                if (transform.position != new Vector3(-9.85f, 1.57f, -10.88f))
                {
                    MovePosition(new Vector3(-9.85f, 1.57f, -10.88f));
                }
                else
                {
                    pos += 1;
                }
                break;
            case 2:
                if (transform.position != new Vector3(-9.85f, 7f, -10.88f))
                {
                    MovePosition(new Vector3(-9.85f, 7f, -10.88f));
                }
                else
                {
                    pos += 1;
                }
                break;
            case 3:
                if (transform.position != new Vector3(5.85f, 7f, -10.88f))
                {
                    MovePosition(new Vector3(5.85f, 7f, -10.88f));
                }
                else
                {
                    pos += 1;
                }
                break;
            case 4:
                if (transform.position != new Vector3(5.85f, 1.57f, -10.88f))
                {
                    MovePosition(new Vector3(5.85f, 1.57f, -10.88f));
                }
                else
                {
                    pos -= 5 ;
                }
                break;
        }

   
            
        void MovePosition(Vector3 target )
        {
            transform.position = Vector3.MoveTowards(transform.position,target, speed * Time.deltaTime);
            
        }
    }
        private IEnumerator GetImg()
    {
        // Preparo petición datos API (normal)
        var data = UnityWebRequest.Get($"https://www.omdbapi.com/?s={searchText}&apikey=bec0dc5f");
        // realiza la petición, esperando respuesta
        yield return data.SendWebRequest();

        // se comprueba que no ha fallado conexión
        if (data.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(data.error);
        }
        else
        {
            // ya disponemos de los resultados de la consulta en data.downloadHandler.text
            Debug.Log(data.downloadHandler.text);
            // Pasamos JSON a objetos
            SearchData mySearch = JsonUtility.FromJson<SearchData>(data.downloadHandler.text);
             // Comprobación del resultado
            Debug.Log(mySearch.Search[0].Poster);
            Debug.Log("Total results: " + mySearch.totalResults);

            for (int i = 0; i < packList.Count && i < mySearch.Search.Count; i++)
            {
                GameObject posterObject = Instantiate(prefabPoster, transform.position, Quaternion.identity);
                PackController packController = posterObject.GetComponentInChildren<PackController>();
                // datos que muestra
                packList[i].title.text = mySearch.Search[i].Title;
                packList[i].year.text = mySearch.Search[i].Type.FirstCharacterToUpper() + " (" + mySearch.Search[i].Year + ")";

                // Preparo petición web de la imagen (Textura)
                if (mySearch.Search[i].Poster.StartsWith("http"))
                {
                    UnityWebRequest www = UnityWebRequestTexture.GetTexture(mySearch.Search[i].Poster);
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError(www.error);
                    }
                    else
                    {
                        Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                        packList[i].SetPosterImage(loadedTexture); // Call method to set poster image on cube
                    }
                }
            }
        }
        // fin de la coroutine
        yield break;
    }
    
    
}
[Serializable]
public class SearchData
{
    public List<MovieData> Search;
    public string totalResults;
    public string Response;
}
[Serializable]
public class MovieData
{
    public string Title;
    public string Year;
    public string imdbID;
    public string Type;
    public string Poster;
}