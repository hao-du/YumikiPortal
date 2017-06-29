using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Database
{
    public class CommandQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, string> _setPairs;
        private string _conditions;

        public CommandQueryBuilder(string tableName)
        {
            _tableName = tableName;
            _setPairs = new Dictionary<string, string>();
            _conditions = string.Empty;
        }

        public void AppendSetPair(string columnName, string value)
        {
            _setPairs.Add(columnName, value);
        }

        public void AppendConditionPair(string conditions)
        {
            _conditions = conditions;
        }

        public string CreateInsertCommand()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("INSERT INTO ( ");
            foreach (KeyValuePair<string, string> setPair in _setPairs)
            {
                builder.AppendFormat("{0}, ", setPair.Key);
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append(" ) ");

            builder.Append(" VALUES ( ");
            foreach (KeyValuePair<string, string> setPair in _setPairs)
            {
                builder.AppendFormat("{0}, ", setPair.Value);
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append(" )");

            return builder.ToString();
        }

        public string CreateUpdateCommand()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} ", _tableName);

            builder.Append("SET ");
            foreach(KeyValuePair<string, string> setPair in _setPairs)
            {
                builder.AppendFormat("{0} = {1}, ", setPair.Key, setPair.Value);
            }
            builder.Remove(builder.Length - 2, 2);

            if (string.IsNullOrWhiteSpace(_conditions))
            {
                builder.AppendFormat("WHERE {0}", _conditions);
            }

            return builder.ToString();
        }

        public string CreateDeleteCommand()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM {0} ", _tableName);

            if (string.IsNullOrWhiteSpace(_conditions))
            {
                builder.AppendFormat("WHERE {0}", _conditions);
            }

            return builder.ToString();
        }
    }
}
