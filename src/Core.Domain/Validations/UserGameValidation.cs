using System;
using Core.Domain.Helpers;
using Core.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Validations {
    public class UserGameValidation {
        private readonly IMemoryCache _memoryCache;

        public UserGameValidation (IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public void CanSave () {
            _memoryCache.Set("teste", "meu teste");
            var token = _memoryCache.Get ("teste");
        }
    }
}