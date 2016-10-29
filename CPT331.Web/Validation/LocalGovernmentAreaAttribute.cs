﻿#region Using References

using System;
using System.ComponentModel.DataAnnotations;

using CPT331.Core.ObjectModel;
using CPT331.Data;

#endregion

namespace CPT331.Web.Validation
{
	public class LocalGovernmentAreaAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			bool isValid = false;

			if (value != null)
			{
				int id = 0;

				if (Int32.TryParse(value.ToString(), out id) == true)
				{
					LocalGovernmentArea localGovernmentArea = DataProvider.LocalGovernmentAreaRepository.GetLocalGovernmentAreaByID(id);

					isValid = (localGovernmentArea != null);
				}
			}

			return isValid;
		}
	}
}
