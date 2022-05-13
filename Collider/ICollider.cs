using System;
using System.Collections.Generic;
using System.Text;

namespace _2DPlatformerRobot.Collider
{
    public interface ICollider
    {
        string Name();
        void CollisionWith(ICollider other);
        bool CollidesWith(ICollider other);
        ICollider GetCollider();
    }
}
