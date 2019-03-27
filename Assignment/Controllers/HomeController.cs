namespace Assignment.Controllers
{
    using Assignment.Models;
    using Assignment.Repositories;
    using System;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string number)
        {
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(number))
            {
                double numberValue = 0;
                double.TryParse(number, out numberValue);

                var details = new PersonDetails
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Number = numberValue
                };

                var personDetails = new PersonDetailsRepository();
                bool result = personDetails.AddPersonDetails(details);
                if(result)
                {
                    ViewBag.Message = "Data has been submitted successfully.";
                }
                else
                {
                    ViewBag.Message = "We are facing problem submitting your data, pleas try again later.";
                }
            }
            return View("/Views/Home/Index.cshtml");
        }

        public ActionResult PersonDetailsList()
        {
            try
            {
                var personDetails = new PersonDetailsRepository();
                var model = personDetails.GetAllPersonDetails();
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}