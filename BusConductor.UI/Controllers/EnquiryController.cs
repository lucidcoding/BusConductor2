using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusConductor.Application.Contracts;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.Enquiry;
using BusConductor.UI.ViewModels.Enquiry;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Text;

namespace BusConductor.UI.Controllers
{
    public class EnquiryController : Controller
    {
        private readonly IEnquiryService _enquiryService;
        private readonly IBusRepository _busRepository;

        public EnquiryController(
            IEnquiryService enquiryService,
            IBusRepository busRepository)
        {
            _enquiryService = enquiryService;
            _busRepository = busRepository;
        }
        
        [Log]
        [EntityFrameworkReadContext]
        public ActionResult Make()
        {
            var busId = new Guid("6a9857a6-d0b0-4e1a-84cb-ee9ade159560");
            var bus = _busRepository.GetById(busId);
            var viewModel = MakeViewModelMapper.Map(bus);
            return View(viewModel);
        }

        [Log]
        [HttpPost]
        [TransactionScope]
        [EntityFrameworkWriteContext]
        public ActionResult Make(MakeViewModel inViewModel)
        {
            var request = MakeViewModelMapper.Map(inViewModel);
            var bus = _busRepository.GetById(inViewModel.BusId);
            var validationMessages = _enquiryService.ValidateMake(request);
            validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));

            if (!ModelState.IsValid)
            {
                MakeViewModelMapper.Hydrate(inViewModel, bus);
                return View("Make", inViewModel);
            }

            _enquiryService.Make(request);

            //todo: put somewhere else
            var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

            smtpClient.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["SmtpServerAccount"],
                ConfigurationManager.AppSettings["SmtpServerPassword"]);

            var enquirySummary = new StringBuilder();
            enquirySummary.AppendLine(string.Format("Camper: {0}", bus.Name));
            enquirySummary.AppendLine(string.Format("Pick-up date: {0:dd/MM/yyyy}", inViewModel.PickUp));
            enquirySummary.AppendLine(string.Format("Drop-off date: {0:dd/MM/yyyy}", inViewModel.DropOff));
            enquirySummary.AppendLine(string.Format("Forename: {0}", inViewModel.Forename));
            enquirySummary.AppendLine(string.Format("Surname: {0}", inViewModel.Surname));
            enquirySummary.AppendLine(string.Format("Address line 1: {0}", inViewModel.AddressLine1));
            enquirySummary.AppendLine(string.Format("Address line 2: {0}", inViewModel.AddressLine2));
            enquirySummary.AppendLine(string.Format("Address line 3: {0}", inViewModel.AddressLine3));
            enquirySummary.AppendLine(string.Format("Town: {0}", inViewModel.Town));
            enquirySummary.AppendLine(string.Format("County: {0}", inViewModel.County));
            enquirySummary.AppendLine(string.Format("Post code: {0}", inViewModel.PostCode));
            enquirySummary.AppendLine(string.Format("Email: {0}", inViewModel.Email));
            enquirySummary.AppendLine(string.Format("Telephone number: {0}", inViewModel.TelephoneNumber));
            enquirySummary.AppendLine(string.Format("Person booking is main driver: {0}", inViewModel.IsMainDriver));
            enquirySummary.AppendLine(string.Format("Driving licence number of driver: {0}", inViewModel.DrivingLicenceNumber));
            enquirySummary.AppendLine(string.Format("Forename of driver: {0}", inViewModel.DriverForename));
            enquirySummary.AppendLine(string.Format("Surname of driver: {0}", inViewModel.DriverSurname));
            enquirySummary.AppendLine(string.Format("Number of adults: {0}", inViewModel.NumberOfAdults));
            enquirySummary.AppendLine(string.Format("Number of children: {0}", inViewModel.NumberOfChildren));

            var confirmationBody = new StringBuilder();
            confirmationBody.AppendLine(string.Format("Dear {0},", inViewModel.Forename));
            confirmationBody.AppendLine();
            confirmationBody.AppendLine("Thank you for your enquiry with Cool Cat Campers. One of our team will contact you as soon as possible to confirm your booking.");
            confirmationBody.AppendLine();
            confirmationBody.AppendLine("The details of your enquiry are shown below.");
            confirmationBody.AppendLine();
            confirmationBody.Append(enquirySummary.ToString());
            confirmationBody.AppendLine();
            confirmationBody.AppendLine("Best regards,");
            confirmationBody.AppendLine("Cool Cat Campers");

            var notificationBody = new StringBuilder();
            notificationBody.AppendLine("You have received a booking enquiry. The details are shown below.");
            notificationBody.AppendLine();
            notificationBody.Append(enquirySummary.ToString());

            MailMessage mailMessage = new MailMessage();
            mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["SmtpServerAccount"]);
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SmtpServerAccount"]);
            mailMessage.To.Add(inViewModel.Email);
            mailMessage.Body = confirmationBody.ToString();
            mailMessage.Subject = "Booking Enquiry Confirmation";
            smtpClient.Send(mailMessage);

            mailMessage = new MailMessage();
            mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["SmtpServerAccount"]);
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SmtpServerAccount"]);
            mailMessage.To.Add(ConfigurationManager.AppSettings["MainAdminEmail"]);
            mailMessage.Body = notificationBody.ToString();
            mailMessage.Subject = "Booking Enquiry";
            smtpClient.Send(mailMessage);

            return View("Confirmation");
        }
    }
}
