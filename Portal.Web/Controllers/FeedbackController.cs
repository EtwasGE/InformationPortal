using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Authorization;
using Abp.AutoMapper;
using Portal.Application.Feedback;
using Portal.Application.Feedback.Dto;
using Portal.Core.Authorization;
using Portal.Core.Content;
using Portal.Web.Controllers.Common;
using Portal.Web.Models.Feedback;

namespace Portal.Web.Controllers
{
    [AbpAuthorize]
    public class FeedbackController : AppControllerBase
    {
        private readonly IFeedbackAppService _feedbackService;
        public FeedbackController(IFeedbackAppService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // this property is injection (see WebModule)
        public string ActiveMenuItemName { get; set; }

        #region Error Report

        [HttpPost]
        public ActionResult ErrorReportModal(int contentId)
        {
            var subjects = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = MessageSubjectType.Grammatical.ToString(),
                    Text = L("ErrorReportGrammatical")
                },

                new SelectListItem
                {
                    Value = MessageSubjectType.Content.ToString(),
                    Text = L("ErrorReportContent")
                },

                new SelectListItem
                {
                    Value = MessageSubjectType.LinkBroken.ToString(),
                    Text = L("ErrorReportLinkBroken")
                },

                new SelectListItem
                {
                    Value = MessageSubjectType.Other.ToString(),
                    Text = L("ErrorReportOther")
                }
            };

            var model = new ErrorReportModalViewModel
            {
                ContentId = contentId,
                Subjects = subjects
            };

            return View("_ErrorReportModal", model);
        }

        [HttpPost]
        public async Task AddErrorReport(AddErrorReportViewModel report)
        {
            CheckModelState();

            var input = report.MapTo<AddErrorReportInput>();
            await _feedbackService.AddErrorReportAsync(input);

            // TODO: реализовать
            // Отправить RealTime сообщение пользователю о том, что его сообщение об ошибке 
            // будет рассмотрено в ближайшее время
            // Отправить всем Approvers уведомление о том, что добавлено сообщение об ошибке
        }

        [AbpAuthorize(PermissionNames.ContentChange)]
        public async Task<ActionResult> ErrorReports(int? page)
        {
            var input = new AllErrorReportInput
            {
                PageIndex = page - 1 ?? 0,
                PageSize = 25
            };

            var output = await _feedbackService.GetErrorReportsAsync(input);
            var model = output.MapTo<PreViewErrorReportViewModel>();
            model.ActiveMenu = ActiveMenuItemName;

            if (Request.IsAjaxRequest())
                return PartialView("_ErrorReports", model);
            return View("ErrorReports", model);
        }

        #endregion
    }
}