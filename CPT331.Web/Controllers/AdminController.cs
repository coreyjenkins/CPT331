﻿#region Using References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;

using CPT331.Core.ObjectModel;
using CPT331.Data;
using CPT331.Web.Models.Admin;

#endregion

namespace CPT331.Web.Controllers
{
    public class AdminController : Controller
    {
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Crime(uint id)
		{
			CrimeModel crimeModel = null;
			Crime crime = CrimeRepository.GetCrimeByID((int)(id));

			if (crime != null)
			{
				crimeModel = new CrimeModel
				(
					crime.Count,
					crime.DateCreatedUtc,
					crime.DateUpdatedUtc,
					crime.ID,
					crime.IsDeleted,
					crime.IsVisible,
					crime.LocalGovernmentAreaID,
					crime.Month,
					crime.OffenceID,
					crime.Year
				);
			}

			return View(crimeModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Crime(CrimeModel crimeModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				if (crimeModel.IsDelete == true)
				{
					//	Can't delete them at this time
				}
				else
				{
					CrimeRepository.UpdateCrime
					(
						crimeModel.ID,
						crimeModel.LocalGovernmentAreaID,
						crimeModel.OffenceID,
						crimeModel.Count,
						crimeModel.Month,
						crimeModel.Year,
						crimeModel.IsDeleted,
						crimeModel.IsVisible
					);
				}

				actionResult = RedirectToAction("Offences", "Admin");
			}
			else
			{
				actionResult = View(crimeModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Crimes(string sortBy, SortDirection? sortDirection, uint? page)
		{
			IEnumerable<Crime> crimes = CrimeRepository.GetCrimes();

			if ((String.IsNullOrEmpty(sortBy) == false) && (sortDirection.HasValue == true))
			{
				SortDirection sort = sortDirection.Value;

				switch (sortBy)
				{
					case "Date":
						if (sort == SortDirection.Ascending)
						{
							crimes = crimes.OrderBy(m => (m.DateCreatedUtc));	//	.ThenBy(m => (m.Name));
						}
						else
						{
							crimes = crimes.OrderByDescending(m => (m.DateCreatedUtc));	//	.ThenBy(m => (m.Name));
						}
						break;

					case "ID":
						if (sort == SortDirection.Ascending)
						{
							crimes = crimes.OrderBy(m => (m.ID));
						}
						else
						{
							crimes = crimes.OrderByDescending(m => (m.ID));
						}
						break;

					case "IsDeleted":
						if (sort == SortDirection.Ascending)
						{
							crimes = crimes.OrderBy(m => (m.IsDeleted));	//	.ThenBy(m => (m.Name));
						}
						else
						{
							crimes = crimes.OrderByDescending(m => (m.IsDeleted));	//	.ThenBy(m => (m.Name));
						}
						break;

					case "IsVisible":
						if (sort == SortDirection.Ascending)
						{
							crimes = crimes.OrderBy(m => (m.IsVisible));	//	.ThenBy(m => (m.Name));
						}
						else
						{
							crimes = crimes.OrderByDescending(m => (m.IsVisible));	//	.ThenBy(m => (m.Name));
						}
						break;

					//	case "Name":
					//		if (sort == SortDirection.Ascending)
					//		{
					//			crimes = crimes.OrderBy(m => (m.Name));
					//		}
					//		else
					//		{
					//			crimes = crimes.OrderByDescending(m => (m.Name));
					//		}
					//		break;
				}
			}

			return View(crimes);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Home()
		{
			// User isn't logged in. 
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Account");
			}
			return View();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult LocalGovernmentArea(uint id)
		{
			LocalGovernmentAreaModel localGovernmentAreaModel = null;
			LocalGovernmentArea localGovernmentArea = LocalGovernmentAreaRepository.GetLocalGovernmentAreaByID((int)(id));

			if (localGovernmentArea != null)
			{
				localGovernmentAreaModel = new LocalGovernmentAreaModel
				(
					localGovernmentArea.DateCreatedUtc,
					localGovernmentArea.DateUpdatedUtc,
					localGovernmentArea.ID,
					localGovernmentArea.IsDeleted,
					localGovernmentArea.IsVisible,
					localGovernmentArea.Name,
					localGovernmentArea.StateID
				);
			}

			return View(localGovernmentAreaModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult LocalGovernmentArea(LocalGovernmentAreaModel localGovernmentAreaModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				if (localGovernmentAreaModel.IsDelete == true)
				{
					//	Can't delete them at this time
				}
				else
				{
					LocalGovernmentAreaRepository.UpdateLocalGovernmentArea
					(
						localGovernmentAreaModel.ID,
						localGovernmentAreaModel.IsDeleted,
						localGovernmentAreaModel.IsVisible,
						localGovernmentAreaModel.Name,
						localGovernmentAreaModel.StateID
					);
				}

				actionResult = RedirectToAction("LocalGovernmentAreas", "Admin");
			}
			else
			{
				actionResult = View(localGovernmentAreaModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult LocalGovernmentAreas(string sortBy, SortDirection? sortDirection, uint? page)
		{
			IEnumerable<LocalGovernmentAreaState> localGovernmentAreaStates = LocalGovernmentAreaStateRepository.GetLocalGovernmentAreaStates();

			if ((String.IsNullOrEmpty(sortBy) == false) && (sortDirection.HasValue == true))
			{
				SortDirection sort = sortDirection.Value;

				switch (sortBy)
				{
					case "Date":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.DateCreatedUtc)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.DateCreatedUtc)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						break;

					case "ID":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.ID));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.ID));
						}
						break;

					case "IsDeleted":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.IsDeleted)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.IsDeleted)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						break;

					case "IsVisible":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.IsVisible)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.IsVisible)).ThenBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						break;

