﻿using System;
using System.Web;
using System.Web.Helpers;

namespace ToolKit
{
    //trash can button
    //do you really want to kill trees?
    //pass buttons to modal for better control over save/close and delete abilities
    public static class ToolKitVM
    {
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
            string modalFooterClose = "<button type='button' id='modalCloseButton' class='btn btn-default' data-dismiss='modal'>Close</button>";
            string modalFooterSubmit = "<button type='submit' id='modalSubmitButton'  form='{0}' class='btn btn-primary'>Save changes</button>";
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



        /// <summary>
        /// Constructs a modal and an ajax handler for passing data from a partial in a modal to a controller action.
        /// This method is most useful when you're trying to do data validation with a partial view inside of a modal.
        /// By default forms will attempt to redirect to a returned action result which will close the modal. This 
        /// modal prevents the form from navigating away while using ajax to speak with the controller asynchronously. That 
        /// way if a partial validation is returned it can be reinserted into the modal without disorienting users.
        /// </summary>
        /// <param name="modalId">Your modals id.</param>
        /// <param name="titleText">The title display of the modal.</param>
        /// <param name="partialTargetId">The id of the innermost div for partial pages.</param>
        /// <param name="formId">The id of the form to submit. This lets the form button exist outside of the form.</param>
        /// <returns>An html string of the modal.</returns>
        public static IHtmlString ModalPreventDefault(string modalId, string titleText, string partialTargetId, string formId)
        {

            //0 is modalId, 1 is formId ,2 is ajax
            string script = "<script type='text/javascript'>{0}</script>";
            string function = " $('#{0}').on('submit',function(e){{e.preventDefault(); {1}{2}{3}{4} }});";

            string action = "var action = $('#{0}').attr('action');"; // 0 is formId
            string method = "var method = $('#{0}').attr('method');";
            string form = "var form = $('#{0}');"; // 0 is formid

            string ajaxCall = "$.ajax({{ url: action, type: method, data: form.serialize(), success: function(response)" +
                "{{ $('#{0}').html(response);}} }});";

            //build action
            action = String.Format(action, formId);
            method = String.Format(method, formId);
            ajaxCall = String.Format(ajaxCall, partialTargetId);
            form = String.Format(form, formId);

            function = String.Format(function, modalId, action, method, form, ajaxCall);

            script = String.Format(script, function);

            IHtmlString modal = Modal(modalId, titleText, partialTargetId, formId);

            string modalWithJS = String.Format("{0}{1}", modal.ToString(), script);

            return new HtmlString(modalWithJS);

        }

        /// <summary>
        /// Creates a generic edit button. The button only contains the text 'Edit'.
        /// </summary>
        /// <param name="buttonClass">An extra defined class given to the button for jquery targetting.</param>
        /// <param name="targetModal">The modal that should open when the button is clicked.</param>
        /// <param name="dataId">The id of the object this button should edit.</param>
        /// <returns>An html string of the button.</returns>
        public static IHtmlString ModalEditButton(string buttonClass, string targetModal, string dataId)
        {
            string editButton = "<button type='button' dataId='{2}' class='btn btn-primary {0}' data-toggle='modal' data-target='#{1}'>Edit</button>";
            editButton = String.Format(editButton, buttonClass, targetModal, dataId);
            return new HtmlString(editButton);
        }


        /// <summary>
        /// Creates an edit button with a glyphicon. 
        /// </summary>
        /// <param name="buttonClass">An extra defined class given to the button for jquery targetting.</param>
        /// <param name="targetModal">The modal that should open when the button is clicked.</param>
        /// <param name="dataId">The id of the object this button should edit.</param>
        /// <param name="optionalText">Optional text to render beside the glyphicon.</param>
        /// <returns>An html string of the button.</returns>
        public static IHtmlString ModalEditButtonGlyph(string buttonClass, string targetModal, string dataId, string optionalText)
        {
            string glyph = "";

            string editButton = "<button type='button' dataId='{2}' class='btn btn-primary {0}' data-toggle='modal' data-target='#{1}'>{3}{4}</button>";


            if (optionalText == null)
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
        /// Builds a script for handling GET requests that load partial views into a modal.
        /// </summary>
        /// <param name="action">The action method to invoke.</param>
        /// <param name="controller">The controller where the action is located.</param>
        /// <param name="buttonClass">An extra defined class given to the button for jquery targetting.</param>
        /// <param name="partialTarget">The partial div in the modal where the returned html is supposed to go.</param>
        public static IHtmlString ButtonScript(string buttonClass, string action, string controller, string partialTarget)
        {

            string url = String.Format("'/{0}/{1}'", controller, action);
            string ajaxCall = "$.ajax( {{ url: {0}, type: 'GET', data: inputData, success: function(response){{ $('#{1}').html(response); }} }});";

            ajaxCall = String.Format(ajaxCall, url, partialTarget);

            string function = "$('.{0}').click(function() {{ $('#modalSubmitButton').show(); var id = $(this).attr('dataId'); inputData = {{ 'id': id }}; {1} }})";

            function = String.Format(function, buttonClass, ajaxCall);

            string script = "<script type='text/javascript'>{0}</script>";

            script = String.Format(script, function);

            return new HtmlString(script);
        }

        /// <summary>
        /// Builds a small script for hiding the modal submit button after a success page is shown. Results in better page flow.
        /// Refreshes the page after the close button is clicked.
        /// </summary>
        /// <returns>An html string of a script tag.</returns>
        public static IHtmlString HideModalButtonOnSuccess()
        {
            var script = "<script type='text/javascript'>{0}{1}</script>";
            var function = " $(document).ready(function () {{ $('#modalSubmitButton').hide(); }} );";
            var refresh = "$('#modalCloseButton').click(function() {{ location.reload(); }});";
            
            script = String.Format(script, function, refresh);
            return new HtmlString(script);

        }
    }
}