using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    public struct DamageData
    {
        //postion relative to character, launch direction, power, radius, damage 
        public List<Tuple<Vector2, Vector2, float, float, int>>[] jabDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] jabComboDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] dashAttackDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] fSmashDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] uSmashDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] dSmashDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] fTiltDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] uTiltDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] dTiltDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] upGetUpDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] downGetUpDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] fAirDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] dAirDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] bAirDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] uAirDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] nAirDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] fThrowDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] dThrowDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] bThrowDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] uThrowDamageFrames;
        public List<Tuple<Vector2, Vector2, float, float, int>>[] grabPummelDamageFrames;

        public List<Tuple<Vector2, Vector2, float, float, int>>[] StateNumToDamageData(int enumeratedState)
        {
            switch(enumeratedState)
            {
                case 36:
                    return jabDamageFrames;
                case 39:
                    return jabComboDamageFrames;
                case 42:
                    return dashAttackDamageFrames;
                case 43:
                    return uTiltDamageFrames;
                case 44:
                    return dTiltDamageFrames;
                case 45:
                    return fTiltDamageFrames;
                case 46:
                    return fSmashDamageFrames;
                case 47:
                    return dSmashDamageFrames;
                case 48:
                    return uSmashDamageFrames;
                case 49:
                    return nAirDamageFrames;
                case 50:
                    return uAirDamageFrames;
                case 51:
                    return fAirDamageFrames;
                case 52:
                    return bAirDamageFrames;
                case 53:
                    return dAirDamageFrames;
                case 83:
                    return grabPummelDamageFrames;
                case 85:
                    return uThrowDamageFrames;
                case 86:
                    return dThrowDamageFrames;
                case 87:
                    return fThrowDamageFrames;
                case 88:
                    return uThrowDamageFrames;
                default:
                    return null;
            }
        }
        public bool LoadData(string path)
        {
            if (!File.Exists(path))
            {   
                Console.WriteLine("File "+path+"does not exist");
                return false;
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        //Jab
                        jabDamageFrames = AddHitboxes(reader);
                        //Jab Combo
                        jabComboDamageFrames = AddHitboxes(reader);
                        //Dash Attack
                        dashAttackDamageFrames = AddHitboxes(reader);
                        //Forward Smash
                        fSmashDamageFrames = AddHitboxes(reader);
                        //Up Smash
                        uSmashDamageFrames = AddHitboxes(reader);
                        //Down Smash
                        dSmashDamageFrames = AddHitboxes(reader);
                        //Foward Tilt
                        fTiltDamageFrames = AddHitboxes(reader); 
                        //Up Tilt
                        uTiltDamageFrames = AddHitboxes(reader);
                        //Down Tilt
                        dTiltDamageFrames = AddHitboxes(reader);
                        //Up Getup
                        upGetUpDamageFrames = AddHitboxes(reader);
                        //Down getup
                        downGetUpDamageFrames = AddHitboxes(reader);
                        //Fair
                        fAirDamageFrames = AddHitboxes(reader); 
                        //Uair
                        uAirDamageFrames = AddHitboxes(reader);
                        //Dair
                        dAirDamageFrames = AddHitboxes(reader);
                        //Bair
                        bAirDamageFrames = AddHitboxes(reader);
                        //FThrow
                        fThrowDamageFrames = AddHitboxes(reader); 
                        //UThrow
                        uThrowDamageFrames = AddHitboxes(reader);
                        //DThrow
                        dThrowDamageFrames = AddHitboxes(reader);
                        //BThrow
                        bThrowDamageFrames = AddHitboxes(reader); 
                    }
                    return true;
                }
                catch
                { 
                    Console.WriteLine("Unexpected error occurred reading file");
                    return false;
                }
            }
        }
        private List<Tuple<Vector2, Vector2, float, float, int>>[] AddHitboxes(StreamReader reader)
        {
            int numDamageFrames = Convert.ToInt32(reader.ReadLine());
            int numHitboxes;
            var atkDamageFrames = new List<Tuple<Vector2, Vector2, float, float, int>>[numDamageFrames];
            for (int i = 0; i < numDamageFrames; i++)
            {
                numHitboxes = Convert.ToInt32(reader.ReadLine());
                for (int j = 0; j < numHitboxes; j++)
                {
                    string[] data = reader.ReadLine().Split(' ');
                    atkDamageFrames[i].Add(
                        new Tuple<Vector2, Vector2, float, float, int>(
                            new Vector2(Convert.ToSingle(data[0]), Convert.ToSingle(data[1])),
                            new Vector2(Convert.ToSingle(data[2]), Convert.ToSingle(data[3])),
                            Convert.ToSingle(data[4]),
                            Convert.ToSingle(data[5]),
                            Convert.ToInt32(data[6])));

                }
            }
            return atkDamageFrames;
        }
    }
}
