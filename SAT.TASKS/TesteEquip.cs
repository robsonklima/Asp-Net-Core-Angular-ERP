using Microsoft.Extensions.Options;
using NLog;
using SAT.SERVICES;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;

namespace SAT.TASKS
{
    public class TesteEquip
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IEquipamentoContratoService _equipamentoContratoService;

        public TesteEquip(IEquipamentoContratoService equipamentoContratoService)
        {
            _equipamentoContratoService = equipamentoContratoService;

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "Logs\\Logs.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
            LogManager.Configuration = config;
        }

        public void LogEquip()
        {
            try
            {
                var equip = "equip";

                _logger.Info(equip);
            }
            catch (System.Exception ex)
            {
                _logger.Info(ex);
            }
        }
    }
}