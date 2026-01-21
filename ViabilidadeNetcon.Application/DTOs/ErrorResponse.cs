using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViabilidadeNetcon.Application.DTOs
{ 
    public class ErrorResponseDto
    {
        public string Code { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }

        public ErrorResponseDto(string code, string reason, string message, string status)
        {
            Code = code;
            Reason = reason;
            Message = message;
            Status = status;
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
