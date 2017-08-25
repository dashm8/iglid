using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iglid.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iglid.Models;
using iglid.Models.GameViewModels;

namespace iglid.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly MatchContext _MatchContext;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly TeamContext _TeamContext;

        public GameController(MatchContext mc,
            UserManager<ApplicationUser> um,
            TeamContext tc)
        {
            _MatchContext = mc;
            _UserManager = um;
            _TeamContext = tc;
        }
        [HttpGet]
        [AllowAnonymous]        
        public IActionResult Index(string id)
        {
            //needs an index view model for each status and team
            var match = _MatchContext.matches.First(x => x.Id == id);
            if (match == null)
                return RedirectToAction(nameof(NotFound));
            return View(match);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Play()
        {
            List<Match> ret = new List<Match>();
            foreach (var match in _MatchContext.matches)
            {
                if (match.outcome == outcome.wait)
                    ret.Add(match);
            }
            ret.OrderBy(x => x.date);
            return View(ret);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(MassageId ? massage)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var user = await GetCurrentUserAsync();
            if (user.Tname == null)
                return RedirectToAction(nameof(Error),MassageId.NoTeam);
            var team = _TeamContext.teams.First(x => x.TeamName == user.Tname);
            if (model.Date > DateTime.Now)
                return Error(MassageId.Error);
            if (team.CanPlay)
                return NotFound();
            Match match = new Match(team, model.Date, model.bestof, model.mode);
            match.Id = Utils.randommathid();
            UpdateMatchOnDb(match,team);            
            await _MatchContext.matches.AddAsync(match);
            return RedirectToAction("Profile", "Team", team.ID);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id)
        {
            var match = _MatchContext.matches.Find(id);
            var user = await GetCurrentUserAsync();
            if (user.Tname == null || match == null)
                return NotFound();
            if (match.outcome != outcome.pending)
                return NotFound();
            var team = _TeamContext.teams.Find(user.Tname);
            match.t2 = team;
            match.outcome = outcome.progress;
            UpdateMatchOnDb(match, match.t1, match.t2);
            _MatchContext.matches.Update(match);
            await _MatchContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index), id);
        }

        [HttpGet]
        public async Task<IActionResult> Report(string id)
        {
            var user = await GetCurrentUserAsync();
            var match = _MatchContext.matches.Find(id);
            if (match == null ||
                (match.t1.Leader != user || match.t2.Leader != user))
                return NotFound();            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Report(ReportViewModel model, string id)
        {
            var user = await GetCurrentUserAsync();
            var match = _MatchContext.matches.Find(id);
            if (match == null ||
                (match.t1.Leader != user || match.t2.Leader != user))
                return NotFound();
            if (model.t1score + model.t2score != (int)match.bestof)
                return NotFound();//wtf
            if (match.outcome == outcome.wait)
            {
                if (model.t1score > model.t2score)
                    match.outcome = outcome.win;
                else
                    match.outcome = outcome.lose;
                if (match.t1score != model.t1score &&
                        match.t2score != model.t2score)
                    match.outcome = outcome.dispute;
                UpdateScore(match, match.t1, match.t2,
                    Utils.excepectwin(match.t1.score, match.t2.score));
                UpdateMatchOnDb(match, match.t1, match.t2);
                return RedirectToAction(nameof(Index), id);
            }
            else if (match.outcome == outcome.progress)
            {
                match.t1score = model.t1score;
                match.t2score = model.t2score;
                UpdateMatchOnDb(match, match.t1, match.t2);
                _MatchContext.matches.Update(match);                
                await _MatchContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index), id);
            }
            else
                return NotFound();


        }

        #region helpers
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _UserManager.GetUserAsync(HttpContext.User);
        }

        private bool CheckAvilable(Team t1)
        {
            foreach (var user in t1.players)
            {
                foreach (var match in user.matches)
                {
                    if (match.outcome == outcome.progress)
                        return false;
                }
            }
            return true;
        }

        private async void UpdateMatchOnDb(Match match,Team team)
        {
            foreach (var player in team.players)
            {
                player.matches.Add(match);
                await _UserManager.UpdateAsync(player);
            }
            _TeamContext.Update(team);
            await _TeamContext.SaveChangesAsync();
        }        

        private async void UpdateMatchOnDb(Match match,Team team,Team team1)
        {
            foreach (var player in team.players)
            {
                player.matches.Add(match);
                await _UserManager.UpdateAsync(player);
            }
            foreach (var player in team1.players)
            {
                player.matches.Add(match);
                await _UserManager.UpdateAsync(player);
            }
            _TeamContext.Update(team);
            _TeamContext.Update(team1);
            await _TeamContext.SaveChangesAsync();
        }

        private async void UpdateScoreDb(int score, Team winner, Team losser)
        {
            foreach (var player in winner.players)
            {
                player.score += score;
                await _UserManager.UpdateAsync(player);
            }
            foreach (var player in losser.players)
            {
                player.score -= score;
                await _UserManager.UpdateAsync(player);
            }
            winner.score += score;
            losser.score -= score;
            _TeamContext.teams.Update(winner);
            _TeamContext.teams.Update(losser);
            await _TeamContext.SaveChangesAsync();
        }

        private void UpdateScore(Match match,Team t1,Team t2,double except)
        {
            double k = 0;
            switch ((int)match.bestof)
            {
                case 1:
                    k = 3;
                    break;
                case 3:
                    k = 3.4;
                    break;
                case 5:
                    k = 3.8;
                    break;
            }
            if (match.outcome == outcome.win)
            {
                int ret = (int)Math.Round(k * (1 - except));
                UpdateScoreDb(ret, t1, t2);
            }
            else
            {
                int ret =  (int)Math.Round(k * (1 - (1 - except)));
                UpdateScoreDb(ret, t2, t1);
            }

        }

        public enum MassageId
        {
            Error,
            NoTeam
            //todo
        }
        #endregion
    }
}