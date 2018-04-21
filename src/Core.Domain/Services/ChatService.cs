using System.Collections.Generic;
using System.Linq;
using Core.Domain.Enums;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Services {
    public class ChatService {

        private readonly IChatRepository _chatRepository;

        private readonly IMemoryCache _memoryCache;

        public ChatService (IChatRepository chatRepository, IMemoryCache memoryCache) {
            _chatRepository = chatRepository;
            _memoryCache = memoryCache;
        }

        public List<Chat> List (bool ignoreCache) {

            //--- obtem do cache
            var chatsCache = _memoryCache.Get<List<Chat>> ("chat");

            //--- se encontrou no cache 
            if (chatsCache != null && chatsCache.Any () && !ignoreCache)
                return chatsCache;

            //--- busca no banco caso não tem no cache
            List<Chat> chatsResponse = _chatRepository.List ();

            //--- guarda no cache
            _memoryCache.Set<List<Chat>> ("chat", chatsResponse);

            //--- retorna
            return chatsResponse;
        }

        public void Create (ChatCreateRequest chatCreateRequest) {
            _chatRepository.Create (chatCreateRequest);
            List (true);
        }
    }
}