﻿using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.Models;

namespace YogaMo.Mobile.Views
{
    /// <summary>
    /// Implements the message data template selector class.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDataTemplateSelector" /> class.
        /// </summary>
        public MessageDataTemplateSelector()
        {
            this.IncomingTextTemplate = new DataTemplate(typeof(IncomingTextTemplate));
            this.OutgoingTextTemplate = new DataTemplate(typeof(OutgoingTextTemplate));
            this.IncomingImageTemplate = new DataTemplate(typeof(IncomingImageTemplate));
            this.OutgoingImageTemplate = new DataTemplate(typeof(OutgoingImageTemplate));
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the incoming text template.
        /// </summary>
        public DataTemplate IncomingTextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing text template.
        /// </summary>
        public DataTemplate OutgoingTextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the incoming image template.
        /// </summary>
        public DataTemplate IncomingImageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing text template.
        /// </summary>
        public DataTemplate OutgoingImageTemplate { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the incoming or outgoing text template.
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="container">The bindable object</param>
        /// <returns>Returns the data template</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null && ((PNMessage)item).User == APIService.CurrentUser.FullName)
            {
                return this.IncomingTextTemplate;
            }
            else
            {
                return this.OutgoingTextTemplate;
            }
        }

        #endregion
    }
}
