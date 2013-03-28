
namespace Maverick.ViewModels.General
{
    public class SelectElement
    {
        #region Constructors

        /// <summary>
        /// an object containing an id and display name,
        /// to be used in dropdowns
        /// </summary>
        public SelectElement ()
        {
        }

        public SelectElement (long ID, string DisplayName)
        {
            this.ID = ID;
            this.DisplayName = DisplayName;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Element id
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Element display name
        /// </summary>
        public string DisplayName { get; set; }

        #endregion
    }
}
