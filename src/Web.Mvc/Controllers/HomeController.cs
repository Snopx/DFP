using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserApp;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private IMapper _mapper { get; set; }
        public HomeController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var user= _mapper.Map<List<UserDto>>(_userService.Table.ToList());
            return View(user);
        }

        public async Task<IActionResult> Create(User u)
        {
            await _userService.AddAsync(u);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
