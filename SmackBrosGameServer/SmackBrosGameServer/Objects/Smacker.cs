using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class Smacker
    {
        protected Vector2 pos;
        protected Vector2 velocity;
        
        private FrameData frameData;
        private DamageData damageData;
        private SpecialMoveData specialData;

        private int maxJumps;
        private int curJumps;

        public SmackerState state;
        public short smackerID;
        public int frameDurationCurState = 0;
        public List<Hurtbox>[] currentStateHurtboxes; 
        public List<Hitbox>[] currentStateHitboxes;
        public int maxDurationCurState = 0;
        public bool isFacingRight;
        
        public Smacker(string dataFilePath)
        {
            frameData.LoadData(dataFilePath + "\\framedata.txt");
            damageData.LoadData(dataFilePath + "\\damagedata.txt");
        }
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }
        public bool IsAerial
        {
            get
            {
                if (state == SmackerState.Fall || state == SmackerState.FallAerial || state == SmackerState.FallAerialB ||
                    state == SmackerState.FallAerialF || state == SmackerState.Tumbling)
                {
                    return true;
                }
                else return false;
            }
        }
        public bool IsGrounded
        {
            get
            {
                 if(state == SmackerState.WalkFast || state == SmackerState.WalkMedium || state == SmackerState.WalkSlow ||
                    state == SmackerState.Standing || state == SmackerState.SquatWait || state == SmackerState.SquatRv ||
                     state == SmackerState.Squat || state == SmackerState.TurnRun || state == SmackerState.Turn || 
                     state == SmackerState.Run || state == SmackerState.RunBrake)
                 {
                     return true;
                 }
                 else return false;
            }
        }
        public bool StateInDefinite
        {
            get
            {
                if(state == SmackerState.Standing || state == SmackerState.WalkSlow || state == SmackerState.WalkMedium || 
                    state == SmackerState.WalkFast || state == SmackerState.Run || state == SmackerState.Fall ||
                    state == SmackerState.FallF || state == SmackerState.FallB  || state == SmackerState.FallAerial || 
                    state == SmackerState.FallAerialF || state == SmackerState.FallAerialB || state == SmackerState.FallSpecial || 
                    state == SmackerState.FallSpecialB || state == SmackerState.FallSpecialF || state == SmackerState.Tumbling ||
                    state == SmackerState.SquatWait || state == SmackerState.DownWaitUp || state == SmackerState.DownWaitDown ||
                    state == SmackerState.CaptureWait || state == SmackerState.LedgeHang || state == SmackerState.ShieldHold ||
                    state == SmackerState.GrabWait || state == SmackerState.LedgeHang || state == SmackerState.Helpless)
                    return true;
                else
                    return false;
            }
        }
        public int EnumeratedState
        {
            get
            {
                return (int)state;
            }
            set
            {
                state = (SmackerState)value;
            }
        }
        public bool CanInput
        {
            get 
            {
                if (state == SmackerState.DeadDown || state == SmackerState.DeadRight || state == SmackerState.DeadLeft ||
                    state == SmackerState.DeadUpStar || state == SmackerState.DeadUpCamera || state == SmackerState.Rebirth ||
                    state == SmackerState.DamageAir || state == SmackerState.DamageGround || state == SmackerState.ShieldBreak ||
                    state == SmackerState.ShieldStunned || state == SmackerState.NoTechBounceUp || state == SmackerState.Helpless||
                    state == SmackerState.NoTechBounceDown || state == SmackerState.TechForward || state == SmackerState.TechBack)
                {
                    return false;
                }
                else return true;
            }
        }
        public void StartJump()
        {
            if(curJumps > 0)
            {
                if(IsGrounded)
                {

                }
                else if(IsAerial)
                {

                }
            }
        }
        public void LoadHitboxes()
        {
            var hitboxes = damageData.StateNumToDamageData(this.EnumeratedState);
            if (hitboxes != null)
            {
                currentStateHitboxes = hitboxes;
            }
            else return;
        }
        public void UpdateFromHitboxes(Stage stage, List<Tuple<int, List<Tuple<Vector2,Vector2,float,float,int>>>> hitboxes)
        {
            //remove hitboxes that are from the current player, condense the list for simplification
            var hb = hitboxes.Where(x => x.Item1 != smackerID).Select(y => y.Item2).ToList();
            velocity.Y += frameData.gravityForce;
            pos += velocity;
        }
        public void UpdateFromInput(Input input, GameData GameMetadata)
        {
            if (input.Start)
            {
                //pause the game
                if (GameMetadata.pauseAlpha > 100)
                {
                    GameMetadata.GamePaused = !GameMetadata.GamePaused;
                    GameMetadata.pauseAlpha = 0;
                }
            }
            frameDurationCurState++;
            if(!StateInDefinite && frameDurationCurState > maxDurationCurState)
            {
                if(IsAerial)
                { 
                    if (state == SmackerState.AttackAirBack)
                        state = SmackerState.FallAerialB;
                    else if (state == SmackerState.AttackAirForward)
                        state = SmackerState.FallAerialF;
                    else if (state == SmackerState.AttackAirDown || state == SmackerState.AttackAirUp || state == SmackerState.AttackAirNeutral)
                        state = SmackerState.FallAerial;
                    else if (state == SmackerState.AirDodge)
                        state = SmackerState.Helpless;

                }
                else if(IsGrounded)
                {
                    if(state == SmackerState.JumpSquat)
                    {
                        if(input.up == 0 && !input.X && !input.Y)
                        {
                            //shorthop
                            
                        }
                        else
                        {
                            //regular hop
                        }
                    }
                    else
                        state = SmackerState.Standing;
                }
            } 
        } 
        public void UpdateSpecial()
        {

        }

        public Tuple<int, List<Hurtbox>> MyHurtboxThisFrame()
        {
            if (currentStateHurtboxes.Length > frameDurationCurState)
            {
                return new Tuple<int, List<Hurtbox>>(smackerID, currentStateHurtboxes[frameDurationCurState]);
            }
            else return null;
        }
        public Tuple<int, List<Hitbox>> HitboxThisFrame()
        {
            if (currentStateHitboxes.Length > frameDurationCurState)
            {
                return new Tuple<int, List<Hitbox>>(smackerID, currentStateHitboxes[frameDurationCurState]);
            }
            else return null;
        }
 
    }
    enum SmackerState
    {
        DeadDown,//0
        DeadRight,
        DeadLeft,
        DeadUpStar,
        DeadUpCamera,
        Rebirth,//5
        RebirthWait,
        Standing,
        WalkSlow,
        WalkMedium,
        WalkFast,//10
        Turn,
        TurnRun,
        Dash,
        Run,
        RunBrake,//15
        JumpSquat,
        JumpForward,
        JumpBackward,
        JumpAirF,
        JumpAirB,//20
        Fall,
        FallF,
        FallB,
        FallAerial,
        FallAerialF,//25
        FallAerialB,
        FallSpecial,
        FallSpecialF,
        FallSpecialB,
        Tumbling,//30
        Squat,
        SquatWait,
        SquatRv,//going from crouch to stand
        Landing,
        LandingFallSpecial,//35
        Attack11,
        Attack12,
        Attack13,
        Attack100Start,
        Attack100Loop,//40
        Attack100End,
        AttackDash,
        AttackUtilt,
        AttackDtilt,
        AttackFtilt,//45
        AttackFsmash,
        AttackDsmash,
        AttackUsmash,
        AttackAirNeutral,
        AttackAirUp,//50
        AttackAirForward,
        AttackAirBack,
        AttackAirDown,
        LandingAirNeutral,
        LandingAirUp,//55
        LandingAirForward,
        LandingAirBack,
        LandingAirDown,
        Special,
        DamageAir,//60
        DamageGround,
        ShieldOn,
        ShieldHold,
        ShieldBreak,
        ShieldOff,//65
        ShieldStunned,
        ShieldReflect,
        NoTechBounceUp,
        DownWaitUp, //Laying on ground facing up
        DownDamageUp, //Getting hit laying on ground facing up //70
        DownStandUp,
        DownSpotUp,
        DownAttackU, //  Get up attack from ground face up
        DownBackU,
        DownForwardU,//75
        NoTechBounceDown,
        DownWaitDown,
        DownDamageDown,
        TechInPlace,
        TechForward,//80
        TechBack,
        TechWall,
        Grab,
        GrabPull, //pulling character in after grab
        GrabDash, //boost grab //80
        GrabDashPull, //pull after boost grab
        GrabWait,
        GrabPummel,
        GrabBreak, //grab broken
        ThrowUp, //85
        ThrowDown,
        ThrowForward,
        ThrowBack,
        CapturePull,
        CaptureWait,//90
        CaptureDamage,
        CaptureBreak,
        CaptureThrowUp,
        CaptureThrowDown,
        CaptureThrowForward,//95
        CaptureThrowBack,
        AirDodge,
        Helpless,
        LedgeHang,
    } 
}
