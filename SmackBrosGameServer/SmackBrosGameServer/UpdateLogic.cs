using System;
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
            if(ServerInitialized && _gameMetadata.FrameNumber >= 0)
            {
                for (int i = 0; i < _clientInputBuffer.Count; i++)
                {
                    if(_clientInputBuffer[i].FrameNumber <= _gameMetadata.FrameNumber)
                    {
                        var item = _clientInputBuffer[i];
                        UpdateFromClientGamepad(item);
                        _clientInputBuffer.RemoveAt(i);
                        i--;
                    }
                }
            }
            foreach(Smacker s in _smackerList)
            {
                for(int i = 0; i < _playerInputs.Count; i++)
                {
                    if (s.SmackerId == _playerInputs[i].Item1)
                    {
                        HandleStateFromInput(s, _playerInputs[i].Item2);
                        _playerInputs.RemoveAt(i);
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
            if(input.Up > 0)
            {
                if(input.Up > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.Up > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.Down > 0)
            {
                if(input.Down > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.Down > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.Right > 0)
            {
                if(input.Right > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.Right > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if(input.Left > 0)
            {
                if(input.Left > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.Left > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }

            //C-STICK INPUTS
            if(input.UpC > 0)
            {
                if(input.UpC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.UpC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.DownC > 0)
            {
                if(input.DownC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.DownC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.RightC > 0)
            {
                if(input.RightC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.RightC > MaxStickExtension / 3)
                {

                }
                else
                {

                }
            }
            else if (input.LeftC > 0)
            {
                if(input.LeftC > MaxStickExtension * 2 / 3)
                {

                }
                else if (input.LeftC > MaxStickExtension / 3)
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
