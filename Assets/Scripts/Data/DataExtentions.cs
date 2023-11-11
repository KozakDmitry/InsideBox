﻿

using Scripts.Data;
using UnityEngine;

namespace Scripts.Data
{
    public static class DataExtentions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => 
            new Vector3Data(vector.x,vector.y,vector.z);

        public static Vector3 AsUnityVector(this Vector3Data vector) => 
            new(vector.x,vector.y,vector.z);
    }
}