﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DeviceDetectorNET;
using FourmBuilder.Common.Helper;
using Microsoft.AspNetCore.Http;

namespace FourmBuilder.Common.Middleware
{
    public class ApplicationMetaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestMeta _requestMeta;

        public ApplicationMetaMiddleware(RequestDelegate next, IRequestMeta requestMeta)
        {
            _next = next;
            _requestMeta = requestMeta;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault();
            try
            {
                var userAgentInfo = DeviceDetector.GetInfoFromUserAgent(userAgent);

                if (httpContext.Connection.RemoteIpAddress != null)
                    _requestMeta.Ip = httpContext.Connection.RemoteIpAddress.ToString();

                if (userAgentInfo.Match.BrowserFamily != "Unknown")
                {
                    _requestMeta.Browser = userAgentInfo.Match.Client.Name;
                    _requestMeta.Os = userAgentInfo.Match.Os.Name;
                    _requestMeta.Device = userAgentInfo.Match.DeviceType;
                    _requestMeta.UserAgent = userAgent ??= "Unknown";
                }
                else
                {
                    _requestMeta.Browser = "";
                    _requestMeta.Os = "";
                    _requestMeta.Device = "";
                    _requestMeta.UserAgent = userAgent ??= "Unknown";
                }
            }
            catch (Exception)
            {
                _requestMeta.Browser = "";
                _requestMeta.Os = "";
                _requestMeta.Device = "";
                _requestMeta.UserAgent = userAgent ??= "Unknown";
            }

            return _next(httpContext);
        }
    }

}