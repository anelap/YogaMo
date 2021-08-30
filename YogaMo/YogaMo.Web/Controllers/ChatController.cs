using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    [Authorization(isClient: true, isInstructor: true)]

    public class ChatController : Controller
    {
        private readonly _150222Context context;

        public ChatController(_150222Context context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var user = HttpContext.GetLoggedInUser();

            ChatVM model = new ChatVM()
            {
                Clients = context.Client.ToList(),
                Instructors = context.Instructor.ToList(),
                Role = user.RoleName,
                Sender = user
            };

            LoadChannelList(model);

            LoadDMs(user.Id, model);

            return View(model);
        }

        private void LoadChannelList(ChatVM model)
        {
            if (model.Role == "Client")
            {
                model.ChannelsList = context.Instructor.Select(x => "dm-client-" + model.Sender.Id + "-instructor-" + x.InstructorId).ToList();
            }
            else if (model.Role == "Instructor")
            {
                model.ChannelsList = context.Client.Select(x => "dm-client-" + x.ClientId + "-instructor-" + model.Sender.Id).ToList();
            }
        }

        private void LoadDMs(int userId, ChatVM model)
        {
            var DM_query = context.ChatInstructorsClients.AsQueryable();

            if (model.Role == "Instructor")
            {
                DM_query = DM_query.Where(x => x.InstructorId == userId);
            }
            else if (model.Role == "Client")
            {
                DM_query = DM_query.Where(x => x.ClientId == userId);
            }

            model.DirectMessages = DM_query
                .Select(x => new DirectMessagesVM
                {
                    Id = x.Id,
                    LastMessage = x.LastMessage,
                    Client = new DMParticipant
                    {
                        Id = x.Client.ClientId,
                        FirstName = x.Client.FirstName,
                        LastName = x.Client.LastName,
                        Role = "Client"
                    },
                    Instructor = new DMParticipant
                    {
                        Id = x.Instructor.InstructorId,
                        FirstName = x.Instructor.FirstName,
                        LastName = x.Instructor.LastName,
                        Role = "Instructor"
                    },
                    LastSeenClient = x.LastSeenClient,
                    LastSeenInstructor = x.LastSeenInstructor
                })
                .OrderByDescending(x => x.LastMessage)
                .ToList();
        }

        [HttpPost]
        public IActionResult UpdateLastMessage(int Id)
        {
            var user = HttpContext.GetLoggedInUser();
            List<ChatInstructorsClients> records = null;
            ChatInstructorsClients record = null;

            if (user.RoleName == "Client")
            {
                records = context.ChatInstructorsClients.Where(x => x.ClientId == user.Id && x.InstructorId == Id).ToList();
                if (records.Count == 0)
                {
                    record = new ChatInstructorsClients
                    {
                        ClientId = user.Id,
                        InstructorId = Id
                    };
                    context.Add(record);
                }
                else
                    record = records[0];
            }
            else if (user.RoleName == "Instructor")
            {
                records = context.ChatInstructorsClients.Where(x => x.InstructorId == user.Id && x.ClientId == Id).ToList();
                if (records.Count == 0)
                {
                    record = new ChatInstructorsClients
                    {
                        InstructorId = user.Id,
                        ClientId = Id
                    };
                    context.Add(record);
                }
                else
                    record = records[0];

            }
            record.LastMessage = DateTime.Now;

            if (records.Count > 1)
            {
                records.RemoveAt(0);
                context.RemoveRange(records);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            var result = context.ChatInstructorsClients.Where(x => x.Id == record.Id).Include(x => x.Instructor).Include(x => x.Client).FirstOrDefault();

            return Json(new DirectMessagesVM
            {
                Id = result.Id,
                LastMessage = result.LastMessage,
                Client = new DMParticipant
                {
                    Id = result.Client.ClientId,
                    FirstName = result.Client.FirstName,
                    LastName = result.Client.LastName,
                    Role = "Client"
                },
                Instructor = new DMParticipant
                {
                    Id = result.Instructor.InstructorId,
                    FirstName = result.Instructor.FirstName,
                    LastName = result.Instructor.LastName,
                    Role = "Instructor"
                },
            });

        }


        [HttpPost]
        public IActionResult UpdateLastSeen(int Id, string Timetoken)
        {
            var user = HttpContext.GetLoggedInUser();
            ChatInstructorsClients record = null;

            if (user.RoleName == "Client")
            {
                record = context.ChatInstructorsClients.Where(x => x.ClientId == user.Id && x.InstructorId == Id).FirstOrDefault();

                if (record != null)
                    record.LastSeenClient = Timetoken;
            }
            else if (user.RoleName == "Instructor")
            {
                record = context.ChatInstructorsClients.Where(x => x.InstructorId == user.Id && x.ClientId == Id).FirstOrDefault();

                if (record != null)
                    record.LastSeenInstructor = Timetoken;
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return Json(true);
        }
    }
}
