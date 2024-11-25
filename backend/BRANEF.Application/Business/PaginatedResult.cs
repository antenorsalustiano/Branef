﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRANEF.Application.Business
{
	public class PaginatedResult<T>
	{
		public List<T> Data { get; set; } = new();
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public bool Ok { get; set; } = true;
		public string? Message { get; set; }
	}
}
