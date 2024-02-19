using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.WPF.Events
{
    public class CommCardEvent : PubSubEvent<IReadOnlyCollection<Card>>
    {

    }
}
