using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public class Smacker : IGameObject
    {
        const float HitlagMultiplier = 0.4f;
        protected Vector2 Pos;
        protected Vector2 Velocity;
        
        private FrameData _frameData;
        private DamageData _damageData;
        private SpecialMoveData _specialData;

        private int _maxJumps;
        private int _curJumps;

        public SmackerState State;
        public short SmackerId;
        public int FrameDurationCurState = 0;
        public List<Hurtbox>[] CurrentStateHurtboxes; 
        public List<DamageCircleFrame>[] CurrentStateHitboxes;
        public int MaxDurationCurState = 0;
        public bool IsFacingRight;

        public float HitstunDuration {get; set;}
        public int Damage {get; set;}
        
        public Smacker(string dataFilePath)
        {
            _frameData.LoadData(dataFilePath + "\\framedata.txt");
            _damageData.LoadData(dataFilePath + "\\damagedata.txt");
        }
        public Vector2 Position
        {
            get { return Pos; }
            set { Pos = value; }
        }
        public bool IsAerial
        {
            get
            {
                if (State == SmackerState.Fall || State == SmackerState.FallAerial || State == SmackerState.FallAerialB ||
                    State == SmackerState.FallAerialF || State == SmackerState.Tumbling)
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
                 if(State == SmackerState.WalkFast || State == SmackerState.WalkMedium || State == SmackerState.WalkSlow ||
                    State == SmackerState.Standing || State == SmackerState.SquatWait || State == SmackerState.SquatRv ||
                     State == SmackerState.Squat || State == SmackerState.TurnRun || State == SmackerState.Turn || 
                     State == SmackerState.Run || State == SmackerState.RunBrake)
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
                if(State == SmackerState.Standing || State == SmackerState.WalkSlow || State == SmackerState.WalkMedium || 
                    State == SmackerState.WalkFast || State == SmackerState.Run || State == SmackerState.Fall ||
                    State == SmackerState.FallF || State == SmackerState.FallB  || State == SmackerState.FallAerial || 
                    State == SmackerState.FallAerialF || State == SmackerState.FallAerialB || State == SmackerState.FallSpecial || 
                    State == SmackerState.FallSpecialB || State == SmackerState.FallSpecialF || State == SmackerState.Tumbling ||
                    State == SmackerState.SquatWait || State == SmackerState.DownWaitUp || State == SmackerState.DownWaitDown ||
                    State == SmackerState.CaptureWait || State == SmackerState.LedgeHang || State == SmackerState.ShieldHold ||
                    State == SmackerState.GrabWait || State == SmackerState.LedgeHang || State == SmackerState.Helpless)
                    return true;
                else
                    return false;
            }
        }
        public int EnumeratedState
        {
            get
            {
                return (int)State;
            }
            set
            {
                State = (SmackerState)value;
            }
        }
        public bool CanInput
        {
            get 
            {
                if (State == SmackerState.DeadDown || State == SmackerState.DeadRight || State == SmackerState.DeadLeft ||
                    State == SmackerState.DeadUpStar || State == SmackerState.DeadUpCamera || State == SmackerState.Rebirth ||
                    State == SmackerState.DamageAir || State == SmackerState.DamageGround || State == SmackerState.ShieldBreak ||
                    State == SmackerState.ShieldStunned || State == SmackerState.NoTechBounceUp || State == SmackerState.Helpless||
                    State == SmackerState.NoTechBounceDown || State == SmackerState.TechForward || State == SmackerState.TechBack)
                {
                    return false;
                }
                else return true;
            }
        }
        public void StartJump()
        {
            if(_curJumps > 0)
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
            var hitboxes = _damageData.StateNumToDamageData(this.EnumeratedState);
            if (hitboxes != null)
            {
                CurrentStateHitboxes = hitboxes;
            }
            else return;
        }
        public void UpdateFromHitboxes(Stage stage, List<Tuple<int, List<Tuple<Vector2,Vector2,float,float,int>>>> hitboxes)
        {
            //remove hitboxes that are from the current player, condense the list for simplification
            var hb = hitboxes.Where(x => x.Item1 != SmackerId).Select(y => y.Item2).ToList();
            Velocity.Y += _frameData.GravityForce;
            Pos += Velocity;
        }
        private void DealDamage(int damage)
        {
            HitstunDuration += HitlagMultiplier * damage;
            Damage += damage;
        }
        public void UpdateFromInput(Input input, GameData gameMetadata)
        {
            if (input.Start)
            {
                //pause the game
                if (gameMetadata.PauseAlpha > 100)
                {
                    gameMetadata.GamePaused = !gameMetadata.GamePaused;
                    gameMetadata.PauseAlpha = 0;
                }
            }
            FrameDurationCurState++;
            if(!StateInDefinite && FrameDurationCurState > MaxDurationCurState)
            {
                if(IsAerial)
                { 
                    if (State == SmackerState.AttackAirBack)
                        State = SmackerState.FallAerialB;
                    else if (State == SmackerState.AttackAirForward)
                        State = SmackerState.FallAerialF;
                    else if (State == SmackerState.AttackAirDown || State == SmackerState.AttackAirUp || State == SmackerState.AttackAirNeutral)
                        State = SmackerState.FallAerial;
                    else if (State == SmackerState.AirDodge)
                        State = SmackerState.Helpless;

                }
                else if(IsGrounded)
                {
                    if(State == SmackerState.JumpSquat)
                    {
                        if(input.Up == 0 && !input.X && !input.Y)
                        {
                            //shorthop
                            
                        }
                        else
                        {
                            //regular hop
                        }
                    }
                    else
                        State = SmackerState.Standing;
                }
            } 
        } 
        public void UpdateSpecial()
        {

        }

        public Tuple<int, List<Hurtbox>> MyHurtboxThisFrame()
        {
            if (CurrentStateHurtboxes.Length > FrameDurationCurState)
            {
                return new Tuple<int, List<Hurtbox>>(SmackerId, CurrentStateHurtboxes[FrameDurationCurState]);
            }
            else return null;
        }
        public Tuple<int, List<DamageCircleFrame>> HitboxThisFrame()
        {
            if (CurrentStateHitboxes.Length > FrameDurationCurState)
            {
                return new Tuple<int, List<DamageCircleFrame>>(SmackerId, CurrentStateHitboxes[FrameDurationCurState]);
            }
            return null;
        }


        public double IntersectsRay(Vector2 origin, Vector2 direction)
        {
            
        }
    }
    public enum SmackerState
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
