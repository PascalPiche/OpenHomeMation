using log4net;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.Sys;
using System.Collections.Generic;

namespace OHM.SYS
{

    public class OpenHomeMation
    {
        #region Private Members

        private ILog _logger;
        private bool _isRunning = false;
        private OhmSystem _ohmSystem;
        private IDictionary<string, IDictionary<string, object>>config = new Dictionary<string, IDictionary<string, object>>();
        #endregion 

        #region Public Ctor

        public OpenHomeMation(IPluginsManager pluginsMng, DataManagerAbstract dataMng, ILoggerManager loggerMng, IInterfacesManager interfacesMng, IVrManager vrMng)
        {
            _ohmSystem = new OhmSystem(interfacesMng, vrMng, loggerMng, dataMng, pluginsMng);

            _logger = _ohmSystem.LoggerMng.GetLogger("OHM");

            //Set default config options
            setDefaultConfig();
        }

        #endregion

        #region Public Properties

        public IAPI API { get { return _ohmSystem.API; } }

        public bool IsRunning { get { return this._isRunning; } }

        #endregion

        #region Public Method

        public bool Start(IList<string> args = null)
        {
            //Result variable
            bool result = false;

            //Check if we are already running
            if (!IsRunning)
            {
                //Parse Args and merge it in options
                parseArgsToConfig(args);

                bool serverRunning = false;
                //Starting internal server based on the config loaded
                if (((bool)config["server-api"]["enabled"]) == true)
                {
                    //Log Launch
                    _logger.Debug("Server-api starting.");

                    //Launch server with config
                    serverRunning = OpenHomeMationServerCreator.Launch(config["server-api"]);
                    if (serverRunning)
                    {
                        _logger.Info("Server-api started.");
                    }
                    else
                    {
                        _logger.Debug("Server-api start failed : Can't start the server instance.");
                    }
                }

                if(((bool)config["system"]["require-server-api"]) == true && serverRunning == false) {
                    _logger.Fatal("Required server api not meet on start");
                    result = false;
                }
                else
                {
                    if (((bool)config["system"]["launch-on-start"]) == true)
                    {
                        _logger.Debug("System starting.");
                        if (serverRunning == false)
                        {
                            _logger.Debug("No server-api on system start");
                        }

                        if (_ohmSystem.Start())
                        {
                            this._isRunning = true;
                            _logger.Info("System started.");
                            result = true;
                        }
                        else
                        {
                            //Log fatal error
                            _logger.Fatal("System start failed : Can't start the system.");
                            result = false;
                        }
                    }
                    else if (serverRunning == false)
                    {
                        _logger.Fatal("System started without Server api and running instance. (Ghost process)");
                        result = true;
                    }
                    else
                    {
                        //Server instance running;
                        result = true;
                    }
                }
            }
            else
            {
                _logger.Warn("Already running.");
            }

            return result;
        }

        public void Shutdown()
        {
            _logger.Debug("Shutdowning system.");
            if (IsRunning)
            {
                //Log data
                _logger.Debug("Stoping running system.");

                //TODO Shutdown AI Layer

                //TODO Shutdown AI Manager

                //TODO Shutdown VR Layer

                //TODO Shutdown VR Manager

                //TODO Shutdown Real Layer

                //TODO Shutdown Interface Manager

                //TODO Shutdown Plugins Manager
            

                //TODO Shutdown System Status

                //Shutdown DataManager
                _ohmSystem.DataMng.Shutdown();

                //TODO shutdown Logger
                //_ohmSystem.LoggerMng.Shutdown();

                //Switch inner flag
                this._isRunning = false;

                //Log data
                _logger.Info("Stoped.");
            }
            else
            {
                _logger.Info("System was not running.");
            }
        }
        #endregion

        private IDictionary<string, object> getServerConfigDefault() {
            IDictionary<string, object> result = new Dictionary<string, object>();
            result["enabled"] = false;
            return result;
        }

        private IDictionary<string, object> getSystemConfigDefault()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            result["require-server-api"] = false;
            result["launch-on-start"] = false;
            result["config-file"] = "";
            return result;
        }

        private void setDefaultConfig()
        {
            config["server-api"] = getServerConfigDefault();
            config["system"] = getSystemConfigDefault();
        }

