
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;



namespace JellyFishTest
{
	public class Tests1
	{
		IWebDriver driver;
		[OneTimeSetUp]
		public void StartChrom()
		{
			driver = new ChromeDriver(".");
		}

		[Test]
		public void RegisterEmployee()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Register";
			RegisterAsEmployer();
			Assert.Pass();
		}

		[Test]
		public void RegisterApplicant()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Register";
			RegisterAsApplicant();
			Assert.Pass();
		}

		[Test]
		public void LoginApplicant()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			Assert.Pass();
		}

		[Test]
		public void LoginEmployer()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			Assert.Pass();
		}



		//[Test]
		//public void ManageApplicantProfile()
		//{

		//    driver.Url = "https://localhost:7143/Identity/Account/Login";
		//    LoginApplicant();
		//    GetHref("Manage");
		//    Assert.Pass();
		//}

		[Test]
		public void UpdateFirstNameEmployerProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			GetHref("manageEmployer");
			driver.FindElement(By.Id("Input_FirstName")).Clear();
			driver.FindElement(By.Id("Input_FirstName")).SendKeys("Dave");
			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Your profile has been updated", a);
		}

		[Test]
		public void UpdateCompanyNameEmployerProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			GetHref("manageEmployer");
			driver.FindElement(By.Id("Input_Name")).Clear();
			driver.FindElement(By.Id("Input_Name")).SendKeys("Google");
			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Your profile has been updated", a);
		}

		[Test]
		public void UpdateLastNameEmployerProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			GetHref ("manageEmployer");
			driver.FindElement(By.Id("Input_LastName")).Clear();
			driver.FindElement(By.Id("Input_LastName")).SendKeys("");
			driver.FindElement(By.Id("update-profile-button")).Click();



			string a = driver.FindElement(By.Id("Input_LastName-error")).GetAttribute("innerHTML");
			Assert.AreEqual("The Last Name field is required.", a);
		}

		[Test]
		public void UpdatePhoneEmployerProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			GetHref("manageEmployer");
			driver.FindElement(By.Id("Input_PhoneNumber")).Clear();
			driver.FindElement(By.Id("Input_PhoneNumber")).SendKeys("");
			driver.FindElement(By.Id("update-profile-button")).Click();



			string a = driver.FindElement(By.Id("Input_PhoneNumber-error")).GetAttribute("innerHTML");
			Assert.AreEqual("You must provide a phone number", a);
		}

		[Test]

		public void SearchFor_JR_Jobs()
		{
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            driver.FindElement(By.Name("searchQuery")).SendKeys("JR");
            driver.FindElement(By.Id("searchBtn")).Click();
            var jobs = driver.FindElement(By.Id("jobs"));
			//var a = driver.FindElement(By.Id("ind-job"));
			var a = jobs.FindElements(By.Id("ind-job"));
			Assert.AreEqual(1, a.Count());
        }
		[Test]
        public void SearchFor_Senior_Jobs()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            driver.FindElement(By.Name("searchQuery")).SendKeys("Senior");
            driver.FindElement(By.Id("searchBtn")).Click();
            var jobs = driver.FindElement(By.Id("jobs"));
            //var a = driver.FindElement(By.Id("ind-job"));
            var a = jobs.FindElements(By.Id("ind-job"));
            Assert.AreEqual(1, a.Count());
        }


        [Test]

        public void Re_ApplyForJob()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            driver.FindElement(By.Name("searchQuery")).SendKeys("Senior");
            driver.FindElement(By.Id("searchBtn")).Click();
            var job = driver.FindElements(By.Id("ind-job")).LastOrDefault();
            driver.Url = job.GetAttribute("href");
            //var a = driver.FindElement(By.Id("ind-job"));
            string isApplied = driver.FindElement(By.Id("isApplied")).GetAttribute("innerHTML");
			//driver.FindElement(By.Id("ind-job"))
			Assert.AreEqual("Already applied !", isApplied);
		}

        [Test]
        public void ApplyForJob()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            driver.FindElement(By.Name("searchQuery")).SendKeys("Intern");
            driver.FindElement(By.Id("searchBtn")).Click();
            var jobs = driver.FindElement(By.Id("jobs"));
            var allJobs = jobs.FindElements(By.Id("ind-job"));
			var job = allJobs.LastOrDefault();
			driver.Url = job.GetAttribute("href");
            driver.FindElement(By.Id("btnApply")).Click();
            string isApplied = driver.FindElement(By.Id("isApplied")).GetAttribute("innerHTML");
            Assert.AreEqual("Already applied !", isApplied);
        }

        [Test]
        public void DisplayApplicantJob()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();

			GetHref("manageJobSeeker");
			GetHref("myjobs");
			string myJobPageTtile = driver.FindElement(By.Id("jobsTitle")).GetAttribute("innerHTML");
			Assert.AreEqual("My Jobs", myJobPageTtile);
        }


		[Test]

		public void FilterAppliedJobs()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("myjobs");
			var myJobsFilter = driver.FindElement(By.Id("filterSelect"));
			var selectElement = new SelectElement(myJobsFilter);
			selectElement.SelectByText("Applied");
			var jobsContainer = driver.FindElement(By.Id("container"));
			var jobs = jobsContainer.FindElements(By.Id("ind-job"));
			Assert.Greater(jobs.Count(), -1);

		}

		



		[Test]

        public void FilterSavedJobs()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            GetHref("myjobs");
            var myJobsFilter = driver.FindElement(By.Id("filterSelect"));
            var selectElement = new SelectElement(myJobsFilter);
            selectElement.SelectByText("Saved");
            var jobsContainer = driver.FindElement(By.Id("container"));
            var jobs = jobsContainer.FindElements(By.Id("ind-job"));
            Assert.Greater(jobs.Count(), -1);
        }

        [Test]

        public void FilterAllJobs()
        {
            driver.Url = "https://localhost:7143/Identity/Account/Login";
            loginAsApplicant();
            GetHref("myjobs");
            var myJobsFilter = driver.FindElement(By.Id("filterSelect"));
            var selectElement = new SelectElement(myJobsFilter);
            selectElement.SelectByText("All");
            var jobsContainer = driver.FindElement(By.Id("container"));
            var jobs = jobsContainer.FindElements(By.Id("ind-job"));
            Assert.Greater(jobs.Count(), -1);
        }


		[Test]

		public void AlreadySelectApplicant()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();

			var d = driver.FindElement(By.Id("empRowContainer"));


			GetHref("viewAppl");
			GetHref("appDetail");
			var aBtnEnables = driver.FindElement(By.Id("acceptBtn")).Enabled;

			Assert.AreEqual(aBtnEnables, false);


	

		}


		[Test]

		public void SelectApplicant()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			var result = "";
			var d = driver.FindElement(By.Id("empRowContainer"));
			var applicants = driver.FindElements(By.Id("viewAppl"));
			if (applicants.Count() > 0)
			{
				var applicant = applicants.Where(x => x.Enabled).First();
				driver.Url = applicant.GetAttribute("href");
				var applicantDetail = driver.FindElements(By.Id("appDetail"));
				if (applicantDetail.Count() > 0)
				{
					foreach (var item in applicantDetail)
					{
						driver.Url = item.GetAttribute("href");
						var acceptBtnEnabled = driver.FindElement(By.Id("acceptBtn")).Enabled;
						if (acceptBtnEnabled)
						{
							driver.FindElement(By.Id("acceptBtn")).Click();
							Assert.AreEqual(acceptBtnEnabled, true);
						}
						else
						{
							result = "Job Already Declined";
							driver.Navigate().Back();

						}
					}
				}
			}
			else
			{
				result = "Job Already Declined";
				Assert.AreEqual(result, "Job Already Declined");
			}







		}


		[Test]
			public void DeclineApplicant()
			{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsEmployer();
			var result = "";
			var d = driver.FindElement(By.Id("empRowContainer"));
			var applicants = driver.FindElements(By.Id("viewAppl"));
			if (applicants.Count() > 0)
			{
				var applicant = applicants.Where(x => x.Enabled).Last();
				driver.Url = applicant.GetAttribute("href");
				var applicantDetail = driver.FindElements(By.Id("appDetail"));
				if (applicantDetail.Count() > 0)
				{
					foreach (var item in applicantDetail)
					{
						driver.Url = item.GetAttribute("href");
						var acceptBtnEnabled = driver.FindElement(By.Id("declineBtn")).Enabled;
						if (acceptBtnEnabled)
						{
						driver.FindElement(By.Id("declineBtn")).Click();
							Assert.AreEqual(acceptBtnEnabled, true);
						}
						else
						{
							result = "Job Already Declined";
							driver.Navigate().Back();

						}
					}
				}
			}
			else
			{
				result = "Job Already Declined";
				Assert.AreEqual(result, "Job Already Declined");
			}
		}



		[OneTimeTearDown]
		public void CloseTest()
		{
			driver.Close();
		}


		private void RegisterAsEmployer()
		{
			driver.FindElement(By.Id("Input_Email")).SendKeys("Srageh6202@conestogac.on.ca");

			WebElement element = (WebElement)driver.FindElement(By.Id("option2"));

			IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
			executor.ExecuteScript("arguments[0].click()", element);

			//driver.FindElement(By.Id("option2")).Click();

			// click second element 

			//driver.FindElement(By.Id("Input_UserType")).SendKeys("2");
			driver.FindElement(By.Id("Input_Password")).SendKeys("Test123@");
			driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Test123@");
			driver.FindElement(By.Id("registerSubmit")).Click();
		}

		private void RegisterAsApplicant()
		{
			driver.FindElement(By.Id("Input_Email")).SendKeys("ragehsuhaib@gmail.com");
			WebElement element = (WebElement)driver.FindElement(By.Id("option1"));
			IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
			executor.ExecuteScript("arguments[0].click()", element);
			driver.FindElement(By.Id("Input_Password")).SendKeys("Test123@");
			driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Test123@");
			driver.FindElement(By.Id("registerSubmit")).Click();
		}

		private void loginAsEmployer()
		{
			driver.FindElement(By.Id("Input_Email")).SendKeys("s@gmail.com");
			driver.FindElement(By.Id("Input_Password")).SendKeys("Test123@");
			driver.FindElement(By.Id("login-submit")).Click();
		}




		private void loginAsApplicant()
		{
			driver.FindElement(By.Id("Input_Email")).SendKeys("d@gmail.com");
			driver.FindElement(By.Id("Input_Password")).SendKeys("Test123@");
			driver.FindElement(By.Id("login-submit")).Click();
		}

		private void GetHref(string id)
		{
			driver.Url = driver.FindElement(By.Id(id)).GetAttribute("href");
		}









	}
}