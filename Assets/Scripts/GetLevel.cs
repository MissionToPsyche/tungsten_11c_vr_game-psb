//Not being used anymore 1/25/24 - Peyton O'Boyle
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevel : MonoBehaviour
{
    public float maxDistance;

    public float getLevel()
    {
        //Get the local y position of the slider
        float localZ = transform.localPosition.z;

        //Return variable is from -1 to 1.
        //1 = limit - (slider height / 2)
        //-1 = -(limit - (slider height / 2)

        //return local y position / maxDisplacement
        return localZ / maxDistance;
    }
}
