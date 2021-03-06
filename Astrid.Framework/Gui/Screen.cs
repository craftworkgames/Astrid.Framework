﻿using System.Collections.Generic;
using Astrid.Animations;
using Astrid.Core;

namespace Astrid.Gui
{
    public interface IScreenContext : IDeviceManager
    {
        Camera Camera { get; }
        Viewport Viewport { get; }
        AssetManager AssetManager { get; }
        void SetScreen(Screen screen);
    }

    public abstract class Screen : IDeviceManager
    {
        protected Screen(IScreenContext game)
        {
            _layers = new List<ScreenLayer>();

            Game = game;
            ClearColor = Color.CornflowerBlue;
            Animations = new AnimationSystem();
        }

        protected IScreenContext Game { get; private set; }
        
        public Color ClearColor { get; set; }
        public AnimationSystem Animations { get; private set; }

        private readonly List<ScreenLayer> _layers;
        public IList<ScreenLayer> Layers
        {
            get { return _layers; }
        }

        public AssetManager AssetManager
        {
            get { return Game.AssetManager; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return Game.GraphicsDevice; }
        }

        public InputDevice InputDevice
        {
            get { return Game.InputDevice; }
        }

        public AudioDevice AudioDevice
        {
            get { return Game.AudioDevice; }
        }

        public void SetScreen(Screen screen)
        {
            Game.SetScreen(screen);
        }

        public virtual void Show() { }
        public virtual void Hide() { }
        public virtual void Resize(int width, int height) { }
        public virtual void Pause() { }
        public virtual void Resume() { }

        public virtual void Update(float deltaTime)
        {
            Animations.Update(deltaTime);

            foreach (var layer in Layers)
                layer.Update(deltaTime, InputDevice);
        }

        public virtual void Render(float deltaTime)
        {
            GraphicsDevice.Clear(ClearColor);

            foreach (var layer in _layers)
                layer.Render(deltaTime);
        }
    }
}
