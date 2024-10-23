using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script should be attached to the ManagerFlocking object in your scene.
// Ensure that you assign the Fish prefab in the Inspector under the variable 'fishPrefab'.

public class FlockManager : MonoBehaviour
{
    // Static instance of FlockManager, allowing access from any Flock script.
    public static FlockManager FM; 
    
    // The fish prefab to instantiate for each fish in the flock
    public GameObject fishPrefab;
    public GameObject leaderPrefab;
    
    // Number of fish in the flock
    public int numFish = 20; 
    
    // Array to store all instantiated fish
    public GameObject[] allFish; 
    
    // Defines the 3D space within which the fish can swim
    public Vector3 swimLimits = new Vector3(5, 5, 5); 
    
    // leader parameters
    public int LeaderNum = 3;
    public GameObject[] allLeader;
    public Vector3[] leaderPos;// leer las posiciones de los lideres
    public Vector3[] leaderObj;//seleccionar/sustituir su objetivo

    // Randomly selected goal position for fish to move towards
    public Vector3 goalPos = Vector3.zero;

    [Header("Fish Settings")] // Organizes the following variables in the Inspector for easier configuration
    
    // Minimum speed of the fish (adjustable in Inspector)
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    
    // Maximum speed of the fish (adjustable in Inspector)
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    
    // Distance at which fish recognize each other as neighbors (for cohesion, separation, and alignment)
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    
    // Rotation speed of the fish when changing direction (adjustable in Inspector)
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    // This method initializes the fish by spawning them within the defined limits
    void Start()
    {
        // Initialize the array to hold all the fish
        allFish = new GameObject[numFish];
        allLeader = new GameObject[LeaderNum];
        leaderPos = new Vector3[LeaderNum];
        leaderObj = new Vector3[LeaderNum];
        
        // Loop to create and place each fish randomly within the swim limits
        for (int i = 0; i < numFish; i++)
        {
            // Calculate a random position within the swim limits
            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),  
                Random.Range(-swimLimits.z, swimLimits.z));
            
            // Instantiate the fish prefab at the random position with no rotation
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < LeaderNum; i++)
        {
            Vector3 pos= this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
            allLeader[i] = Instantiate(leaderPrefab, pos, Quaternion.identity);
            leaderPos[i] = pos;
            //objetivo para cada lider
            leaderObj[i] = randomObj();
        }
        // Set the static reference to this instance of FlockManager
        FM = this;
        
        // Initialize the goal position as the FlockManager's position
        goalPos = this.transform.position;
    }

    // Update is called once per frame
    // Randomly updates the goal position for fish to swim towards
    void Update()
    {
        // Occasionally change the goal position (10% chance each frame)
        if (Random.Range(0, 100) < 10)
        {
            // Calculate a new random goal position within the swim limits
            goalPos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
        }
        //hacemos que se muevan de manera independiente
        for (int i = 0; i < LeaderNum; i++)
        { 
               if(Vector3.Distance(allLeader[i].transform.position, leaderObj[i])< 0.5)
            {
                leaderObj[i] = randomObj();
            }
            //generamos un vector para trasladar al cada lider a su posicion
            Vector3 direction = (leaderObj[i] - allLeader[i].transform.position).normalized;
            allLeader[i].transform.Translate(direction * maxSpeed * Time.deltaTime);

            //actualizamos su posicion
            leaderPos[i] = allLeader[i].transform.position;

        }
        
    }
    Vector3 randomObj()
    {
        return this.transform.position + new Vector3(
            Random.Range(-swimLimits.x, swimLimits.x),
            Random.Range(-swimLimits.y, swimLimits.y),
            Random.Range(-swimLimits.z, swimLimits.z));
    }
}