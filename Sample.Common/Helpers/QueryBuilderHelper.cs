using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Helpers;

public static class QueryBuilderHelper
{
        #region [Method(s)]

        public static string BuildInsertQuery(this string query) =>
                query.Replace("@IsDeleted", "0")
                        .Replace("@CreationDate", $"'{DateTime.Now.ToString(CultureInfo.CurrentCulture)}'")
                        .Replace("@LastUpdated", $"'{DateTime.Now.ToString(CultureInfo.CurrentCulture)}'");

        public static string AddSoftDelete(this string query) =>
                query + "IsDeleted = 0";

        public static string AddSoftDeleteWithAnd(this string query) =>
                query + " And ".AddSoftDelete();

        #endregion
}
