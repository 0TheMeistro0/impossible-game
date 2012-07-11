using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImpossibleGame
{
    /// <summary>
    /// This is the  container interface
    /// </summary>
    public interface IIoCContainer
    {
        /// <summary>
        /// Player which currently is using
        /// </summary>
        IPlayer Player { get; set; }
        
        /// <summary>
        /// Map, where player is currently posted
        /// </summary>
        IMap Map { get; set; }
    }
}
