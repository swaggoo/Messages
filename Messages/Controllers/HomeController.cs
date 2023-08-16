using Microsoft.AspNetCore.Mvc;
using Messages.Models.ViewModels;
using Messages.Models;
using Messages.IRepository;

namespace Messages.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMessageRepository _messageRepository;

    public HomeController(
        ILogger<HomeController> logger,
        IMessageRepository messageRepository)
    {
        _logger = logger;
        _messageRepository = messageRepository;
    }

    public IActionResult Index()
    {
        var userId = GetUserId();
        _logger.LogInformation("Fetching messages for user: {UserId}", userId);
        var messagesViewModel = new MessagesViewModel
        {
            AllMessages = _messageRepository.GetAllMessages(),
            UserMessages = _messageRepository.GetUserMessages(userId),
        };

        return View(messagesViewModel);
    }


    // GET
    public IActionResult Create()
    {
        return View();
    }


    // POST
    [HttpPost]
    public IActionResult Create(Message message)
    {
        if (ModelState.IsValid)
        {
            var userId = GetUserId();
            _logger.LogInformation("Creating a new message for user: {UserId}", userId);

            message.UserId = userId;
            _messageRepository.AddMessage(message);
            _logger.LogInformation("Message created: {MessageText}", message.Text);

            return RedirectToAction("Index");
        }

        return View(message);
    }

    private string GetUserId()
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (userId == null)
        {
            userId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("UserId", userId);
        }

        return userId;
    }
}
