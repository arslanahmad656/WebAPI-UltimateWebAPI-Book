﻿namespace Contracts;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }

    IEmployeeRepository Employee { get; }

    public Task Save();
}
