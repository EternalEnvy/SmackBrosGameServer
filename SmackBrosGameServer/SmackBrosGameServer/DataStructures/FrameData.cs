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

        public float gravityForce;

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
            Vector2 position;
            Vector2 characterMovement;
            float radius;
            bool modifier; 
            public FrameDataCircle(Vector2 position, Vector2 characterMovement, float radius, bool modifier)
            {
                this.position = position;
                this.characterMovement = characterMovement;
                this.radius = radius;
                this.modifier = modifier;
            }
        }
        //Position relative to char, movement, radius, Special Modifier (use based on context)
        public List<FrameDataCircle>[] walkFrames;
        public List<FrameDataCircle>[] runFrames;
        public List<FrameDataCircle>[] dashFrames;
        public List<FrameDataCircle>[] jumpFrames;
        public List<FrameDataCircle>[] airJumpForwardFrames;
        public List<FrameDataCircle>[] airJumpBackWardFrames;
        public List<FrameDataCircle>[] jabFrames;
        public List<FrameDataCircle>[] jabComboFrames;
        public List<FrameDataCircle>[] fTiltFrames;
        public List<FrameDataCircle>[] uTiltFrames;
        public List<FrameDataCircle>[] dTiltFrames;
        public List<FrameDataCircle>[] dashAttackFrames;
        public List<FrameDataCircle>[] uSmashFrames;
        public List<FrameDataCircle>[] fSmashFrames;
        public List<FrameDataCircle>[] dSmashFrames;
        public List<FrameDataCircle>[] fairFrames;
        public List<FrameDataCircle>[] uairFrames;
        public List<FrameDataCircle>[] dairFrames;
        public List<FrameDataCircle>[] bairFrames;
        public List<FrameDataCircle>[] nairFrames;
        public List<FrameDataCircle>[] grabFrames;
        public List<FrameDataCircle>[] dashGrabFrames;
        public List<FrameDataCircle>[] airDodgeFrames;
        public List<FrameDataCircle>[] spotDodgeFrames;
        public List<FrameDataCircle>[] rollForwardFrames;
        public List<FrameDataCircle>[] rollBackwardFrames;
        public List<FrameDataCircle>[] techInPlaceFrames;
        public List<FrameDataCircle>[] techForwardFrames;
        public List<FrameDataCircle>[] techBackFrames;
        public List<FrameDataCircle>[] techMissFrames;
        public List<FrameDataCircle>[] getUpDownFrames;
        public List<FrameDataCircle>[] getUpUpFrames;
        public List<FrameDataCircle>[] toCrouchFrames;
        public List<FrameDataCircle>[] toShieldFrames;
        public List<FrameDataCircle>[] aerialEndLagFrames;

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

                        gravityForce = AddFloatData(reader);

                        //THE FRAME DATA
                        walkFrames = AddFrameData(reader);
                        runFrames = AddFrameData(reader);
                        dashFrames = AddFrameData(reader);
                        jumpFrames = AddFrameData(reader);
                        airJumpForwardFrames = AddFrameData(reader);
                        airJumpBackWardFrames = AddFrameData(reader);
                        jabFrames = AddFrameData(reader);
                        jabComboFrames = AddFrameData(reader);
                        dashAttackFrames = AddFrameData(reader);
                        fTiltFrames = AddFrameData(reader);
                        uTiltFrames = AddFrameData(reader);
                        dTiltFrames = AddFrameData(reader);
                        fSmashFrames = AddFrameData(reader);
                        uSmashFrames = AddFrameData(reader);
                        dSmashFrames = AddFrameData(reader);
                        fairFrames = AddFrameData(reader);
                        uairFrames = AddFrameData(reader);
                        dairFrames = AddFrameData(reader);
                        bairFrames = AddFrameData(reader);
                        nairFrames = AddFrameData(reader);
                        grabFrames = AddFrameData(reader);
                        dashGrabFrames = AddFrameData(reader);
                        airDodgeFrames = AddFrameData(reader);
                        spotDodgeFrames = AddFrameData(reader);
                        rollForwardFrames = AddFrameData(reader);
                        rollBackwardFrames = AddFrameData(reader);
                        techInPlaceFrames = AddFrameData(reader);
                        techForwardFrames = AddFrameData(reader);
                        techBackFrames = AddFrameData(reader);
                        techMissFrames = AddFrameData(reader);
                        getUpUpFrames = AddFrameData(reader);
                        getUpDownFrames = AddFrameData(reader);
                        toCrouchFrames = AddFrameData(reader);
                        toShieldFrames = AddFrameData(reader);
                        aerialEndLagFrames = AddFrameData(reader);
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
            var Frames = new List<FrameDataCircle>[numFrames];
            for (int i = 0; i < numFrames; i++)
            {
                numHitboxes = Convert.ToInt32(reader.ReadLine());
                for (int j = 0; j < numHitboxes; j++)
                {
                    string[] data = reader.ReadLine().Split(' ');
                    Frames[i].Add(
                        new FrameDataCircle(
                            new Vector2(Convert.ToSingle(data[0]), Convert.ToSingle(data[1])),
                            new Vector2(Convert.ToSingle(data[2]), Convert.ToSingle(data[3])),
                            Convert.ToSingle(data[4]),
                            Convert.ToBoolean(data[5])));

                }
            }
            return Frames;
        }
    }
}
