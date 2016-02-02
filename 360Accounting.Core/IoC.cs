using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public static class IoC
    {
        private static readonly IApplicationContext AppContext =
                new XmlApplicationContext(false,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["SpringFilePath"] ?? "Spring.cfg.xml"));

        public static T Resolve<T>(string name)
        {
            return
                (T)AppContext.GetObject(name);
        }

        public static bool Exists(string name)
        {
            return
                AppContext.ContainsObjectDefinition(name);
        }
    }
}
