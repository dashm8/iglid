using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iglid.Models.TournamentsModels;
using iglid.Models.TournamentViewModels;
using iglid.Models;
using iglid.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace iglid.Controllers
{
    [Authorize]
    public class tournamentsController : Controller
    {
        private readonly TournamentContext _TournamentContext;
        private readonly ApplicationDbContext _UserContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GameContext _gameContext;

        public tournamentsController(TournamentContext tc,
            ApplicationDbContext adc,
            GameContext gm,
            UserManager<ApplicationUser> um)
        {
            _TournamentContext = tc;
            _UserContext = adc;
            _gameContext = gm;
            _userManager = um;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var tours = _TournamentContext.tours.Where(x => x.date > DateTime.Now);
            return View(tours);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();            
            if (!user.IsAdmin)
                return NotFound();
            return View(new tournament());
        }

        [HttpPost]
        public async Task<IActionResult> Create(tournament tour)
        {
            var user = await GetCurrentUserAsync();
            if (!user.IsAdmin)
                return NotFound();
            if (tour.date < DateTime.Now)
                return NotFound();//change
            bool x = true;
            string id = Utils.randommathid();
            while (x)
            {                
                if (_TournamentContext.tours.Find(id) == null)
                    x = false;
                id = Utils.randommathid();
            }
            tour.id = id;
            await _TournamentContext.tours.AddAsync(tour);
            return RedirectToAction(nameof(Index));                
        }

        [HttpGet]
        public async Task<IActionResult> Tournament(string id)
        {
            var matches = _TournamentContext.Tmatchs.Where(x => x.tourid == id);
            var teams = _TournamentContext.Tteams.Where(x => x.tourid == id);
            var user = await GetCurrentUserAsync();
            var tour = _TournamentContext.tours.Find(id);
            TournamentViewModel model = new TournamentViewModel()
            {
                matches = matches,
                Teams = teams,
                DidStart = DateTime.Now > tour.date

            };
            if (matches.Count() > 0 && tour.date < DateTime.Now)
                gernerate_matches(id);
            if (TUtils.intourney(user, teams, id))
                model.myteam = TUtils.GetTeam(user, teams, id);
            foreach (var team in teams)
            {
                foreach (var n in team.place.Split(','))
                {
                    team.placing.Add(int.Parse(n));
                }
            }
            return View(model);//if the tourney did not start display team list
            //else display bracket
        }

        private async void gernerate_matches(string id)
        {
            if (_TournamentContext.Tmatchs.Where(x => x.tourid == id).Count() > 0)
                id = id + "donothing";            
            else
            {
                var tour = _TournamentContext.tours.Find(id);
                var teams = _TournamentContext.Tteams.Where(x => x.tourid == id).ToList();
                List<Models.TournamentsModels.Match> matches 
                    = new List<Models.TournamentsModels.Match>();
                for (int i = 0; i < teams.Count();i++)
                {
                    Models.TournamentsModels.Match match 
                        = new Models.TournamentsModels.Match
                    {
                        tourid = id,
                        matchid = Utils.randommathid(),
                        t1id = teams[i].id,                        
                    };
                    i++;
                    match.t2id = teams[i].id;
                    matches.Append(match);
                }
                await _TournamentContext.Tmatchs.AddRangeAsync(matches);
                await _TournamentContext.SaveChangesAsync();
            }
            
        }

        [HttpGet]
        public IActionResult Join(string id)
        {
            return View(new Models.TournamentsModels.Team());
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id, Models.TournamentsModels.Team team)
        {
            tournament tr = _TournamentContext.tours.Find(id);
            var teams = _TournamentContext.Tteams.Where(x => x.tourid == id);
            if (tr.maxteams <= tr.teamcount + 1)
                return NotFound();//full
            if (tr.date > DateTime.Now)
                return NotFound();
            var user = await GetCurrentUserAsync();
            if (TUtils.intourney(user, teams, id))
                return NotFound();//already in tourney
            team.leader = user;
            team.users.Append(user);
            while (true)
            { 
                team.id = Utils.randommathid();
                if (_TournamentContext.Tteams.Find(team.id) == null)
                    break;
            }
            team.tourid = id;
            tr.teamcount += 1;
            _TournamentContext.tours.Update(tr);
            _TournamentContext.Tteams.Append(team);
            await _TournamentContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index), id);//redirect to team list
        }

        [HttpGet]
        public async Task<IActionResult> Team(string id)//teamid
        {
            var user = await GetCurrentUserAsync();
            var team = _TournamentContext.Tteams.Find(id);
            if (team == null)
                return NotFound();
            bool isleader = team.leader == user;
            return View(isleader);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(string id,string username)
        {
            username = username.Replace('\'', ' ');
            var user = await GetCurrentUserAsync();
            var team = _TournamentContext.Tteams.Find(id);
            var add = await _userManager.FindByNameAsync(username);
            if (team == null)
                return NotFound();
            if (team.leader != user)
                return NotFound();
            var tour = _TournamentContext.tours.Find(team.tourid);
            if ((int)tour.type + 1 <= team.users.Count())
                return NotFound();
            team.invited.Append(add);
            tourInv inv = new tourInv
            {
                sender = user,
                team = team,
                tourid = tour
            };
            user.tourinvites.Add(inv);
            return RedirectToAction(nameof(Team), id);
        }//teamid

        [HttpPost]
        public async Task<IActionResult> Report(string id,string action)
        {
            var user = await GetCurrentUserAsync();
            var match = _TournamentContext.Tmatchs.Find(id);
            var tour = TUtils.GetTournament(match.tourid, _TournamentContext.tours);
            if (!TUtils.intourney(user, _TournamentContext.Tteams, match.tourid))
                return NotFound();
            var team = TUtils.GetTeam(user, _TournamentContext.Tteams, match.tourid);
            if (team.id == match.t1id)
            {
                if (action == "iwon")
                    match.t1rep = TourEnums.outcome.iwon;
                else
                    match.t1rep = TourEnums.outcome.hewon;
            }
            else if (team.id == match.t2id)
            {
                if (action == "iwon")
                    match.t2rep = TourEnums.outcome.iwon;
                else
                    match.t2rep = TourEnums.outcome.hewon;
            }
            else
                return NotFound();
            if (match.status == TourEnums.status.wait)
            {
                if (match.t1rep == match.t2rep)
                { 
                    match.status = TourEnums.status.end;
                    int n = 0;
                    int next = 0;
                    int place = (int)team.place.Last();
                    if (place % 2 == 1)
                        place++;
                    n = place / 2;
                    next = tour.maxteams + n;
                    team.place.Append((char)next);
                }
                else
                    match.status = TourEnums.status.dispute;                
            }
            else
            {
                match.status = TourEnums.status.wait;
            }
            //update all values in db
            _TournamentContext.Tteams.Update(team);
            _TournamentContext.Tmatchs.Update(match);
            await _TournamentContext.SaveChangesAsync();
            //return redirect to bracket;
            return RedirectToAction(nameof(Tournament), match.tourid);            
        }//matchid

        [HttpGet]
        public IActionResult Dispute(string id)//matchid
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Dispute(string id,Dispute model)
        {
            var user = await GetCurrentUserAsync();
            var match = _TournamentContext.Tmatchs.Find(id);
            if (match.status != TourEnums.status.dispute)
                return NotFound();
            if (!TUtils.intourney(user, _TournamentContext.Tteams, match.tourid))
                return NotFound();
            var team = TUtils.GetTeam(user, _TournamentContext.Tteams, match.tourid);
            if (team.id != match.t1id && team.id != match.t2id)
                return NotFound();
            await _gameContext.Disputes.AddAsync(model);
            return RedirectToAction(nameof(_TournamentContext), match.tourid);//nope
        }


        #region helpers

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        #endregion
    }
}