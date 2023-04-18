using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
 
 
public class ChangeMaterial : MonoBehaviour
{
    public Texture2D[] textures;
    public GameObject[] materials;

    public int test;

    GameObject myobject;
    Texture thetexture;

    private void Start()
    {
        
            for (test = 0; test < textures.Length; test++)
            {
                myobject = materials[test];
                thetexture = textures[test];
                
                myobject.GetComponent<Renderer>().material.mainTexture = thetexture;
            }

     
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
                myobject = materials[test];
                thetexture = textures[test];
                myobject.GetComponent<Renderer>().material.mainTexture = thetexture;
                
                Debug.Log(test);

                test++;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (test = 0; test < textures.Length; test++)
            {
                myobject = materials[test];
                thetexture = textures[test];
                
                myobject.GetComponent<Renderer>().material.mainTexture = thetexture;
            }

        }
    }
}