					case "Name":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.Name)).ThenBy(m => (m.StateName));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.Name)).ThenBy(m => (m.StateName));
						}
						break;

					case "StateName":
						if (sort == SortDirection.Ascending)
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderBy(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						else
						{
							localGovernmentAreaStates = localGovernmentAreaStates.OrderByDescending(m => (m.StateName)).ThenBy(m => (m.Name));
						}
						break;
				}
			}

			return View(localGovernmentAreaStates);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewCrime()
		{
			return View(new CrimeModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult NewCrime(CrimeModel crimeModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				CrimeRepository.AddCrime(crimeModel.Count, crimeModel.LocalGovernmentAreaID, crimeModel.Month, crimeModel.OffenceID, crimeModel.Year);

				actionResult = RedirectToAction("Crimes", "Admin");
			}
			else
			{
				actionResult = View(crimeModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewLocalGovernmentArea()
		{
			return View(new LocalGovernmentAreaModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult NewLocalGovernmentArea(LocalGovernmentAreaModel localGovernmentAreaModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				LocalGovernmentAreaRepository.AddLocalGovernmentArea(localGovernmentAreaModel.IsDeleted, localGovernmentAreaModel.IsVisible, localGovernmentAreaModel.Name, localGovernmentAreaModel.StateID);

				actionResult = RedirectToAction("LocalGovernmentAreas", "Admin");
			}
			else
			{
				actionResult = View(localGovernmentAreaModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewOffence()
		{
			return View(new OffenceModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult NewOffence(OffenceModel offenceModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				OffenceRepository.AddOffence(offenceModel.IsDeleted, offenceModel.IsVisible, offenceModel.Name);

				actionResult = RedirectToAction("Offences", "Admin");
			}
			else
			{
				actionResult = View(offenceModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewOffenceCategory()
		{
			return View(new OffenceCategoryModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult NewOffenceCategory(OffenceCategoryModel offenceCategoryModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				OffenceCategoryRepository.AddOffenceCategory(offenceCategoryModel.IsDeleted, offenceCategoryModel.IsVisible, offenceCategoryModel.Name);

				actionResult = RedirectToAction("OffenceCategories", "Admin");
			}
			else
			{
				actionResult = View(offenceCategoryModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult NewState()
		{
			return View(new StateModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult NewState(StateModel stateModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				StateRepository.AddState(stateModel.AbbreviatedName, stateModel.IsDeleted, stateModel.IsVisible, stateModel.Name);

				actionResult = RedirectToAction("States", "Admin");
			}
			else
			{
				actionResult = View(stateModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Offence(uint id)
		{
			OffenceModel offenceModel = null;
			Offence offence = OffenceRepository.GetOffenceByID((int)(id));

			if (offence != null)
			{
				offenceModel = new OffenceModel(offence.DateCreatedUtc, offence.DateUpdatedUtc, offence.ID, offence.IsDeleted, offence.IsVisible, offence.Name, offence.OffenceCategoryID);
			}

			return View(offenceModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Offence(OffenceModel offenceModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				if (offenceModel.IsDelete == true)
				{
					//	Can't delete them at this time
				}
				else
				{
					OffenceRepository.UpdateOffence(offenceModel.ID, offenceModel.IsDeleted, offenceModel.IsVisible, offenceModel.Name, offenceModel.OffenceCategoryID);
				}

				actionResult = RedirectToAction("Offences", "Admin");
			}
			else
			{
				actionResult = View(offenceModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Offences(string sortBy, SortDirection? sortDirection, uint? page)
		{
			IEnumerable<Offence> offences = OffenceRepository.GetOffences();

			if ((String.IsNullOrEmpty(sortBy) == false) && (sortDirection.HasValue == true))
			{
				SortDirection sort = sortDirection.Value;

				switch (sortBy)
				{
					case "Date":
						if (sort == SortDirection.Ascending)
						{
							offences = offences.OrderBy(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						else
						{
							offences = offences.OrderByDescending(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						break;

					case "ID":
						if (sort == SortDirection.Ascending)
						{
							offences = offences.OrderBy(m => (m.ID));
						}
						else
						{
							offences = offences.OrderByDescending(m => (m.ID));
						}
						break;

					case "IsDeleted":
						if (sort == SortDirection.Ascending)
						{
							offences = offences.OrderBy(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						else
						{
							offences = offences.OrderByDescending(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						break;

					case "IsVisible":
						if (sort == SortDirection.Ascending)
						{
							offences = offences.OrderBy(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						else
						{
							offences = offences.OrderByDescending(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						break;

					case "Name":
						if (sort == SortDirection.Ascending)
						{
							offences = offences.OrderBy(m => (m.Name));
						}
						else
						{
							offences = offences.OrderByDescending(m => (m.Name));
						}
						break;
				}
			}

			return View(offences);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult OffenceCategory(uint id)
		{
			OffenceCategoryModel offenceCategoryModel = null;
			OffenceCategory offenceCategory = OffenceCategoryRepository.GetOffenceCategoryByID((int)(id));

			if (offenceCategory != null)
			{
				offenceCategoryModel = new OffenceCategoryModel(offenceCategory.DateCreatedUtc, offenceCategory.DateUpdatedUtc, offenceCategory.ID, offenceCategory.IsDeleted, offenceCategory.IsVisible, offenceCategory.Name);
			}

			return View(offenceCategoryModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult OffenceCategory(OffenceCategoryModel offenceCategoryModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				if (offenceCategoryModel.IsDelete == true)
				{
					//	Can't delete them at this time
				}
				else
				{
					OffenceCategoryRepository.UpdateOffenceCategory(offenceCategoryModel.ID, offenceCategoryModel.IsDeleted, offenceCategoryModel.IsVisible, offenceCategoryModel.Name);
				}

				actionResult = RedirectToAction("OffenceCategories", "Admin");
			}
			else
			{
				actionResult = View(offenceCategoryModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult OffenceCategories(string sortBy, SortDirection? sortDirection, uint? page)
		{
			IEnumerable<OffenceCategory> offenceCategories = OffenceCategoryRepository.GetOffenceCategories();

			if ((String.IsNullOrEmpty(sortBy) == false) && (sortDirection.HasValue == true))
			{
				SortDirection sort = sortDirection.Value;

				switch (sortBy)
				{
					case "Date":
						if (sort == SortDirection.Ascending)
						{
							offenceCategories = offenceCategories.OrderBy(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						else
						{
							offenceCategories = offenceCategories.OrderByDescending(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						break;

					case "ID":
						if (sort == SortDirection.Ascending)
						{
							offenceCategories = offenceCategories.OrderBy(m => (m.ID));
						}
						else
						{
							offenceCategories = offenceCategories.OrderByDescending(m => (m.ID));
						}
						break;

					case "IsDeleted":
						if (sort == SortDirection.Ascending)
						{
							offenceCategories = offenceCategories.OrderBy(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						else
						{
							offenceCategories = offenceCategories.OrderByDescending(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						break;

					case "IsVisible":
						if (sort == SortDirection.Ascending)
						{
							offenceCategories = offenceCategories.OrderBy(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						else
						{
							offenceCategories = offenceCategories.OrderByDescending(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						break;

					case "Name":
						if (sort == SortDirection.Ascending)
						{
							offenceCategories = offenceCategories.OrderBy(m => (m.Name));
						}
						else
						{
							offenceCategories = offenceCategories.OrderByDescending(m => (m.Name));
						}
						break;
				}
			}

			return View(offenceCategories);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult State(uint id)
		{
			StateModel stateModel = null;
			State state = StateRepository.GetStateByID((int)(id));

			if (state != null)
			{
				stateModel = new StateModel(state.AbbreviatedName, state.DateCreatedUtc, state.DateUpdatedUtc, state.ID, state.IsDeleted, state.IsVisible, state.Name);
			}

			return View(stateModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult State(StateModel stateModel)
		{
			ActionResult actionResult = null;

			if (ModelState.IsValid == true)
			{
				if (stateModel.IsDelete == true)
				{
					//	Can't delete them at this time
				}
				else
				{
					StateRepository.UpdateState(stateModel.ID, stateModel.AbbreviatedName, stateModel.IsDeleted, stateModel.IsVisible, stateModel.Name);
				}

				actionResult = RedirectToAction("States", "Admin");
			}
			else
			{
				actionResult = View(stateModel);
			}

			return actionResult;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult States(string sortBy, SortDirection? sortDirection, uint? page)
		{
			IEnumerable<State> states = StateRepository.GetStates();

			if ((String.IsNullOrEmpty(sortBy) == false) && (sortDirection.HasValue == true))
			{
				SortDirection sort = sortDirection.Value;

				switch (sortBy)
				{
					case "AbbreviatedName":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.AbbreviatedName));
						}
						else
						{
							states = states.OrderByDescending(m => (m.AbbreviatedName));
						}
						break;

					case "Date":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						else
						{
							states = states.OrderByDescending(m => (m.DateCreatedUtc)).ThenBy(m => (m.Name));
						}
						break;

					case "ID":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.ID));
						}
						else
						{
							states = states.OrderByDescending(m => (m.ID));
						}
						break;

					case "IsDeleted":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						else
						{
							states = states.OrderByDescending(m => (m.IsDeleted)).ThenBy(m => (m.Name));
						}
						break;

					case "IsVisible":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						else
						{
							states = states.OrderByDescending(m => (m.IsVisible)).ThenBy(m => (m.Name));
						}
						break;

					case "Name":
						if (sort == SortDirection.Ascending)
						{
							states = states.OrderBy(m => (m.Name));
						}
						else
						{
							states = states.OrderByDescending(m => (m.Name));
						}
						break;
				}
			}

			return View(states);
		}
	}
}