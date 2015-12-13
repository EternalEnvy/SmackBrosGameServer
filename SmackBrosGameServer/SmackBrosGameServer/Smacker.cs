﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class Smacker
    {
        public bool isFacingRight;
        protected Vector2 pos;
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

    }
    enum SmackerStates
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
        SquatRv,
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
        DownWaitUp,
        DownDamageUp,
        DownStandUp,
        DownSpotUp,
        DownAttackU,
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

    } 
}
