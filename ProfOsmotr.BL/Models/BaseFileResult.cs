using System;

namespace ProfOsmotr.BL.Models
{
    public class BaseFileResult
    {
        public BaseFileResult(byte[] bytes, string contentType, string fileName)
        {
            Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        public byte[] Bytes { get; }

        public string ContentType { get; }

        public string FileName { get; }
    }
}