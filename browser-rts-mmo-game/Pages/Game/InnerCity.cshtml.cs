﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Internal;
using BrowserGame.Static;

namespace BrowserGame.Pages.Game
{
    public class InnerCityModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InnerCityModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Player player = await _context.Players.Include(p => p.Capital)
                                                   .FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

                if (player == null) return Redirect("/Game/CreatePlayer");

                id = player.Capital.Id;
            }

            CityManager cityManager = await CityManager.LoadCityManagerAsync(id ?? 0, _context);

            if (cityManager.NotUsers(User))
            {
                return NotFound();
            }

            ViewData["CityManager"] = cityManager;
            return Page();
        }
    }
}