using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour
{

    [SerializeField]
    // List of all the possible position to spawn a collectable
    private GameObject[] spawnPointsArray;

    [SerializeField]
    // List of all the collectables
    private GameObject[] collectablesArray;

    [SerializeField]
    // Reference of the player
    private GameObject player;

    // Bool to Check if the collectable as been positionated
    private bool collectablePositionated;

    // Random integer to casualy select a position
    private int randomInt;

    // Start is called before the first frame update
    void Start()
    {
        collectablePositionated = false;

        // We generate a random int from 0 to the size of the SpawnPositionList 
        randomInt = Random.Range(0, spawnPointsArray.Length);

        // For the player we select a random Position to spawn it
        player.transform.position = spawnPointsArray[randomInt].transform.position;

        // We set the position to null so we won't get it again
        spawnPointsArray[randomInt] = null;

        // Foreach collectable in the list we search a random position to spawn it
        for (int i = 0; i < collectablesArray.Length; i++)
        {
            // Check if the collectable has been positionated
            do
            {
                // We generate a random int from 0 to the size of the SpawnPositionList 
                randomInt = Random.Range(0, spawnPointsArray.Length);

                // Check if the Random Position isn't null
                if (spawnPointsArray[randomInt] != null)
                {
                    // Set the collectable position to the Random spawn position
                    collectablesArray[i].transform.position = spawnPointsArray[randomInt].transform.position;

                    // Set random position to null
                    spawnPointsArray[randomInt] = null;

                    // Set the bool to true so we can bypass the while
                    collectablePositionated = true;
                }

            } while (!collectablePositionated);

            // Set the bool to false
            collectablePositionated = false;
        }
    }
}
