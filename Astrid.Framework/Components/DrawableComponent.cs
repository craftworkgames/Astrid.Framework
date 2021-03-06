﻿using Astrid.Core;

namespace Astrid.Components
{
    public abstract class DrawableComponent : Component
    {
        public abstract Rectangle GetBoundingRectangle();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}