using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;



public class WebSocketClient : MonoBehaviour
{
    public const string APIDomain = "localhost:1234";
    public const string APIUrl = "https://" + APIDomain;
    private SocketIOComponent socket;
    public TestSocketControl controls;




    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        // socket.Connect();
        socket.On("open", OnSocketOpen);

        socket.On("left", HandleLeft);

        socket.On("right", HandleRight);
        socket.On("spawn", HandleSpawn);
        socket.On("fire", HandleFire);
        socket.On("destroy", HandleDestroy);
    }
    public void OnSocketOpen(SocketIOEvent ev)
    {
        Debug.Log("updated socket id " + socket.sid);
        socket.Emit("registerGame", new JSONObject("{\"id\": \"" + socket.sid + "\"}"));
    }

    public void HandleLeft(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        string id = e.data.list[0].ToString();
        Debug.Log("ID: " + id);
        GetComponent<AlienController>().Left(id);
    }
    public void HandleFire(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        string id = e.data.list[0].ToString();
        Debug.Log("ID: " + id);
        GetComponent<AlienController>().Fire(id);
    }
    public void HandleRight(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        string id = e.data.list[0].ToString();
        Debug.Log("ID: " + id);
        GetComponent<AlienController>().Right(id);
    }
    public void HandleSpawn(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        string id = e.data.list[0].ToString();
        Debug.Log("ID: " + id);
        GetComponent<AlienController>().Spawn(id);
    }
    public void HandleDestroy(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        string id = e.data.list[0].ToString();
        Debug.Log("ID: " + id);
        GetComponent<AlienController>().Destroy(id);
    }
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;

        }
    }

}
