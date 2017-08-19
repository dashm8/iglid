using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using iglid.Data;
using iglid.Services;
using iglid.Models;
using iglid.Models.TeamViewModels;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace iglid.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly TeamContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;


        public TeamController(TeamContext context,
            UserManager<ApplicationUser> usermanager
            , IEmailSender email)
        {
            _context = context;
            _userManager = usermanager;
            _emailSender = email;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(MassageId? massage = null)
        {
            ViewData["StatusMassage"] =
                massage == MassageId.TeamCreatedS ? "Team Created Successfuly"
                : massage == MassageId.TeamCreatedF ? "Team Was Not Created"
                : massage == MassageId.Join ? "Request Sent"
                : massage == MassageId.JoinF ? "Cannot Send Request"
                : massage == MassageId.InviteSent ? "Invite Sent"
                : massage == MassageId.InviteSendF ? "Cannot send invite"
                : massage == MassageId.Error ? "an error has occured"
                : massage == MassageId.Team404 ? "Team does not exists"
                :"";
            var user = await GetCurrentUserAsync();
            var tempteams = _context.teams.Include(x => x.Leader).OrderBy(s => s.score);
            IndexViewModel model = new IndexViewModel() { HasTeam = user.Tname != null, teams =tempteams };
            if (user.Tname != null)
                model.TeamId = _context.teams.First(t => t.TeamName == user.Tname).ID;
            if (model.teams.Count() == 0)
                return Redirect("Team/" + nameof(Create));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user.Tname != null)
                return Redirect(nameof(Index));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var user = await GetCurrentUserAsync();
            if (user.Tname != null)
                return RedirectToAction(nameof(Index),MassageId.TeamCreatedF);
            Team team = new Team()
            {
                TeamName = model.team_name,
                CanPlay = false,
                Leader = user
            };
            user.Tname = model.team_name;
            await _userManager.UpdateAsync(user);                        
            await _context.teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),MassageId.TeamCreatedS);

        }

        [HttpGet]
        public IActionResult Profile(long id)
        {
            Models.Team team = _context.teams.First(t => t.ID == id);
            return View(team);
        }

        [HttpGet]
        public IActionResult Join(long id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Join(long id, JoinViewModel model)
        {
            var team = await _context.teams.FindAsync(id);
            if (team == null)
                return RedirectToAction(nameof(Index), MassageId.Team404);
            var user = await GetCurrentUserAsync();
            Requests request = new Requests(user, model.massage);
            team.requests.Add(request);
            _context.teams.Update(team);
            int x = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),MassageId.Join);
        }

        [HttpGet]
        public async Task<IActionResult> Invite(long id)
        {
            var user = await GetCurrentUserAsync();
            var team = _context.teams.First(t => t.ID == id);            
            if (user == team.Leader)
                return RedirectToAction(nameof(Profile), id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Invite(long id, InviteViewModel model)
        {
            Massage msg = new Massage(model.massage,
                await GetCurrentUserAsync(),
               id);
            var team = await _context.teams.FindAsync(id);
            if (team.players.Count > 3)
                return RedirectToAction(nameof(Index),MassageId.InviteSendF);
            var user = await _userManager.FindByIdAsync(model.invite.Id);
            user.massages.Add(msg);
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index),MassageId.InviteSent);
        }
        #region helpers

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public enum MassageId
        {
            TeamCreatedS,
            TeamCreatedF,
            InviteSent,
            InviteSendF,
            Join,
            JoinF,
            Team404,
            Error
        }

        #endregion
    }
}