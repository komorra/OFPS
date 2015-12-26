using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OFPSEngine.Configuration
{
    /// <summary>
    /// Class for reading and writing INI configuration files.
    /// These files are used to tell the game a configuration.
    /// </summary>
    public class ConfigData : Dictionary<string, object>
    {
        /// <summary>
        /// Gets the value of generic type from configuration.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="key">Configuration key name</param>        
        public T GetValue<T>(string key)
        {
            return (T) Convert.ChangeType(this[key], typeof (T));
        }

        /// <summary>
        /// Sets the value on configuration. If key doesn't exist
        /// the key will be created.
        /// </summary>
        /// <param name="key">Name of the key</param>
        /// <param name="value">Key value</param>
        public void SetValue(string key, object value)
        {
            if (ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                Add(key, value);
            }
        }

        /// <summary>
        /// Serializes any .NET object with exposed public
        /// properties to configuration data.
        /// </summary>
        /// <param name="data">Object that hold public properties</param>        
        public static ConfigData Serialize(object data)
        {
            var result = new ConfigData();

            var properties = data.GetType().GetProperties();
            foreach (var prop in properties)
            {
                result.SetValue(prop.Name, prop.GetValue(data, null));
            }

            return result;
        }

        /// <summary>
        /// Deserializes any configuration data into
        /// object of given type.
        /// </summary>
        /// <typeparam name="T">Type of expected object</typeparam>
        /// <param name="config">Configuration data</param>      
        public static T Deserialize<T>(ConfigData config) where T : new()
        {
            T data = new T();

            var properties = data.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = Convert.ChangeType(config[prop.Name], prop.PropertyType);
                prop.SetValue(data, value, null);
            }

            return data;
        }

        /// <summary>
        /// Creates config data from given stream.
        /// </summary>
        /// <param name="stream">Stream which will be read</param>        
        public static ConfigData FromStream(Stream stream)
        {
            var sr = new StreamReader(stream);
            var result = new ConfigData();

            string line = null;

            while ((line = sr.ReadLine())!=null)
            {
                var parts = line.Split('=').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();

                var key = parts[0];
                var value = parts[1];

                result.Add(key, value);
            }

            return result;
        }

        /// <summary>
        /// Saves current configuration data state to stream.
        /// </summary>
        /// <param name="stream">Stream which will be written</param>
        public void ToStream(Stream stream)
        {
            var sw = new StreamWriter(stream);
            foreach (var kv in this)
            {
                sw.WriteLine("{0} = {1}", kv.Key, kv.Value);
            }
        }
    }
}
