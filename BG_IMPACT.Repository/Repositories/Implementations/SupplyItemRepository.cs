﻿using BG_IMPACT.Repository.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Implementations
{
    public class SupplyItemRepository : ISupplyItemRepository
    {
        private readonly SqlConnection _connection;

        public SupplyItemRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync().GetAwaiter().GetResult();
            }
        }


        //public async Task<object> spSupplyOrderCreate(object param)
       //{
            //object? result = await _connection.QueryFirstOrDefaultAsync("spSupplyOrderCreate", commandType: CommandType.StoredProcedure);
            //return result;
        //}


    }
}
