// See https://aka.ms/new-console-template for more information

using Dapper;
using Microsoft.Data.SqlClient;

Console.WriteLine("Please enter SqlConnection String:");
var _connectionString = Console.ReadLine();
var _commandText = "SELECT @DateTime";
var _dateTimeValue = new DateTime(2022, 10, 31, 23, 59, 59, 999, DateTimeKind.Unspecified);

using(var _conn = new SqlConnection(_connectionString))
{
    var _record = _conn.QueryFirst(_commandText, new { @DateTime = _dateTimeValue });

    /*
     * -- output --
     * 10/31/2022 11:59:59 PM
     * {DapperRow,  = '11/1/2022 12:00:00 AM'}
     */
    Console.WriteLine();
    Console.WriteLine("_dateTimeValue: {0}", _dateTimeValue);
    Console.WriteLine("Value from database {0}", _record);
    Console.WriteLine();
}

Console.WriteLine("press any key to exit");
Console.ReadKey();
