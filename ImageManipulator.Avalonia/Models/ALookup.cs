/* Image Manipulator - (C) 2021 Premysl Fara  */

namespace ImageManipulator.Avalonia.Models
{
    using System;
    
    
    /// <summary>
    /// The base class for all lookups.
    /// </summary>
    public abstract class ALookup : ILookup
    {
        public int Id { get; set; }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The name of a lookup cannot be empty.");
                }

                _name = value;
            }
        }

        
        public override string ToString()
        {
            return Name;
        }
    }
}