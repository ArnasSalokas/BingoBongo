using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using Template.Services.Base;
using Template.Services.Services.Contracts;

namespace Template.Services.Services
{
    public class RngService : BaseService, IRngService
    {
        private readonly RNGCryptoServiceProvider _rng;

        public RngService(IServiceProvider services) : base(services) => _rng = services.GetRngCrypto();

        public byte[] Bytes(int size)
        {
            var bytes = new byte[size];

            _rng.GetBytes(bytes);

            return bytes;
        }
    }
}
