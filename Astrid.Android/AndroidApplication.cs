using Astrid.Windows;
using OpenTK.Platform.Android;

namespace Astrid.Android
{
    public class AndroidApplication : ApplicationBase
    {
        private readonly AndroidApplicationConfig _config;

        public AndroidApplication(AndroidApplicationConfig config)
        {
            _config = config;
        }

        private OpenTKGameView _view;
        public AndroidGameView View
        {
            get { return _view; }
        }

        public override AssetManager CreateAssetManager(IDeviceManager deviceManager)
        {
            return new AndroidAssetManager(deviceManager, _config.Activity);
        }

        private GLGraphicsDevice _graphicsDevice;

        public override GraphicsDevice CreateGraphicsDevice()
        {
            var width = _config.Activity.Resources.DisplayMetrics.WidthPixels;
            var height = _config.Activity.Resources.DisplayMetrics.HeightPixels;
            _graphicsDevice = new GLGraphicsDevice(width, height);
            return _graphicsDevice;
        }

        private AndroidInputDevice _inputDevice;
        public override InputDevice CreateInputDevice(IInputDeviceContext context)
        {
            _inputDevice = new AndroidInputDevice(context);
            return _inputDevice;
        }

        public override AudioDevice CreateAudioDevice()
        {
            return new AndroidAudioDevice();
        }
        
        public override void Run(IApplicationListener game)
        {
            _view = new OpenTKGameView(_config.Activity, game, _graphicsDevice, _config);
            _view.RequestFocus();
            _view.FocusableInTouchMode = true;
            _view.SetOnTouchListener(_inputDevice.Listener);
        }

        public void Pause()
        {
            _view.Pause();
        }

        public void Resume()
        {
            _view.Resume();
        }
    }
}
