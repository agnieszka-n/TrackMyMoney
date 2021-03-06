﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Models;

namespace TrackMyMoney.Services.Contracts
{
    public interface ICategoriesManager
    {
        OperationResult<List<CostCategory>> GetCategories();
        OperationResult RenameCategory(int id, string newName);
        OperationResult AddCategory(string name);
        OperationResult DeleteCategory(int id);
    }
}
