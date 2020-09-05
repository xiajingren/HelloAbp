using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    public class GetAllOrgnizationUnitInput : ISortedResultRequest
    {
        public GetAllOrgnizationUnitInput()
        {
            Sorting = "Code";
        }
        public string Filter { get; set; }
        public string Sorting { get; set; }
    }
}
