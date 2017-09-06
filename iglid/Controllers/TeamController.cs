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
        private readonly GameContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;


        public TeamController(GameContext context,
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
                : massage == MassageId.TeamDeletedS ? "Your team was deleted"
                :"";
            var user = await GetCurrentUserAsync();
            var tempteams = _context.teams.Include(x => x.Leader).OrderBy(s => s.score);
            IndexViewModel model = new IndexViewModel() { HasTeam = user.team != null, teams =tempteams };
            if (user.team != null)
                model.TeamId = user.team.ID;
            if (model.teams.Count() == 0)
                return Redirect("Team/" + nameof(Create));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Massages(long id)
        {
            var user = await GetCurrentUserAsync();
            var team = _context.teams.Find(id);
            if (user != team.Leader)
                return NotFound();
            return View(team.requests);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user.team != null)
                return Redirect(nameof(Index));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var user = await GetCurrentUserAsync();
            if (user.team != null)
                return RedirectToAction(nameof(Index),MassageId.TeamCreatedF);
            Team team = new Team()
            {
                TeamName = model.team_name,
                CanPlay = false,
                Leader = user,
                players = new List<ApplicationUser>()                
            };
            var temp = await _context.teams.FindAsync(model.team_name);
            if (temp == null)
                RedirectToAction(nameof(Index),MassageId.Error);
            team.players.Add(user);
            user.team = team;
            await _userManager.UpdateAsync(user);                        
            await _context.teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),MassageId.TeamCreatedS);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Profile(long id)
        {
            Team team = _context.teams.
                Include(x => x.Leader).
                Include(x => x.players).
                First(t => t.ID == id);
            var user = await GetCurrentUserAsync();
            bool isleader = user == team.Leader;
            ProfileViewModel model = new ProfileViewModel() { IsCurrentUserLeader = isleader, team = team };
            return View(model);
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
            await _context.SaveChangesAsync();
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
            var team = _context.teams.Find(id);
            Massage msg = new Massage(model.massage,
                await GetCurrentUserAsync(),
               team);
            if (team == null)
                return NotFound();
            if (team.players.Count > 3)
                return RedirectToAction(nameof(Index),MassageId.InviteSendF);
            var user = await _userManager.FindByIdAsync(model.invite.Id);
            user.massages.Add(msg);
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index),MassageId.InviteSent);
        }

        [HttpPost]           
        public async Task<IActionResult> Remove(long teamid, string playerid)
        {
            var user = await GetCurrentUserAsync();
            var team = await _context.teams.FindAsync(teamid);
            if (team == null)
                return RedirectToAction(nameof(Index), MassageId.Team404);
            if (team.Leader != user)
                return RedirectToAction(nameof(Index), MassageId.Error);
            var ToRemove = await _userManager.FindByIdAsync(playerid);
            if (ToRemove == null)
                return RedirectToAction(nameof(Index), MassageId.Error);
            team.players.Remove(ToRemove);
            _context.teams.Update(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Profile),team.ID);
        }

        [HttpPost]
        public async Task<IActionResult> Exit(long id)
        {
            var user = await GetCurrentUserAsync();
            var team = await _context.teams.FindAsync(id);
            if (!team.players.Exists(x => x == user))
                return RedirectToAction(nameof(Index), MassageId.Error);
            if (team.Leader == user)
                return RedirectToAction(nameof(Index), MassageId.ExitF);
            team.players.Remove(user);
            _context.teams.Update(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), MassageId.Exit);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var team = await _context.teams.FindAsync(id);
            if (team == null)
                return NotFound();
            var user = await GetCurrentUserAsync();
            if (team.Leader != user)
                return NotFound();
            _context.teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),MassageId.TeamDeletedS);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(long id)
        {
            var user = await GetCurrentUserAsync();
            var team = user.team;         
            if (user.team == null || team.players.Count == 4)
                return NotFound();
            var massage = team.requests.Find(x => x.ID == id);
            team.players.Add(massage.sender);
            team.requests.Remove(massage);
            massage.sender.team = team;
            if (team.players.Count == 4) { 
                team.CanPlay = true;
                team.score = team.players.Sum(x => x.score) / 4;
            }
            _context.teams.Update(team);
            await _userManager.UpdateAsync(massage.sender);
            return RedirectToAction(nameof(Massages), team.ID);

        }

        [HttpPost]
        public async Task<IActionResult> Decline(long id)
        {
            var user = await GetCurrentUserAsync();
            var team = user.team;
            var req = team.requests.Find(x => x.ID == id);
            team.requests.Remove(req);
            return RedirectToAction(nameof(Massages), team.ID);
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
            TeamDeletedS,
            InviteSent,
            InviteSendF,
            Join,
            JoinF,
            Exit,
            ExitF,
            Team404,
            Error
        }

        #endregion
    }
}