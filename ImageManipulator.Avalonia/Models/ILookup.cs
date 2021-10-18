/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Models
{
    /// <summary>
    /// Represents a lookup.
    /// </summary>
    public interface ILookup
    {
        /// <summary>
        /// An unique ID of a lookup item.
        /// </summary>
        int Id { get; set; }
        
        /// <summary>
        /// A name of a lookup item.
        /// </summary>
        string Name { get; set; }
    }
}