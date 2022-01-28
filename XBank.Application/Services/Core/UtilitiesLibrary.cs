using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Application.Services.Core
{
    public static class UtilitiesLibrary
    {
        public static string GetSectionFromSettings(IConfiguration configuration, string section, string subSection = null)
        {
            string responseSection = string.Empty;
            var response = configuration.GetSection(section);
            if (!string.IsNullOrEmpty(subSection))
                responseSection = response.GetSection(subSection).Value;
            else
                responseSection = response.Value;
            
            return responseSection;
        }
    }
}
