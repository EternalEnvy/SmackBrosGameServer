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

        //Position relative to char, movement, radius, Special Modifier (use based on context)
        public List<Tuple<Vector2, Vector2, float, bool>>[] walkFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] runFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dashFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] jumpFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] airJumpForwardFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] airJumpBackWardFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] jabFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] jabComboFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] fTiltFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] uTiltFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dTiltFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dashAttackFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] uSmashFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] fSmashFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dSmashFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] fairFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] uairFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dairFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] bairFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] nairFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] grabFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] dashGrabFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] airDodgeFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] spotDodgeFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] rollForwardFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] rollBackwardFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] techInPlaceFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] techForwardFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] techBackFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] techMissFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] getUpDownFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] getUpUpFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] toCrouchFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] toShieldFrames;
        public List<Tuple<Vector2, Vector2, float, bool>>[] aerialEndLagFrames;

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
        private List<Tuple<Vector2, Vector2, float, bool>>[] AddFrameData(StreamReader reader)
        {
            string line = reader.ReadLine();
            while(line.Substring(0,1) == "#")
            {
                line = reader.ReadLine();
            }
            int numFrames = Convert.ToInt32(line);
            int numHitboxes;
            var Frames = new List<Tuple<Vector2, Vector2, float, bool>>[numFrames];
            for (int i = 0; i < numFrames; i++)
            {
                numHitboxes = Convert.ToInt32(reader.ReadLine());
                for (int j = 0; j < numHitboxes; j++)
                {
                    string[] data = reader.ReadLine().Split(' ');
                    Frames[i].Add(
                        new Tuple<Vector2, Vector2, float, bool>(
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
