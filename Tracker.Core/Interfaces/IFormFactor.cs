using System;
using System.Collections.Generic;
using System.Text;

namespace Tracker.Core.Interfaces
{
    public interface IFormFactor
    {
        public string GetFormFactor();
        public string GetPlatform();
    }
}
