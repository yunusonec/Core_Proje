using Core_Proje.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    public class ProfileController : Controller
    {
        public readonly UserManager<WriterUser> _userManager;

        public ProfileController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = values.Name;
            model.Surname = values.SurName;
            model.PictureUrl = values.ImageUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(p.Picture != null)
            {
                // Şu anki çalışma dizinini alır
                var resource = Directory.GetCurrentDirectory();
                // Yüklenen dosyanın uzantısını alır
                var extension = Path.GetExtension(p.Picture.FileName);
                // Dosya adını benzersiz bir şekilde oluşturur
                var imagename = Guid.NewGuid() + extension;
                // Dosyanın kaydedileceği konumu oluşturur
                var savelocation = resource + "/wwwroot/userimage/" + imagename;
                // Dosyayı oluşturmak için bir dosya akışi oluşturur
                var stream = new FileStream(savelocation, FileMode.Create);
                // Yüklenen dosyayı oluşturulan dosya akışına kopyalar
                await p.Picture.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.Name = p.Name;
            user.SurName = p.Surname;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
