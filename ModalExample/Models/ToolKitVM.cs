using System;
using System.Web;

namespace HabitatForHumanity.ViewModels
{
    public static class ToolKitVM
    {
        /// <summary>
        /// Creates a Modal with the given properties
        /// </summary>
        /// <param name="title">The title that the modal is to display.</param>
        /// <param name="divId">The id of the Modal div.</param>
        /// <param name="partialTarget">The innermost div that the partial page needs to target.</param>
        /// <returns></returns>
        public static IHtmlString Modal(string title, string divId, string partialTarget)
        {
            string htmlString = String.Format("<!-- Modal --><div class=\"modal fade\" id=\"{1}\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"{1}Label\"><div class=\"modal-dialog\" role=\"document\">" +
                "<div class=\"modal-content\"><div class=\"modal-header\">" +
                "<button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" +
                "<h4 class=\"modal-title\" id=\"{1}lLabel\">{0}</h4></div>" +
                " <div class=\"modal-body\">" +
                "<!--The Div to insert returned partial into--><div id = \"{2}\"></div></div></div></div></div>", title, divId, partialTarget);
            return new HtmlString(htmlString);
        }


        /// <summary>
        /// Constructs a generic modal with the given attributes. Modal body content is made up of an empty div. 
        /// Target the empty div with an ajax repsonse to populate the modal content. 
        /// </summary>
        /// <param name="modalId">Your modals id.</param>
        /// <param name="titleText">The title display of the modal.</param>
        /// <param name="partialTargetId">The id of the innermost div for partial pages.</param>
        /// <param name="formId">The id of the form to submit. This lets the form button exist outside of the form.</param>
        /// <returns>An html string of the modal.</returns>
        public static IHtmlString Modal(string modalId, string titleText, string partialTargetId, string formId)
        {

            //Structure from outer most to inner is as follows
            //modal(dialog(content(header(close, title), body(partial), footer))) 


            string partialTarget = "<div id='{0}'></div>";
            string modalBody = "<div class='modal-body'>{0}</div>";
            string modalHeader = "<div class='modal-header'>{0}{1}</div>";
            string modalHeaderClose = "<button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
            string modalHeaderTitle = "<h4 class='modal-title' id='{0}Label'>{1}</h4>"; //0 = myModal 1= title
            string modalContent = "<div class='modal-content'>{0}{1}{2}</div>";
            string modalDialog = "<div class='modal-dialog' role='document'>{0}</div>";
            string modalFooter = "<div class='modal-footer'>{0}{1}</div>";
            string modalFooterClose = "<button type='button' class='btn btn-default' data-dismiss='modal'>Close</button>";
            string modalFooterSubmit = "<button type='submit'  form='{0}' class='btn btn-primary'>Save changes</button>";
            string modal = "<div class='modal fade' id='{0}' tabindex='-1' role='dialog' aria-labelledby='{0}Label'>{1}</div>"; //0 = myModal 1= 

            //string fullModal = String.Format(modal, modalId, modalDialog);

            //build body
            partialTarget = String.Format(partialTarget, partialTargetId);
            modalBody = String.Format(modalBody, partialTarget);

            //build header
            modalHeaderTitle = String.Format(modalHeaderTitle, modalId, titleText);
            modalHeader = String.Format(modalHeader, modalHeaderClose, modalHeaderTitle);

            //build footer
            modalFooterSubmit = String.Format(modalFooterSubmit, formId);
            modalFooter = String.Format(modalFooter, modalFooterClose, modalFooterSubmit);

            //build content
            modalContent = String.Format(modalContent, modalHeader, modalBody, modalFooter);

            //build dialog
            modalDialog = String.Format(modalDialog, modalContent);

            //build modal
            modal = String.Format(modal, modalId, modalDialog);

            return new HtmlString(modal);
        }

        public static IHtmlString ModalEditButton(string buttonClass, string targetModal, string dataId)
        {
            string editButton = "<button type='button' dataId='{2}' class='btn btn-primary {0}' data-toggle='modal' data-target='#{1}'>Edit</button>";
            editButton = String.Format(editButton, buttonClass, targetModal, dataId);
            return new HtmlString(editButton);
        }

        public static IHtmlString ModalEditButtonGlyph(string buttonClass, string targetModal, string dataId, string optionalText)
        {
            string glyph = "";

            string editButton = "<button type='button' dataId='{2}' class='btn btn-primary {0}' data-toggle='modal' data-target='#{1}'>{3}{4}</button>";


            if(optionalText == null)
            {
                glyph = "<span class='glyphicon glyphicon-edit'></span>"; //no margin styling to keep the button size consistent
            }
            else
            {
                glyph = "<span class='glyphicon glyphicon-edit' style='margin-right:5px'></span>";
            }


            editButton = String.Format(editButton, buttonClass, targetModal, dataId, glyph, optionalText);
            return new HtmlString(editButton);
        }


        /// <summary>
        /// Creates a Button that can activate a modal.
        /// </summary>
        /// <param name="elementClass">The class of the button. Used for ajax targeting: $("[elementClass]").attr("dataId")</param>
        /// <param name="targetModal">The modal that opens when the button is clicked</param>
        /// <param name="innerHtml">Html inside of the button itself. Useful for using spans with glyphicons in them.</param>
        /// <param name="dataId">The id of the data type used by the button. If my VolunteerId is 1, my dataId would be 1.</param>
        /// <returns></returns>
        public static IHtmlString ModalButton(string elementClass, string targetModal, string innerHtml, string dataId)
        {
            string htmlString = String.Format("<button  dataId={3} type=\"button\" class=\"btn {0}\" data-toggle=\"modal\" data-target=\"#{1}\">{2}</button>", elementClass, targetModal, innerHtml, dataId);
            return new HtmlString(htmlString);
        }
    }


}