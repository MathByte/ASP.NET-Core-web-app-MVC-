using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace JellyFishTest
{
	public class Tests2
	{
		IWebDriver driver;
		[OneTimeSetUp]
		public void StartChrom()
		{
			driver = new ChromeDriver(".");
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
		public void UpdateFirstNameProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_FirstName")).Clear();
			driver.FindElement(By.Id("Input_FirstName")).SendKeys("Dave");
			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Your profile has been updated", a);
		}

	
		[Test]
		public void UpdateLastNameProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_LastName")).Clear();
			driver.FindElement(By.Id("Input_LastName")).SendKeys("Nichole");
			driver.FindElement(By.Id("update-profile-button")).Click();



			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Assert.AreEqual("Your profile has been updated", a);
		}

		[Test]
		public void UpdatePhoneProfile()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_PhoneNumber")).Clear();
			driver.FindElement(By.Id("Input_PhoneNumber")).SendKeys("222-222-2222");
			driver.FindElement(By.Id("update-profile-button")).Click();



			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Assert.AreEqual("Your profile has been updated", a);
		}


		[Test]
		public void CheckingUnValidPhoneNumberMessage()
		{

			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_PhoneNumber")).Clear();
			driver.FindElement(By.Id("Input_PhoneNumber")).SendKeys("");
			driver.FindElement(By.Id("update-profile-button")).Click();



			string a = driver.FindElement(By.Id("Input_PhoneNumber-error")).GetAttribute("innerHTML");
			Assert.AreEqual("You must provide a phone number", a);
		}

		[Test]
		public void Address()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_Street")).Clear();
			driver.FindElement(By.Id("Input_City")).Clear();
			driver.FindElement(By.Id("Input_PostalCode")).Clear();
			driver.FindElement(By.Id("Input_Province")).Clear();


			driver.FindElement(By.Id("Input_Street")).SendKeys("testS");
			driver.FindElement(By.Id("Input_City")).SendKeys("testC");
			driver.FindElement(By.Id("Input_PostalCode")).SendKeys("testP");
			driver.FindElement(By.Id("Input_Province")).SendKeys("testPRO");


			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Your profile has been updated", a);
		}




		[Test]
		public void Skills()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_Skill")).Clear();
			driver.FindElement(By.Id("Input_Skill")).SendKeys("testS");



			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Your profile has been updated", a);
		}




		[Test]
		public void SkillsDublicationCheck()
		{
			driver.Url = "https://localhost:7143/Identity/Account/Login";
			loginAsApplicant();
			GetHref("manageJobSeeker");
			driver.FindElement(By.Id("Input_Skill")).Clear();
			driver.FindElement(By.Id("Input_Skill")).SendKeys("testS");



			driver.FindElement(By.Id("update-profile-button")).Click();

			string a = driver.FindElement(By.Id("message")).GetAttribute("innerHTML");
			Console.WriteLine(a);
			Assert.AreEqual("Skill is already exist!", a);
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
			driver.FindElement(By.Id("Input_Email")).SendKeys("kkerbabian7278@conestogac.on.ca");
			WebElement element = (WebElement)driver.FindElement(By.Id("option1"));

			IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
			executor.ExecuteScript("arguments[0].click()", element);

			driver.FindElement(By.Id("Input_Password")).SendKeys("Admin!23");
			driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("Admin!23");
			driver.FindElement(By.Id("registerSubmit")).Click();
		}





		private void loginAsApplicant()
		{
			driver.FindElement(By.Id("Input_Email")).SendKeys("kkerbabian7278@conestogac.on.ca");
			driver.FindElement(By.Id("Input_Password")).SendKeys("Admin!23");
			driver.FindElement(By.Id("login-submit")).Click();
		}

		private void GetHref(string id)
		{
			driver.Url = driver.FindElement(By.Id(id)).GetAttribute("href");
		}









	}
}