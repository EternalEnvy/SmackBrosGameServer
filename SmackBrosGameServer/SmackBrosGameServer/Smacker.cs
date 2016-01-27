using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class Smacker
    {
        private FrameData frameData;
        private DamageData damageData;
        private int maxJumps;
        private int curJumps;
        public SmackerState state;
        public int smackerID;
        public bool isFacingRight;
        public int frameDurationCurState = 0;
        protected Vector2 pos;
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
    }
    enum SmackerState
    {
        DeadDown,
        DeadRight,
        DeadLeft,
        DeadUpStar,
        DeadUpCamera,
        Rebirth,
        RebirthWait,
        Standing,
        WalkSlow,
        WalkMedium,
        WalkFast,
        Turn,
        TurnRun,
        Dash,
        Run,
        RunBrake,
        JumpSquat,
        JumpForward,
        JumpBackward,
        JumpAirF,
        JumpAirB,
        Fall,
        FallF,
        FallB,
        FallAerial,
        FallAerialF,
        FallAerialB,
        FallSpecial,
        FallSpecialF,
        FallSpecialB,
        Tumbling,
        Squat,
        SquatWait,
        SquatRv,//going from crouch to stand
        Landing,
        LandingFallSpecial,
        Attack11,
        Attack12,
        Attack13,
        Attack100Start,
        Attack100Loop,
        Attack100End,
        AttackDash,
        AttackUtilt,
        AttackDtilt,
        AttackFtilt,
        AttackFsmash,
        AttackDsmash,
        AttackUsmash,
        AttackAirNeutral,
        AttackAirUp,
        AttackAirForward,
        AttackAirBack,
        AttackAirDown,
        LandingAirNeutral,
        LandingAirUp,
        LandingAirForward,
        LandingAirBack,
        LandingAirDown,
        Special,
        DamageAir,
        DamageGround,
        ShieldOn,
        ShieldHold,
        ShieldBreak,
        ShieldOff,
        ShieldStunned,
        ShieldReflect,
        NoTechBounceUp,
        DownWaitUp, //Laying on ground facing up
        DownDamageUp, //Getting hit laying on ground facing up
        DownStandUp,
        DownSpotUp,
        DownAttackU, //  Get up attack from ground face up
        DownBackU,
        DownForwardU,
        NoTechBounceDown,
        DownWaitDown,
        DownDamageDown,
        TechInPlace,
        TechForward,
        TechBack,
        TechWall,
        Grab,
        GrabPull, //pulling character in after grab
        GrabDash, //boost grab
        GrabDashPull, //pull after boost grab
        GrabWait,
        GrabPummel,
        GrabBreak, //grab broken
        ThrowUp,
        ThrowDown,
        ThrowForward,
        ThrowBack,
        CapturePull,
        CaptureWait,
        CaptureDamage,
        CaptureBreak,
        CaptureThrowUp,
        CaptureThrowDown,
        CaptureThrowForward,
        CaptureThrowBack,
        AirDodge,
        Helpless,
    } 
}
