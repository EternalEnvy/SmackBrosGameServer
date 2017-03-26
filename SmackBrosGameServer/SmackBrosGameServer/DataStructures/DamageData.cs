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
        public List<DamageCircleFrame>[] JabDamageFrames;
        public List<DamageCircleFrame>[] JabComboDamageFrames;
        public List<DamageCircleFrame>[] DashAttackDamageFrames;
        public List<DamageCircleFrame>[] FSmashDamageFrames;
        public List<DamageCircleFrame>[] USmashDamageFrames;
        public List<DamageCircleFrame>[] DSmashDamageFrames;
        public List<DamageCircleFrame>[] FTiltDamageFrames;
        public List<DamageCircleFrame>[] UTiltDamageFrames;
        public List<DamageCircleFrame>[] DTiltDamageFrames;
        public List<DamageCircleFrame>[] UpGetUpDamageFrames;
        public List<DamageCircleFrame>[] DownGetUpDamageFrames;
        public List<DamageCircleFrame>[] FAirDamageFrames;
        public List<DamageCircleFrame>[] DAirDamageFrames;
        public List<DamageCircleFrame>[] BAirDamageFrames;
        public List<DamageCircleFrame>[] UAirDamageFrames;
        public List<DamageCircleFrame>[] NAirDamageFrames;
        public List<DamageCircleFrame>[] FThrowDamageFrames;
        public List<DamageCircleFrame>[] DThrowDamageFrames;
        public List<DamageCircleFrame>[] BThrowDamageFrames;
        public List<DamageCircleFrame>[] UThrowDamageFrames;
        public List<DamageCircleFrame>[] GrabPummelDamageFrames;

        public List<DamageCircleFrame>[] StateNumToDamageData(int enumeratedState)
        {
            switch(enumeratedState)
            {
                case 36:
                    return JabDamageFrames;
                case 39:
                    return JabComboDamageFrames;
                case 42:
                    return DashAttackDamageFrames;
                case 43:
                    return UTiltDamageFrames;
                case 44:
                    return DTiltDamageFrames;
                case 45:
                    return FTiltDamageFrames;
                case 46:
                    return FSmashDamageFrames;
                case 47:
                    return DSmashDamageFrames;
                case 48:
                    return USmashDamageFrames;
                case 49:
                    return NAirDamageFrames;
                case 50:
                    return UAirDamageFrames;
                case 51:
                    return FAirDamageFrames;
                case 52:
                    return BAirDamageFrames;
                case 53:
                    return DAirDamageFrames;
                case 83:
                    return GrabPummelDamageFrames;
                case 85:
                    return UThrowDamageFrames;
                case 86:
                    return DThrowDamageFrames;
                case 87:
                    return FThrowDamageFrames;
                case 88:
                    return UThrowDamageFrames;
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
                        JabDamageFrames = AddHitboxes(reader);
                        //Jab Combo
                        JabComboDamageFrames = AddHitboxes(reader);
                        //Dash Attack
                        DashAttackDamageFrames = AddHitboxes(reader);
                        //Forward Smash
                        FSmashDamageFrames = AddHitboxes(reader);
                        //Up Smash
                        USmashDamageFrames = AddHitboxes(reader);
                        //Down Smash
                        DSmashDamageFrames = AddHitboxes(reader);
                        //Foward Tilt
                        FTiltDamageFrames = AddHitboxes(reader); 
                        //Up Tilt
                        UTiltDamageFrames = AddHitboxes(reader);
                        //Down Tilt
                        DTiltDamageFrames = AddHitboxes(reader);
                        //Up Getup
                        UpGetUpDamageFrames = AddHitboxes(reader);
                        //Down getup
                        DownGetUpDamageFrames = AddHitboxes(reader);
                        //Fair
                        FAirDamageFrames = AddHitboxes(reader); 
                        //Uair
                        UAirDamageFrames = AddHitboxes(reader);
                        //Dair
                        DAirDamageFrames = AddHitboxes(reader);
                        //Bair
                        BAirDamageFrames = AddHitboxes(reader);
                        //FThrow
                        FThrowDamageFrames = AddHitboxes(reader); 
                        //UThrow
                        UThrowDamageFrames = AddHitboxes(reader);
                        //DThrow
                        DThrowDamageFrames = AddHitboxes(reader);
                        //BThrow
                        BThrowDamageFrames = AddHitboxes(reader); 
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
