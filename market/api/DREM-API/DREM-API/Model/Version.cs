using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model
{
    /// <summary>
    /// Version model
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Instance identifier. Every application runtime has its own guid. If 3 pods are launched in kubernetes, it is possible to identify instance by this identifier
        /// </summary>
        public string InstanceIdentifier { get; set; }
        /// <summary>
        /// Last time when instance has been reset
        /// </summary>
        public string InstanceStartedAt { get; set; }
        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; set; } = "DREM-API";
        /// <summary>
        /// Docker image version
        /// </summary>
        public string DockerImageVersion { get; set; }
        /// <summary>
        /// Build number from devops or github actions
        /// </summary>
        public string BuildNumber { get; set; }
        /// <summary>
        /// Application dll version
        /// </summary>
        public string DLLVersion { get; set; }
        /// <summary>
        /// Dll build time
        /// </summary>
        public string BuildTime { get; set; }
        /// <summary>
        /// Culture info
        /// </summary>
        public string Culture { get; set; } = CultureInfo.CurrentCulture.Name;
        /// <summary>
        /// Shows info weather email service is configured
        /// </summary>
        public bool EmailConfigured { get; set; } = false;
        /// <summary>
        /// Shows info weather persistant redis service is configured
        /// </summary>
        public bool RedisConfigured { get; set; } = false;
        /// <summary>
        /// Shows info weather SMS service is configured
        /// </summary>
        public bool SMSConfigured { get; set; } = false;
        /// <summary>
        /// Storage test
        /// </summary>
        public string StorageTest { get; set; }

        /// <summary>
        /// Get version
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="start"></param>
        /// <param name="dllVersion"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Version GetVersion(string instanceId, DateTimeOffset start, string dllVersion, IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(instanceId))
            {
                throw new ArgumentException($"'{nameof(instanceId)}' cannot be null or empty", nameof(instanceId));
            }

            if (string.IsNullOrEmpty(dllVersion))
            {
                throw new ArgumentException($"'{nameof(dllVersion)}' cannot be null or empty", nameof(dllVersion));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var ret = new Version();
            var versionFile = "version.txt";
            if (System.IO.File.Exists(versionFile))
            {
                var version = System.IO.File.ReadAllText(versionFile).Trim();
                var versionData = version.Split('|');
                if (versionData.Length == 3)
                {
                    var pos = versionData[0].LastIndexOf('-');
                    if (pos > 0)
                    {
                        ret.ApplicationName = versionData[0].Substring(0, pos - 1).Trim();
                        ret.BuildNumber = versionData[0][(pos + 1)..].Trim();
                    }
                    ret.DLLVersion = versionData[1].Trim();
                    ret.BuildTime = versionData[2].Trim();
                }
            }
            else
            {
                ret.DLLVersion = dllVersion;
            }
            var versionFileDocker = "docker-version.txt";
            if (System.IO.File.Exists(versionFileDocker))
            {
                ret.DockerImageVersion = System.IO.File.ReadAllText(versionFileDocker).Trim();
            }
            ret.InstanceStartedAt = start.ToString("o");
            ret.InstanceIdentifier = instanceId;


            return ret;
        }
    }
}
