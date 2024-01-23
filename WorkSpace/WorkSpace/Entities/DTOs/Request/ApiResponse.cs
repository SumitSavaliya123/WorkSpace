using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Request
{
    public class ApiResponse
    {
        public string Message { get; set; } = string.Empty;

        public bool Success { get; set; }

        public object? Errors { get; set; }

        public object? Data { get; set; }

        public int StatusCode { get; set; }
    }
}
