using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class ChatController : Controller
    {
        private AppDbContext db;
        private UserRepository userRepository;
        private FriendsRepository friendsRepository;
        private ChatRepository chatRepository;
        private FriendsViewModel friendsModel;
        private ChatsViewModel chatsModel;
        private MessagesView messagesModel;
        private AddToChatModel addFriendsModel;
        public ChatController(AppDbContext context)
        {
            db = context;
            userRepository = new UserRepository(context);
            friendsRepository = new FriendsRepository(context);
            chatRepository = new ChatRepository(context);
            friendsModel = new FriendsViewModel();
            chatsModel = new ChatsViewModel();
            messagesModel = new MessagesView();
            addFriendsModel = new AddToChatModel();
        }

        public IActionResult ChatsPage()
        {
            chatsModel.UserChats = chatRepository.GetUserChats(CurrentUser.UserId);
            chatsModel.PublicChats = chatRepository.GetPublicChats(CurrentUser.UserId);
            return View(chatsModel);
        }

        public RedirectToRouteResult DeleteFromChat(Chat chat)
        {
            chatRepository.DeleteMemberFromChat(chat.Id, CurrentUser.UserId);
            return RedirectToRoute(new { controller = "Chat", action = "ChatsPage" });
        }

        public RedirectToRouteResult AddChat(Chat chat)
        {
            chatRepository.AddChatMember(chat.Id, CurrentUser.UserId);
            return RedirectToRoute(new { controller = "Chat", action = "ChatsPage" });
        }

        public IActionResult Chat(Chat chat)
        {
            messagesModel.Chat = chatRepository.GetChatById(chat.Id);
            messagesModel.List = chatRepository.GetMessagesByChatId(chat.Id);
            return View(messagesModel);
        }

        [HttpPost]
        public RedirectToRouteResult SendMessage(int Id)
        {
            if (Request.Form["messageText"] != "") { 
                Message message = new Message();
                message.ChatId = Id;
                message.UserId = CurrentUser.UserId;
                message.MessageText = Request.Form["messageText"];
                message.Date = DateTime.Now;
                chatRepository.AddMessage(message);
            }
            return RedirectToRoute(new { controller = "Chat", action = "Chat", Id});
        }
        public IActionResult AddFriends(Chat chat)
        {
            addFriendsModel.Friends = chatRepository.InviteFriendsToChat(chat.Id, CurrentUser.UserId);
            addFriendsModel.Chat = chatRepository.GetChatById(chat.Id);
            return View(addFriendsModel);
        }
        public RedirectToRouteResult AddFriendToChat(int friendId, int chatId)
        {
            if(friendId != 0 && chatId != 0)
            {
                chatRepository.AddChatMember(chatId, friendId);
            }
            return RedirectToRoute(new { controller = "Chat", action = "Chat", Id = chatId });
        }
        [HttpGet]
        public IActionResult CreateChat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateChat(Chat chat)
        {
            if(chat.Name != null)
            {
                chatRepository.AddChat(chat);
                return RedirectToAction("ChatsPage", "Chat");
            }
            return View();
        }

        public IActionResult SearchChat(string query)
        {
            chatsModel.Query = query;
            chatsModel.PublicChats = chatRepository.GetPublicChats(CurrentUser.UserId);
            return View(chatsModel);
        }
    }
}
