    // using System;
    // using System.Collections.Generic;
    // using UnityEngine;

    // public class SocketTest : MonoBehaviour
    // {
    //     private SocketIOUnity.SocketIOUnity socket; // Sử dụng lớp đầy đủ "SocketIOUnity.SocketIOUnity"

    //     private void Start()
    //     {
    //         // Khởi tạo kết nối tới server Socket.IO
    //         var uri = new Uri("http://localhost:11100");  // Thay bằng địa chỉ server của bạn
    //         socket = new SocketIOUnity.SocketIOUnity(uri, new SocketIOUnity.SocketIOOptions
    //         {
    //             Query = new Dictionary<string, string>
    //             {
    //                 { "token", "UNITY" }
    //             },
    //             Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
    //         });

    //         // Xác định UnityThreadScope nếu bạn cần thao tác với các đối tượng Unity
    //         socket.unityThreadScope = SocketIOUnity.UnityThreadScope.Update;

    //         // Đăng ký sự kiện khi kết nối thành công
    //         socket.On("connection", response =>
    //         {
    //             Debug.Log("Connected to server, received message: " + response.ToString());
    //         });

    //         // Đăng ký sự kiện "spin"
    //         socket.OnUnityThread("spin", response =>
    //         {
    //             Debug.Log("Spin event triggered");
    //             // Thực hiện hành động trên đối tượng Unity, ví dụ: xoay object
    //             // objectToSpin.transform.Rotate(0, 45, 0);  // Nếu bạn có object
    //         });

    //         // Kết nối tới server
    //         socket.Connect();
    //     }

    //     private void OnApplicationQuit()
    //     {
    //         // Đảm bảo đóng kết nối khi ứng dụng tắt
    //         socket.Disconnect();
    //     }
    // }
