using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug
{
    public enum EntityType
    {
        Unknown = -1,  //  Establishes the minimum value, ensures valid types start with 0 and up
        Player,
        Host,

        FinalEntity  //  Always add new types before this one!
    }

    public class EntityLocation
    {
        //  Maintains 3D coordinates in full (double) world resolution with convenience references in normal (int) screen resolution
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        public int NormX
        {
            get
            {
                return (int)Math.Round(X);
            }
        }

        public int NormY 
        {
            get
            {
                return (int)Math.Round(Y);
            }
        }

        public int NormZ 
        {
            get
            {
                return (int)Math.Round(Z);
            }
        }

        public double DistanceTo(EntityLocation aPoint)  //  Simple distance calc to another location in 3 dimensions
        {
            double rtn = Math.Sqrt(Math.Pow((X - aPoint.X), 2) + Math.Pow((Y - aPoint.Y), 2) + Math.Pow((Z - aPoint.Z), 2));

            return rtn;
        }

        public void Move(double ofsX, double ofsY, double ofsZ = 0.0)  //  Move the location in world terms by the distance specified in each axis (usually these won't move in the Z-axis so defaults to 0)
        {
            X = X + ofsX;
            Y = Y + ofsY;
            Z = Z + ofsZ;
        }
    }

    public interface IEntity
    {
        public EntityType Type { get; }
        public string Label { get; }
        public EntityLocation Location { get; }
    }



}
