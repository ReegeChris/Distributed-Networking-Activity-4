using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

//references: https://answers.unity.com/questions/26856/check-object-for-motion.html

public class Client : MonoBehaviour
{
    public GameObject myCube;
    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket clientSoc;

    //Lecture 5
    private float[] pos;
    private byte[] bpos;

    //Exercise 4 - Vector 3 variables to store the current position and the last position of the cube
    private Vector3 currentPosition;
    private Vector3 lastPosition;

    public static void StartClient()
    {
        //Recieve buffer
        byte[] buffer = new byte[512];

        try
        {

            IPAddress ip = IPAddress.Parse("192.168.2.13"); //"192.168.2.13"
            remoteEP = new IPEndPoint(ip, 11111);

            clientSoc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            
        
        } catch (Exception e)
          {
            Debug.Log("Exception: " + e.ToString());
          }


    }

    // Start is called before the first frame update
    void Start()
    {
        myCube = GameObject.Find("Cube");
        StartClient();

        //Lecture 5
        pos = new float[] {myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z};
        bpos = new byte[pos.Length * 4];


    }

    // Update is called once per frame
    void Update()
    {
        //Set the current position to be the trasnform.position of the cube
        currentPosition = myCube.transform.position;


        //Lecture 4 Exercise
     
        // Sned X position of Cube to Server
        // outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.x.ToString());

        // Debug.Log("Sending X: " + myCube.transform.position.x);

        //clientSoc.SendTo(outBuffer, remoteEP);

        //Send Z position to the server
        // outBuffer = Encoding.ASCII.GetBytes(myCube.transform.position.z.ToString());

        //clientSoc.SendTo(outBuffer, remoteEP);

        //EXERCISE 4 - If the current position of the cube is equal to the last position, that means the cube is not moving so no data is being sent
        if(currentPosition == lastPosition)
        {

            Debug.Log("Cube is not moving");

        }

        //LECTURE 5
        pos = new float [] {myCube.transform.position.x, myCube.transform.position.y, myCube.transform.position.z };

        Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);

        clientSoc.SendTo(bpos, remoteEP);
         

        lastPosition = currentPosition;


    }
}
