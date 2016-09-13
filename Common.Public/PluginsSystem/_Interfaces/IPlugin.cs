﻿using OHM.Nodes.ALR;
using OHM.Nodes.ALV;
using OHM.SYS;
using System;

namespace OHM.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        PluginStates State { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        bool Install(IOhmSystemInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        bool Uninstall(IOhmSystemUnInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        bool Update(IOhmSystemInstallGateway system);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ALRInterfaceAbstractNode CreateInterface(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IVrType CreateVrNode(string key);
    }
}
