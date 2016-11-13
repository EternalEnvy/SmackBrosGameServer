using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    public struct DamageCircleFrame
    {
        Vector2 _position;
        Vector2 _launchDirection;
        float _power;
        float _radius;
        float _damage;
        public DamageCircleFrame(Vector2 position, Vector2 launchDirection, float power, float radius, float damage)
        {
            this._position = position;
            this._launchDirection = launchDirection;
            this._power = power;
            this._radius = radius;
            this._damage = damage;
        }
    }
    public struct DamageData
    {
        //postion relative to character, launch direction, power, radius, damage 
        public List<DamageCircleFrame>[] jabDamageFrames;
        public List<DamageCircleFrame>[] jabComboDamageFrames;
        public List<DamageCircleFrame>[] dashAttackDamageFrames;
        public List<DamageCircleFrame>[] fSmashDamageFrames;
        public List<DamageCircleFrame>[] uSmashDamageFrames;
        public List<DamageCircleFrame>[] dSmashDamageFrames;
        public List<DamageCircleFrame>[] fTiltDamageFrames;
        public List<DamageCircleFrame>[] uTiltDamageFrames;
        public List<DamageCircleFrame>[] dTiltDamageFrames;
        public List<DamageCircleFrame>[] upGetUpDamageFrames;
        public List<DamageCircleFrame>[] downGetUpDamageFrames;
        public List<DamageCircleFrame>[] fAirDamageFrames;
        public List<DamageCircleFrame>[] dAirDamageFrames;
        public List<DamageCircleFrame>[] bAirDamageFrames;
        public List<DamageCircleFrame>[] uAirDamageFrames;
        public List<DamageCircleFrame>[] nAirDamageFrames;
        public List<DamageCircleFrame>[] fThrowDamageFrames;
        public List<DamageCircleFrame>[] dThrowDamageFrames;
        public List<DamageCircleFrame>[] bThrowDamageFrames;
        public List<DamageCircleFrame>[] uThrowDamageFrames;
        public List<DamageCircleFrame>[] grabPummelDamageFrames;

        public List<DamageCircleFrame>[] StateNumToDamageData(int enumeratedState)
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
        private List<DamageCircleFrame>[] AddHitboxes(StreamReader reader)
        {
            int numDamageFrames = Convert.ToInt32(reader.ReadLine());
            int numHitboxes;
            var atkDamageFrames = new List<DamageCircleFrame>[numDamageFrames];
            for (int i = 0; i < numDamageFrames; i++)
            {
                numHitboxes = Convert.ToInt32(reader.ReadLine());
                for (int j = 0; j < numHitboxes; j++)
                {
                    string[] data = reader.ReadLine().Split(' ');
                    atkDamageFrames[i].Add(
                        new DamageCircleFrame(
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
