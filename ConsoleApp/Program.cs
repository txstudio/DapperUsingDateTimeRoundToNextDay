// See https://aka.ms/new-console-template for more information

using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Please enter SqlConnection String:");
var _connectionString = Console.ReadLine();
var _commandText = "SELECT @DateTime, @DateTime2, @ABC";
var _storedProcName = "dbo.DateTimeParameter";
/*
CREATE PROCEDURE [dbo].[DateTimeParameter]
	@DateStart	DATETIME,
	@DateEnd	DATETIME
AS
	SELECT @DateStart [DateStart], @DateEnd [DateEnd]
GO
 */
var _dateTimeValue = new DateTime(2022, 10, 31, 23, 59, 59, 999, DateTimeKind.Unspecified);

using(var _conn = new SqlConnection(_connectionString))
{
    SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTime2);

    /*
    var _record = await _conn.QueryFirstAsync(_commandText, new { 
        DateTime = _dateTimeValue, 
        ABC = "abc",
        DateTime2 = new DateTime(2022, 10, 31, 23, 59, 59, 999)
    });
    */
    /*
     * -- output --
     * 10/31/2022 11:59:59 PM
     * {DapperRow,  = '11/1/2022 12:00:00 AM'}
     */

    var _record = await _conn.QueryFirstAsync(_storedProcName
        , new { 
            DateStart = new DateTime(2022, 10, 31, 23, 59, 59, 999),
            DateEnd = new DateTime(2022, 8, 31, 23, 59, 59, 999)
        }
        , commandType: CommandType.StoredProcedure);
    /*
     * -- output --
     * 10/31/2022 11:59:59 PM
     * {DapperRow, DateStart = '11/1/2022 12:00:00 AM', DateEnd = '9/1/2022 12:00:00 AM'}
     */

    Console.WriteLine();
    Console.WriteLine("_dateTimeValue: {0}", _dateTimeValue);
    Console.WriteLine("Value from database {0}", _record);
    Console.WriteLine();
}

Console.WriteLine("press any key to exit");
Console.ReadKey();
