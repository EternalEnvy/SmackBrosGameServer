﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SmackBrosGameServer
{
    public partial class GameServerWindow : Window
    {
        const int MaxStickExtension = 100;
        public void Update()
        {
            if(serverInitialized && GameMetadata.FrameNumber >= 0)
            {
                for (int i = 0; i < ClientInputBuffer.Count; i++)
                {
                    if(ClientInputBuffer[i].FrameNumber <= GameMetadata.FrameNumber)
                    {
                        var item = ClientInputBuffer[i];
                        UpdateFromClientGamepad(item);
                        ClientInputBuffer.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach(Smacker s in smackerList)
            {
                for(int i = 0; i < playerInputs.Count; i++)
                {
                    if (s.SmackerId == playerInputs[i].Item1)
                    {
                        HandleStateFromInput(s, playerInputs[i].Item2);
                        playerInputs.RemoveAt(i);
                    }
                }
            }
        }
        public void HandleStateFromInput(Smacker smacker, Input input)
        {
            if((smacker.State == SmackerState.DeadDown)||(smacker.State == SmackerState.DeadRight)||(smacker.State == SmackerState.DeadLeft) ||
                (smacker.State == SmackerState.DeadUpStar) ||(smacker.State == SmackerState.DeadUpCamera) ||(smacker.State == SmackerState.Rebirth) ||
                (smacker.State == SmackerState.DamageAir) ||(smacker.State == SmackerState.DamageGround) ||(smacker.State == SmackerState.ShieldBreak) ||
                (smacker.State == SmackerState.ShieldStunned) || (smacker.State == SmackerState.NoTechBounceUp) || (smacker.State == SmackerState.Helpless))
            {
                return;
            }
            //ANALOG STICK INPUTS
            if(input.up > 0)
            {
                if(input.up > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.up > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.down > 0)
            {
                if(input.down > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.down > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.right > 0)
            {
                if(input.right > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.right > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.left > 0)
            {
                if(input.left > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.left > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }

            //C-STICK INPUTS
            if(input.upC > 0)
            {
                if(input.upC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.upC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.downC > 0)
            {
                if(input.downC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.downC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.rightC > 0)
            {
                if(input.rightC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.rightC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.leftC > 0)
            {
                if(input.leftC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.leftC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }

            //TRIGGERS
            if(input.LeftTrigger > 0)
            {
                //if()
            }
            else if(input.RightTrigger > 0)
            {

            }
                        
            //ANALOG TRIGGER
            if(input.RightAnalog)
            {

            }
            else if(input.LeftAnalog)
            {

            }

            //BUTTONS
            if(input.A)
            {

            }
            else if(input.B)
            {

            }
            else if(input.X)
            {

            }
            else if(input.Y)
            {

            }
        }
    }
}
