// Devin Brannon Interactive Scriptin Fall 2023
// this code will spawn cubes at random locations around the map


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCubes : MonoBehaviour
{
    [SerializeField]
    GameObject go; 

    [Header("Color Change Experiment")]
    [SerializeField]
    float colorChangeInterval = 1f;

    [SerializeField]
    GameObject colorChangeObject;


    // Start is called before the first frame update
    void Start()
    {
        SayHello();
        Debug.Log("7 + 8 = " + AddTwoNumbers(7,8));
        int answer = AddTwoNumbers(400,600);
        Debug.Log("400 + 600 = " + answer);

        StartCoroutine(ChangeColor(colorChangeObject, colorChangeInterval)); 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(wait(go));
        }
     // Debug.Log("I exist!");
    }
void SayHello() {
        Debug.Log("Hello");
     }  
 
   int AddTwoNumbers(int num1, int num2) {
    return num1 + num2;
   }  
   IEnumerator wait(GameObject go) {
    go.SetActive(false);
    yield return new WaitForSeconds(2f);
    go.SetActive(true);
   }

   // this coroutine will change the color of a gameobject every half second.
   IEnumerator ChangeColor(GameObject givenObject, float interval = 0.5f) {
        // loop with a while() loop
        while(true) {
            // wait our seconds
          yield return new WaitForSeconds(interval);
          // then change color.
          // hypothetically, givenObject is the cube.
          givenObject.GetComponent<Renderer>().material.color = Random.ColorHSV();   
        }
        

   }
}

    