        private void parseArgsToConfig(IList<string> args)
        {
            _logger.Debug("Args parsing");

            //TODO : Split task
            IDictionary<string, IList<string>> tasks = new Dictionary<string, IList<string>>();
            IList<string> configFileArgs = null;
            string tempTaskKey = null;
            IList<string> tempValue = null;
            bool isStarting = true;

            //Will overide default Config
            foreach (string item in args)
            {

                if (isStarting && item.StartsWith("-")) {
                    isStarting = false;

                    //Prepare first task
                    tempTaskKey = item.Substring(1);
                    tempValue = new List<string>();
                }
                else if (!isStarting && item.StartsWith("-") && tempTaskKey != null)
                {
                    //Close task
                    if (tempTaskKey == "-system.config-file") {
                        //Special case
                        _logger.Debug("External config file provided by args");

                        //Store file
                        configFileArgs = tempValue;
                    } else {
                        tasks[tempTaskKey] = tempValue;
                        
                        //Reset variable
                        tempValue = new List<string>();
                        tempTaskKey = item.Substring(1);
                    }
                }
                else if (!isStarting && tempTaskKey != null)
                {
                    //Append task option
                    tempValue.Add(item);
                }
                else
                {
                    //ERROR no command in first args
                    _logger.Error("Argument parsing error");
                }
            }

            //Close last task
            if (tempTaskKey == "-system.config-file")
            {
                //Special case
                _logger.Debug("External config file provided by args");

                //Store file
                configFileArgs = tempValue;
            }
            else if (tempTaskKey != null)
            {
                tasks[tempTaskKey] = tempValue;
            }

            //Apply config file options before direct arguments
            if (configFileArgs != null)
            {
                _logger.Debug("TODO FIND FILE AND PARSE CONFIG FILE");
            }

            _logger.Debug(tasks.Count + " arguments detected.");

            //Parse task
            foreach (var item in tasks)
            {
                string key = item.Key.ToUpper();
                if (key == "SERVER-API.ENABLED")
                {
                    _logger.Debug("Processing server-api.enabled arguments");
                    if (item.Value.Count == 1 && item.Value[0].ToUpper() == "TRUE")
                    {
                        config["server-api"]["enabled"] = true;
                        _logger.Info("server-api.enabled set to true from arguments");
                    }
                    else if (item.Value.Count == 1 && item.Value[0].ToUpper() == "FALSE")
                    {
                        config["server-api"]["enabled"] = false;
                        _logger.Info("server-api.enabled set to false from arguments");
                    }
                    else
                    {
                        _logger.Warn("Arguments value for server-api.enabled cannot be parsed. Received : " + item.Value[0]);
                    }
                }
                else if (key == "SERVER-API.PORT")
                {
                    _logger.Debug("Processing server-api.port arguments");
                    config["server-api"]["port"] = item.Value[0];
                    _logger.Info("server-api.port set to " + item.Value[0] + " from arguments");
                }
                else if (key == "SYSTEM.REQUIRE-SERVER-API")
                {
                    if (item.Value.Count == 1)
                    {
                        string val = item.Value[0].ToUpper();
                        if (val == "TRUE")
                        {
                            config["system"]["require-server-api"] = true;
                        }
                        else if (val == "FALSE")
                        {
                            config["system"]["require-server-api"] = false;
                        }
                        else
                        {
                            _logger.Warn("Arguments value for system.require-server-api cannot be parsed. Reiceved: " + val);
                        }
                    }
                    else
                    {
                        _logger.Warn("Arguments value for system.require-server-api is invalid. Received multiples values.");
                    }
                }
                else if (key == "SYSTEM.LAUNCH-ON-START")
                {
                    if (item.Value.Count == 1)
                    {
                        string val = item.Value[0];
                        if (val.ToUpper() == "TRUE")
                        {
                            config["system"]["launch-on-start"] = true;
                        }
                        else if (val.ToUpper() == "FALSE")
                        {
                            config["system"]["launch-on-start"] = false;
                        }
                        else
                        {
                            _logger.Warn("Arguments value for system.launch-on-start cannot be parsed. Reiceved: " + val);
                        }
                    }
                    else
                    {
                        _logger.Warn("Arguments value for system.require-server-api is invalid. Received multiples values.");
                    }
                }
            }

            _logger.Debug("Args parsed and splitted");

            //Strict config priority
            //External config files



        }
    }
}
