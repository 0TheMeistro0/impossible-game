using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImpossibleGame
{
    /// <summary>
    /// This is the  container interface
    /// </summary>
    public class IoCContainer : IIoCContainer
    {
        /// <summary>
        /// This is the player which currently is using
        /// </summary>
        public IPlayer Player { get; set; }
    }
}
