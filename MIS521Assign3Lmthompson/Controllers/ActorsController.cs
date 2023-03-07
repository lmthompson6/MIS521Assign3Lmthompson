using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIS521Assign3Lmthompson.Data;
using MIS521Assign3Lmthompson.Models;
using Tweetinvi;
using VaderSharp2;

namespace MIS521Assign3Lmthompson.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Actor.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            ActorTweetsVM vm = new ActorTweetsVM();
            vm.Actor = actor;
            var Tweets = new List<Tweet>();
            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(actor.Name);
            var tweetsResponse = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();
            double sentScoreAvg = 0;
            for (int i = 0; i < tweetsResponse.Length; i++)
            {
                Tweet newTweet = new Tweet();
                newTweet.TweetText = tweetsResponse[i].Text;
                var results = analyzer.PolarityScores(tweetsResponse[i].Text);
                newTweet.SentScore = results.Compound;
                sentScoreAvg += results.Compound;
                Tweets.Add(newTweet);
            }
            vm.Tweets = Tweets;
            vm.ScoreAvg = (sentScoreAvg / tweetsResponse.Length);

            return View(vm);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,IMDB")] Actor actor, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await Image.CopyToAsync(memoryStream);
                    actor.Image = memoryStream.ToArray();
                }
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        public async Task<IActionResult> GetActorImage(int id)
        {
            var actor = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            var imageData = actor.Image;
            return File(imageData, "image/jpg");
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,IMDB")] Actor actor, IFormFile Image)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await Image.CopyToAsync(memoryStream);
                        actor.Image = memoryStream.ToArray();
                    }
/*                    else
                    {
                        var actorSearch = await _context.Actor.FirstOrDefaultAsync(a => a.Id == id);
                        Console.WriteLine("made it");
                        actor.Image = actorSearch.Image;
                    }*/
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actor'  is null.");
            }
            var actor = await _context.Actor.FindAsync(id);
            if (actor != null)
            {
                _context.Actor.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
          return _context.Actor.Any(e => e.Id == id);
        }
    }
}
