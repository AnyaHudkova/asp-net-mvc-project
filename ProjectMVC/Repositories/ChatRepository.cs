using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Repositories
{
    public class ChatRepository
    {
        public AppDbContext context { get; set; }
        private DbSet<Chat> chatTable = null;
        private DbSet<ChatMember> chatMemberTable = null;
        private DbSet<Message> messageTable = null;

        private UserRepository userRepository;
        private FriendsRepository friendsRepository;

        public ChatRepository(AppDbContext context)
        {
            this.context = context;
            chatTable = context.Set<Chat>();
            chatMemberTable = context.Set<ChatMember>();
            messageTable = context.Set<Message>();
            userRepository = new UserRepository(context);
            friendsRepository = new FriendsRepository(context);
        }
        public void AddChat(Chat chatModel)
        {
            chatTable.Add(chatModel);
            context.SaveChanges();
            AddChatMember(chatModel.Id, CurrentUser.UserId);
        }

        public void AddChatMember(int chatId, int userId)
        {
            ChatMember chatMember = new ChatMember();
            chatMember.UserId = userId;
            chatMember.ChatId = chatId;
            chatMemberTable.Add(chatMember);
            context.SaveChanges();
        }

        public Chat GetChatById(int chatId)
        {
            foreach (Chat chat in chatTable)
            {
                if (chat.Id == chatId) return chat;
            }
            return null;
        }
        public List<Chat> GetAllChats()
        {
            List<Chat> chats = new List<Chat>();
            foreach (Chat chat in chatTable)
            {
                chats.Add(chat);
            }
            return chats;
        }
        public List<Chat> GetUserChats(int userId)
        {
            List<Chat> allChats = GetAllChats();
            List<Chat> userChats = new List<Chat>();
            foreach (Chat chat in allChats)
            {
                if (IsInChat(userId, chat.Id))
                {
                    userChats.Add(chat);
                }
            }
            return userChats;
        }

        public List<User> InviteFriendsToChat(int chatId, int userId)
        {
            List<User> friends = friendsRepository.GetUserFriends(userId);
            List<User> inviteFriends = new List<User>();
            foreach (User user in friends)
            {
                if (!IsInChat(user.Id, chatId)) inviteFriends.Add(user);
            }
            return inviteFriends;
        }

        public List<ChatMember> GetAllChatMembers()
        {
            List<ChatMember> members = new List<ChatMember>();
            foreach (ChatMember member in chatMemberTable)
            {
                members.Add(member);
            }
            return members;
        }

        public bool IsInChat(int userId, int chatId)
        {
            List<ChatMember> members = GetAllChatMembers();
            foreach (ChatMember chatMember in members)
            {
                if (chatMember.UserId == userId && chatMember.ChatId == chatId)
                    return true;
            }
            return false;
        }

        public List<Chat> GetPublicChats (int userId)
        {
            List<Chat> allChats = GetAllChats();
            List<Chat> publicChats = new List<Chat>();
            foreach (Chat chat in allChats)
            {
                if (!IsInChat(userId, chat.Id) && !chat.PrivateChat)
                {
                    publicChats.Add(chat);
                }
            }
            return publicChats;
        }

        public void DeleteMemberFromChat(int chatId, int userId)
        {
            int idDeleted = FindChatMember(chatId, userId).Id;
            chatMemberTable.Remove(chatMemberTable.Find(idDeleted));
            context.SaveChanges();
        }

        public ChatMember FindChatMember(int chatId, int userId)
        {
            foreach (ChatMember member in chatMemberTable)
            {
                if (member.UserId == userId && member.ChatId == chatId)
                    return member;
            }
            return null;
        }

        public List<Message> GetAllMessages()
        {
            List<Message> messages = new List<Message>();
            foreach(Message message in messageTable)
            {
                messages.Add(message);
            }
            return messages;
        }

        public void AddMessage(Message message)
        {
            messageTable.Add(message);
            context.SaveChanges();
        }

        public List<MessageViewModel> GetMessagesByChatId(int chat_id)
        {
            List<MessageViewModel> list = new List<MessageViewModel>();
            List<Message> messages = GetAllMessages();
            foreach (Message message in messages)
            {
                if(message.ChatId == chat_id)
                {
                    User user = userRepository.GetUserById(message.UserId);
                    MessageViewModel model = new MessageViewModel();
                    model.User = user;
                    model.Message = message;
                    list.Add(model);
                }             
            }
            return list;
        }
    }
}
