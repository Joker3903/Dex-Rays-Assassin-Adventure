// Devin Brannon Interactive Scripting Fall 2023
// this code will spawn cubes at random locations around the map


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    public string[] names = {"Leonardo", "Raphael", "Donatello", "Michelangelo"};

    // names[0] = Leonardo
    // names[1] = Raphael
    // names[2] = Donatello
    // names[3] = Michelangelo

    [SerializeField]
    private Color[] colors; 

    [SerializeField]
    bool debug = false;

    // public (or editable) variables at the top
    [SerializeField]
    private GameObject prefabCube;

    [SerializeField]
    private int totalCubes = 25;

    [SerializeField]
    [Range(0.1f, 2f)]
    private float spawnCubeInterval = 1f;
    
    [SerializeField]
    private float spawnPositionRange = 40;
    
    private bool canStartSpawnLoop = true; 
    
    // Start is called before the first frame update
    void Start(){
        Debug.Log("Press Shift+0 to enable debug mode."); 
        if(debug) Debug.Log("<color=cyan>Press G to spawn cubes.</color>");
        if(debug) Debug.Log("<color=magenta>Press B to collect cubes.</color>");
        if(debug) Debug.Log("The first name in the array of names is " + names[0]);
        StartCoroutine(SpawnLoop()); 
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.LeftShift)) {
            if(Input.GetKeyDown(KeyCode.Alpha0)) {
                debug = !debug;     //  toggle the boolean
                Debug.Log("Debug mode is now" + debug);
            }
        }

        if(Input.GetKeyDown(KeyCode.G)) {
            if(canStartSpawnLoop == true) {
                StartCoroutine(SpawnLoop());
            }
            else {
                if(debug) Debug.Log("<color=red>You cannot start a new loop</color> until the old one is finished."); 
            } 
        }

        if(Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(CollectCubes());
        }
    }
    // spawn a single cube with a random color, in a random position, with a rigidbody.  
    GameObject SpawnCube() {
        if(debug) Debug.Log("<color=green>Starting SpawnCube() function.</color>");

        if(debug) Debug.Log("creating cube from prefab 'prefabCube'");
        GameObject cube = Instantiate(prefabCube);
        // move the cube to a random x position of -40, 40
        // move the cube to a y position of 2 
        // move the cube to a random z position of -40, 40

        // changing the name to a random name
        int index = Random.Range(0,names.Length);
        cube.name = names[index];
        cube.name = names[Random.Range(0,names.Length)];
        

        Vector3 newPos = new Vector3(
                                Random.Range(-spawnPositionRange,spawnPositionRange),
                                Random.Range(1.5f,2.5f),
                                Random.Range(-spawnPositionRange, spawnPositionRange)
                            );

        if(debug) Debug.Log("setting cube position to " + newPos);              
        cube.transform.position = newPos;

        // log error (turn on error pause)
        // if(debug) Debug.LogError("Pausing here to look at the position of the cube.");

        Color newColor = Random.ColorHSV();  
        if(debug) Debug.Log("setting color to " + newColor);
        // the old way
        // cube.GetComponent<Renderer>().material.color = newColor;

        // the new way, use the colors from our array of colors.
        // names[1] always gets colors[1]. 
        cube.GetComponent<Renderer>().material.color = colors[index];

        if(debug) Debug.Log("adding Rigidbody component.");
        // cube.AddComponent(typeof(Rigidbody));


        if(debug) Debug.Log("<color=red>End of SpawnCube() function.</color>");
        return cube; 
    } 

    // this function and it's loop all happen on one frame.
    IEnumerator CollectCubes() {
        // find all cubes in scene // add them to an array and add them to an array
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        // move all of them to the same location (0,2,0)
        int i = 10;
        while(i < cubes.Length) {
            cubes[i].transform.position = new Vector3(0,2,0);
            i += 1;
            yield return new WaitForEndOfFrame(); 
        }
    }

    // continue to spawn cubes until enough have been spawned
    IEnumerator SpawnLoop() {
        // don't allow the player to spawn more cubes until this coroutine is finished.
        canStartSpawnLoop = false;

        int counter = 0;
        while (counter < totalCubes) {
            // counter = counter + 1; 
            counter += 1;       // adds 1 to counter.
            SpawnCube(); 
            yield return new WaitForSeconds(spawnCubeInterval);
        
        }

        // don't allow the player to spawn more cubes until this coroutine is finished.
        canStartSpawnLoop = true;
    }
}


