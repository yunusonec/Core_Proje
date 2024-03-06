using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.ViewComponents.Testimonial
{
    public class TestimonialList:ViewComponent
    {
        TestimonilalManager testimonilalManager = new TestimonilalManager(new EfTestiMonialDal());

        public IViewComponentResult Invoke()
        {
            var values = testimonilalManager.TGetList();
            return View(values);
        }
    }
}
