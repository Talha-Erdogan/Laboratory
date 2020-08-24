﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Common
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ApiUrl
        {
            get
            {
                //return System.Configuration.ConfigurationManager.AppSettings["PortalApiUrl"];
                // todo: appsettings.json dosyasından okunması sağlanacak
                //return _configuration.GetValue<string>("AppSettings:ApiUrl");
                return _configuration.GetSection(ApiUrl).Value;
            }
        }

        public static string ApiBaseUrl
        {
            get
            {
                //return System.Configuration.ConfigurationManager.AppSettings["PortalApiUrl"];
                // todo: appsettings.json dosyasından okunması sağlanacak
                //return _configuration.GetValue<string>("AppSettings:ApiBaseUrl");
                return _configuration.GetSection(ApiBaseUrl).Value;
            }
        }

    }
}
