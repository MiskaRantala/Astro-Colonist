/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*void calculateIntegrationField(int targetX, int targetY)
    {
        unsigned int targetID = targetY * mArrayWidth + targetX;

        resetField();//Set total cost in all cells to 65535
        list openList;

        //Set goal node cost to 0 and add it to the open list
        setValueAt(targetID, 0);
        openList.push_back(targetID);

        while (openList.size > 0)
        {
            //Get the next node in the open list
            unsigned currentID = openList.front();
            openList.pop_front();

            unsigned short currentX = currentID % mArrayWidth;
            unsigned short currentY = currentID / mArrayWidth;

            //Get the N, E, S, and W neighbors of the current node
            std::vector neighbors = getNeighbors(currentX, currentY);
            int neighborCount = neighbors.size();

            //Iterate through the neighbors of the current node
            for (int i = 0; i & lt; neighborCount; i++)         
            {
                //Calculate the new cost of the neighbor node             
                // based on the cost of the current node and the weight of the next node             
                unsigned int endNodeCost = getValueByIndex(currentID)                          
                    + getCostField()-&gt;getCostByIndex(neighbors[i]);

                //If a shorter path has been found, add the node into the open list
                if (endNodeCost & lt; getValueByIndex(neighbors[i]))
            {
                    //Check if the neighbor cell is already in the list.
                    //If it is not then add it to the end of the list.
                    if (!checkIfContains(neighbors[i], openList))
                    {
                        openList.push_back(neighbors[i]);
                    }

                    //Set the new cost of the neighbor node.
                    setValueAt(neighbors[i], endNodeCost);
                }
            }
        }
    }

}
*/