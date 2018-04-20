using System.Collections.Generic;
using Core.Domain.Enums;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;

namespace Core.Domain.Services {
    public class ChatService {

        private readonly IChatRepository _chatRepository;

        public ChatService (IChatRepository chatRepository) {
            _chatRepository = chatRepository;
        }

        public List<Chat> List () {
            List<Chat> chatsResponse = _chatRepository.List ();
            return chatsResponse;
        }

        public void Create(Chat chatRequest) {
            _chatRepository.Create(chatRequest);
        }
    }
}