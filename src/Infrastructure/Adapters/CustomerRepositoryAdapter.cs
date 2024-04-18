﻿using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.Extensions;

namespace Infrastructure.Repositories;

public class CustomerRepositoryAdapter : ICustomerRepository
{
    private readonly ICustomerSqlRepository _customerSqlRepository;
    public CustomerRepositoryAdapter(ICustomerSqlRepository customerSqlRepository)
    {
        _customerSqlRepository = customerSqlRepository;
    }

    public async Task<Customer> GetByCpf(string cpf, CancellationToken cancellationToken)
    {
        var customerSql = await _customerSqlRepository.GetAsync(
            customer => customer.Cpf.Equals(cpf), 
            cancellationToken);

        return customerSql.ToCustomer();
    }
}