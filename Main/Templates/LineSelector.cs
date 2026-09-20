using Main.ViewModels;

namespace Main.Templates
{
    public class LineSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template compte courant
        /// </summary>
        public required DataTemplate NoteTemplate { get; set; }

        /// <summary>
        /// Template compte courant
        /// </summary>
        public required DataTemplate WorkTemplate { get; set; }

        /// <summary>
        /// Template compte courant
        /// </summary>
        public required DataTemplate ProductTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                ProductViewModel => ProductTemplate,
                WorkViewModel => WorkTemplate,
                _ => NoteTemplate
            };
        }
    }
}
