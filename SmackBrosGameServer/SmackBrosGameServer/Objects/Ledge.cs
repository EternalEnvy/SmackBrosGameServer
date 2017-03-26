using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public struct Ledge
    {
        const int InvicibilityTime = 150; //frames
        bool _hasPlayerActive;
        bool _isFacingRight;
        public Vector2 MyOrigin;
        float _radius;
        public bool CollideLedge(Vector2 pos, bool facingRight)
        {
            if((pos - MyOrigin).Length < _radius && (_isFacingRight == facingRight))
            {
                _hasPlayerActive = true;
                return true;
            }
            return false;
        }
    }
}
