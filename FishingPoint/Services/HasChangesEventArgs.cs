using System;

namespace FishingPoint.Services
{
    public class HasChangesEventArgs : EventArgs
    {
        public bool HasChanges { get; set; }
    }
}