using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Services;
using Core.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers {

    [Route ("api/chat")]
    public class ChatController : Controller {
        private readonly ChatService _chatService;

        public ChatController (ChatService chatService) {
            _chatService = chatService;
        }

        [HttpGet]
        public List<Chat> List () {
            List<Chat> chatsResponse = _chatService.List ();
            return chatsResponse;
        }

        [HttpPost]
        public void Create (Chat chat) {
            _chatService.Create (chat);
        }
    }
}