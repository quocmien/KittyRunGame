// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using SocketIOClient;

// public class SocketTest : MonoBehaviour
// {
//     private SocketIOUnity socket;
//     // Start is called before the first frame update
//     private void Start()
//     {
//         socket = new SocketIOUnity("http://localhost:3000");

//         socket.OnConnected += (sender, e) => 
//         {
//             Debug.Log("Connected");
//         };

//         socket.On("hello", data => 
//         {
//             Debug.Log(data);
//         });

//         socket.Connect();

//     }

//     private void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.A))
//         {
//             socket.Emit("test", 123);

//         }
//     }
// }
