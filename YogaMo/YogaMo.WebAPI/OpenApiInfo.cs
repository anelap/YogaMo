﻿using Swashbuckle.AspNetCore.Swagger;

namespace YogaMo.WebAPI
{
    internal class OpenApiInfo : Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}