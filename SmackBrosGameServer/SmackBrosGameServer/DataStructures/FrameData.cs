using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    public struct FrameData
    {
        public short Width;
        public short Height;

        public short NumJumps;
        public float FirstJumpForce;
        public float ShortHopForce;
        public float AuxJumpForce;

        public short FairEndLag;
        public short UairEndLag;
        public short DairEndLag;
        public short BairEndLag;
        public short NairEndLag;

        public float WalkSpeed;
        public float RunSpeed;
        public float DashSpeed;

        public float GravityForce;

        public short JumpCrouchDuration;
        public short JabDuration;
        public short JabComboDuration;
        public short DashAttackDuration;
        public short FSmashDuration;
        public short USmashDuration;
        public int DSmashDuration;
        public int FTiltDuration;
        public int UTiltDuration;
        public int DTiltDuration;
        public int DownGetupAttackDuration;
        public int UpGetupAttackDuration;
        
        public int FairDuration;
        public int UairDuration;
        public int DairDuration;
        public int BairDuration;
        public int NairDuration;
        public int GrabDuration;
        public int DashGrabDuration;
        public int AirDodgeDuration;
        public int SpotDodgeDuration;
        public int RollFowardDuration;
        public int RollBackwardDuration;

        public struct FrameDataCircle
        {
            Vector2 _position;
            Vector2 _characterMovement;
            float _radius;
            bool _modifier; 
            public FrameDataCircle(Vector2 position, Vector2 characterMovement, float radius, bool modifier)
            {
                this._position = position;
                this._characterMovement = characterMovement;
                this._radius = radius;
                this._modifier = modifier;
            }
        }
        //Position relative to char, movement, radius, Special Modifier (use based on context)
        public List<FrameDataCircle>[] WalkFrames;
        public List<FrameDataCircle>[] RunFrames;
        public List<FrameDataCircle>[] DashFrames;
        public List<FrameDataCircle>[] JumpFrames;
        public List<FrameDataCircle>[] AirJumpForwardFrames;
        public List<FrameDataCircle>[] AirJumpBackWardFrames;
        public List<FrameDataCircle>[] JabFrames;
        public List<FrameDataCircle>[] JabComboFrames;
        public List<FrameDataCircle>[] FTiltFrames;
        public List<FrameDataCircle>[] UTiltFrames;
        public List<FrameDataCircle>[] DTiltFrames;
        public List<FrameDataCircle>[] DashAttackFrames;
        public List<FrameDataCircle>[] USmashFrames;
        public List<FrameDataCircle>[] FSmashFrames;
        public List<FrameDataCircle>[] DSmashFrames;
        public List<FrameDataCircle>[] FairFrames;
        public List<FrameDataCircle>[] UairFrames;
        public List<FrameDataCircle>[] DairFrames;
        public List<FrameDataCircle>[] BairFrames;
        public List<FrameDataCircle>[] NairFrames;
        public List<FrameDataCircle>[] GrabFrames;
        public List<FrameDataCircle>[] DashGrabFrames;
        public List<FrameDataCircle>[] AirDodgeFrames;
        public List<FrameDataCircle>[] SpotDodgeFrames;
        public List<FrameDataCircle>[] RollForwardFrames;
        public List<FrameDataCircle>[] RollBackwardFrames;
        public List<FrameDataCircle>[] TechInPlaceFrames;
        public List<FrameDataCircle>[] TechForwardFrames;
        public List<FrameDataCircle>[] TechBackFrames;
        public List<FrameDataCircle>[] TechMissFrames;
        public List<FrameDataCircle>[] GetUpDownFrames;
        public List<FrameDataCircle>[] GetUpUpFrames;
        public List<FrameDataCircle>[] ToCrouchFrames;
        public List<FrameDataCircle>[] ToShieldFrames;
        public List<FrameDataCircle>[] AerialEndLagFrames;

        public bool LoadData(string path)
        {
            if(!File.Exists(path)){
                Console.WriteLine("File " + path + "does not exist");
                return false;
            }
            else {
                try {
                    using(StreamReader reader = new StreamReader(path))
                    {
                        //BASIC NUMBERS
                        Height = AddShortData(reader);
                        Width = AddShortData(reader);
                        NumJumps = AddShortData(reader);
                        ShortHopForce = AddFloatData(reader);
                        FirstJumpForce = AddFloatData(reader);
                        AuxJumpForce = AddFloatData(reader);

                        FairEndLag = AddShortData(reader);
                        UairEndLag = AddShortData(reader);
                        DairEndLag = AddShortData(reader);
                        BairEndLag = AddShortData(reader);
                        NairEndLag = AddShortData(reader);

                        WalkSpeed = AddFloatData(reader);
                        RunSpeed = AddFloatData(reader);
                        DashSpeed = AddFloatData(reader);

                        GravityForce = AddFloatData(reader);

                        //THE FRAME DATA
                        WalkFrames = AddFrameData(reader);
                        RunFrames = AddFrameData(reader);
                        DashFrames = AddFrameData(reader);
                        JumpFrames = AddFrameData(reader);
                        AirJumpForwardFrames = AddFrameData(reader);
                        AirJumpBackWardFrames = AddFrameData(reader);
                        JabFrames = AddFrameData(reader);
                        JabComboFrames = AddFrameData(reader);
                        DashAttackFrames = AddFrameData(reader);
                        FTiltFrames = AddFrameData(reader);
                        UTiltFrames = AddFrameData(reader);
                        DTiltFrames = AddFrameData(reader);
                        FSmashFrames = AddFrameData(reader);
                        USmashFrames = AddFrameData(reader);
                        DSmashFrames = AddFrameData(reader);
                        FairFrames = AddFrameData(reader);
                        UairFrames = AddFrameData(reader);
                        DairFrames = AddFrameData(reader);
                        BairFrames = AddFrameData(reader);
                        NairFrames = AddFrameData(reader);
                        GrabFrames = AddFrameData(reader);
                        DashGrabFrames = AddFrameData(reader);
                        AirDodgeFrames = AddFrameData(reader);
                        SpotDodgeFrames = AddFrameData(reader);
                        RollForwardFrames = AddFrameData(reader);
                        RollBackwardFrames = AddFrameData(reader);
                        TechInPlaceFrames = AddFrameData(reader);
                        TechForwardFrames = AddFrameData(reader);
                        TechBackFrames = AddFrameData(reader);
                        TechMissFrames = AddFrameData(reader);
                        GetUpUpFrames = AddFrameData(reader);
                        GetUpDownFrames = AddFrameData(reader);
                        ToCrouchFrames = AddFrameData(reader);
                        ToShieldFrames = AddFrameData(reader);
                        AerialEndLagFrames = AddFrameData(reader);
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
        private short AddShortData(StreamReader reader)
        {
            string line = reader.ReadLine();
            while(line.Substring(0,1) == "#")
            {
                line = reader.ReadLine();
            }
            return Convert.ToInt16(line);
        }
        private float AddFloatData(StreamReader reader)
        {
            string line = reader.ReadLine();
            while(line.Substring(0,1) == "#")
            {
                line = reader.ReadLine();
            }
            return Convert.ToSingle(line);
        }
        private List<FrameDataCircle>[] AddFrameData(StreamReader reader)
        {
            string line = reader.ReadLine();
            while(line.Substring(0,1) == "#")
            {
                line = reader.ReadLine();
            }
            int numFrames = Convert.ToInt32(line);
            int numHitboxes;
            var frames = new List<FrameDataCircle>[numFrames];
            for (int i = 0; i < numFrames; i++)
            {
                numHitboxes = Convert.ToInt32(reader.ReadLine());
                for (int j = 0; j < numHitboxes; j++)
                {
                    string[] data = reader.ReadLine().Split(' ');
                    frames[i].Add(
                        new FrameDataCircle(
                            new Vector2(Convert.ToSingle(data[0]), Convert.ToSingle(data[1])),
                            new Vector2(Convert.ToSingle(data[2]), Convert.ToSingle(data[3])),
                            Convert.ToSingle(data[4]),
                            Convert.ToBoolean(data[5])));

                }
            }
            return frames;
        }
    }
}
