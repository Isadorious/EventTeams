using NLog;
using System;
using System.IO;
using Torch;
using Torch.API;

namespace EventTeams
{
    public class EventTeams : TorchPluginBase
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private Persistent<EventTeamsConfig> _config;
        public EventTeamsConfig Config => _config?.Data;

        public Logger Logger = Log;
        public void Save() => _config.Save();

        /// <inheritdoc />
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            SetupConfig();
        }

        private void SetupConfig()
        {
            var configFile = Path.Combine(StoragePath, "EventTeams.cfg");

            try
            {
                _config = Persistent<EventTeamsConfig>.Load(configFile);
            }
            catch(Exception e)
            {
                Log.Warn(e);
            }

            if(_config?.Data == null)
            {
                Log.Info("Create Default config because none was found");

                _config = new Persistent<EventTeamsConfig>(configFile, new EventTeamsConfig());
                _config.Save();
            }
        }

        public void ReloadConfig()
        {
            var configFile = Path.Combine(StoragePath, "EventTeams.cfg");

            _config = Persistent<EventTeamsConfig>.Load(configFile);

        }

    }
}
