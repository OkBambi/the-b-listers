using UnityEngine;
using UnityEngine.AI;

public class ZigZagAI : MonoBehaviour
{
    //the zigzagger is an enemy that will spawn in pairs from the stopwatch
    //the zigzagger should zigzag towards the player.
    //the zigzagger should jump at the player with physics when it enters a certain range
    //  the jump should have a tiny wind up and doesnt have to be very accurate
    //  for the jump, you will need to disable the navmesh agent temporily and re enable it when you land

    //the zigzagger will need the player's x and z pos, so just get the whole player
    [SerializeField] GameObject player;

    //the zigzagger also needs to move with the navmesh agent
    [SerializeField] NavMeshAgent agent;                

    //the zigzagger should have adjustable zag stats
    [SerializeField] float zagDistance;
    [SerializeField] float zagAngle; //if this is 0, then it will go straight

    //the zagger should decide which phase of zaggin their on ie. zag left first, zag right first
    [SerializeField] bool isZagLeft = false;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    void ZigZag()
    {
        //this should start with a half Zag length going one way
        //continue with a full zag length going the other way
    }
}
