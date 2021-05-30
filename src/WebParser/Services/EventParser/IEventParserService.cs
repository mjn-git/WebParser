using System.Collections.Generic;
using WebParser.Model;

namespace WebParser.Services.EventParser
{
    public interface IEventParserService
    {
        IList<EventModel> LoadData(string fileContent);
    }
}
