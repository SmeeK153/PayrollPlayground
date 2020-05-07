using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using API.Controllers.Companies.v1.Requests;
using API.Controllers.Companies.v1.Responses;
using API.Extensions;
using Domain.ValueObjects;
using Infrastructure.Proxies;
using Infrastructure.Proxies.Companies.Requests;
using Infrastructure.Proxies.People.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Companies.v1
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("v{version:apiVersion}/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IRequestService _requestService { get; }

        public CompanyController(IRequestService requestService) => (_requestService) = (requestService);
        
        /// <summary>
        /// Retrieves all companies available to the user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<CompanySummary>>> GetCompanies()
        {
            var result = await _requestService.Execute(new GetAllCompanies());
            var companies = result.ToList();
            if (!companies.Any())
            {
                return NoContent();
            }
            var companySummaries = companies.Select(company => company.ToSummary());
            return Ok(companySummaries);
        }

        /// <summary>
        /// Retrieves details on a specific company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CompanyDetail>> GetCompany(Guid id)
        {
            var company = await _requestService.Execute(new GetCompany {Id = id});
            if (company is null)
            {
                return NotFound();
            }
            var companyDetails = company.ToDetail();
            return Ok(companyDetails);
        }
        
        /// <summary>
        /// Registers a new company on the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CompanyDetail>> CreateCompany(CreateNewCompanyRequest request)
        {
            var company = await _requestService.Execute(new CreateNewCompany
            {
                Name = request.Name,
                PaychecksPerYear = request.PaychecksPerYear
            });
            var companyDetails = company.ToDetail();
            return Ok(companyDetails);
        }
        
        /// <summary>
        /// Removes an existing company from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            var company = await _requestService.Execute(new GetCompany {Id = id});
            if (company is null)
            {
                return NotFound();
            }
            company.CloseCompany();
            return NoContent();
        }

        /// <summary>
        /// Associates an employee with a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{companyId}/employees")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<EmployeeDetail>> AddEmployeeToCompany(Guid companyId, AddEmployeeToCompanyRequest request)
        {
            var company = await _requestService.Execute(new GetCompany {Id = companyId});
            if (company is null)
            {
                return NotFound();
            }
            var person = await _requestService.Execute(new GetOrCreatePerson
            {
                TaxIdentificationNumber = request.TaxIdentificationNumber,
                Name = new Name(request.EmployeeFirstName, request.EmployeeLastName)
            });
            var employee = company.AddEmployee(person);
            var employeeDetail = employee.ToDetail();
            return Ok(employeeDetail);
        }
        
        /// <summary>
        /// Retrieves an employee at a specified company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{companyId}/employees/{employeeId}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<EmployeeDetail>> LookupEmployeeAtCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _requestService.Execute(new GetEmployee
            {
                Company = companyId,
                Employee = employeeId
            });
            if (employee is null)
            {
                return NotFound();
            }
            var employeeDetail = employee.ToDetail();
            return Ok(employeeDetail);
        }

        [HttpPost("{companyId}/employees/{employeeId}/dependents")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<EmployeeDetail>> AddDependentToEmployee(Guid companyId, Guid employeeId, AddDependentToEmployeeRequest request)
        {
            var company = await _requestService.Execute(new GetCompany {Id = companyId});
            if (company is null)
            {
                return NotFound();
            }

            var employee = company.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee is null)
            {
                return NotFound();
            }
            
            var person = await _requestService.Execute(new GetOrCreatePerson
            {
                TaxIdentificationNumber = request.TaxIdentificationNumber,
                Name = new Name(request.DependentFirstName, request.DependentLastName)
            });
            employee.AddDependent(person);
            return Ok(employee);
        }

        /// <summary>
        /// De-associates an employee with a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("{companyId}/employees/{employeeId}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> RemoveEmployeeFromCompany(Guid companyId, Guid employeeId)
        {
            var company = await _requestService.Execute(new GetCompany {Id = companyId});
            if (company is null)
            {
                return NotFound();
            }
            company.RemoveEmployee(employeeId);
            return NoContent();
        }
    }
}