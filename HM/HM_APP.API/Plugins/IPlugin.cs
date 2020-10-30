using System;
using System.Collections.Generic;
using System.Text;

namespace HM_App.API.Plugins
{
    public interface IPlugin
    {
        /// <summary>
        /// Nome do Plugin
        /// </summary>
        string Nome { get; }
        /// <summary>
        /// Descrição do Plugin
        /// </summary>
        string Descrição { get; }

        /// <summary>
        /// Versão actual do Plugin
        /// </summary>
        SemVersion Version { get; }

        /// <summary>
        /// Tipo de implementação do plugin
        /// </summary>
        PluginType PluginForm { get; }

        /// <summary>
        /// If you want pass some args
        /// </summary>
        [Obsolete]
        string args { get; }

        /// <summary>
        /// Represent the object class to iniciate
        /// </summary>
        Type ObjectType { get; }
    }
}
