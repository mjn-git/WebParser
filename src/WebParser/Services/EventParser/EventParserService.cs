using System;
using System.Collections.Generic;
using System.Linq;
using WebParser.Exceptions;
using WebParser.Model;

namespace WebParser.Services.EventParser
{
    public class EventParserService : IEventParserService
    {
        private const int FileColumnCount = 4;

        private const int ColumnName = 0;
        private const int ColumnDescription = 1;
        private const int ColumnStartTime = 2;
        private const int ColumnEndTime = 3;


        private const int MaxLenghtName = 32;
        private const int MaxLenghtDescription = 255;
        private const string TimeFormat = "yyyy-MM-ddTHH:mmzzz";


        private readonly char[] RowSplitter = { '\n', '\r' };
        private readonly char[] ColumnSplitter = { ';' };


        public IList<EventModel> LoadData(string fileContent)
        {
            var arrayData = GetArray(fileContent);
            if (arrayData?.Count == 0)
            {
                throw new WebParserException($"File doesn't contain any records.");
            }
            return GetEventList(arrayData);
        }

        private List<string[]> GetArray(string fileContent)
        {
            var arrayData = fileContent
                .Trim()
                .Split(RowSplitter)
                .Where(row => row.Length > 0)
                .Select(row => row
                               .Trim(ColumnSplitter)
                               .Split(ColumnSplitter)).ToList();

            return arrayData;
        }


        private static IList<EventModel> GetEventList(List<string[]> arrayData)
        {
            var eventList = new List<EventModel>();
            for (var i = 0; i < arrayData.Count; i++)
            {
                eventList.Add(ReadRow(arrayData[i], i));
            }
            return eventList;
        }

        private static EventModel ReadRow(string[] columns, int index)
        {
            if (columns.Length != FileColumnCount)
            {
                throw new WebParserException($"Row {index + 1} doesn't contain required {FileColumnCount} columns.");
            }

            return new EventModel
            {
                Name = GetEventName(columns, index),
                Description = GetEventDescription(columns, index),
                StartDate = GetEventStartDate(columns, index),
                EndDate = GetEventEndDate(columns, index),
            };
        }

        private static DateTime GetEventEndDate(string[] columns, int index)
        {
            var value = columns[ColumnEndTime];
            if (!TryParseFileTime(value, out DateTime time))
            {
                throw new WebParserException($"End time on row {index + 1} has to be in format yyyy-MM-ddTHH:mmzzz.");
            }
            return time;
        }

        private static DateTime GetEventStartDate(string[] columns, int index)
        {
            var value = columns[ColumnStartTime];
            if (!TryParseFileTime(value, out DateTime time))
            {
                throw new WebParserException($"Start time on row {index + 1} has to be in format yyyy-MM-ddTHH:mmzzz.");
            }
            return time;
        }

        private static bool TryParseFileTime(string value, out DateTime time)
        {
            if (DateTime.TryParse(value, out time) && time.ToString(TimeFormat) == value)
            {
                return true;

            }
            return false;
        }

        private static string GetEventDescription(string[] columns, int index)
        {
            var value = columns[ColumnDescription];
            if (string.IsNullOrEmpty(value))
            {
                throw new WebParserException($"Description on row {index + 1} cannot be empty.");
            }

            if (value.Length > MaxLenghtDescription)
            {
                throw new WebParserException($"Description on row {index + 1} cannot be longer then {MaxLenghtName}.");
            }
            return value;
        }

        private static string GetEventName(string[] columns, int index)
        {
            var value = columns[ColumnName];
            if (string.IsNullOrEmpty(value))
            {
                throw new WebParserException($"Name on row {index + 1} cannot be empty.");
            }

            if (value.Length > MaxLenghtName)
            {
                throw new WebParserException($"Name on row {index + 1} cannot be longer then {MaxLenghtName}.");
            }
            return value;
        }
    }
}
