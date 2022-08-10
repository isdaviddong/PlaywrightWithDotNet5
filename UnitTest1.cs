using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace autUiTest
{
    [TestClass]
    public class UnitTest1
    {
      [TestMethod]
        public void TestMethod1()
        {
            //運行後的結果包含 24.22145
            Assert.IsTrue(AutoScript().Result.Contains("24.22145"));
        }

	//run auto script
        public static async Task<string> AutoScript()
        {
            using var playwright = await Playwright.CreateAsync();
	    //launch browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            // Open new page
            var page = await context.NewPageAsync();

            // Go to https://td-testplan.azurewebsites.net/
            await page.GotoAsync("https://td-testplan.azurewebsites.net/");

            // Click input[name="fieldHeight"]
            await page.ClickAsync("input[name=\"fieldHeight\"]");

            // Fill input[name="fieldHeight"]
            await page.FillAsync("input[name=\"fieldHeight\"]", "170");

            // Press Tab
            await page.PressAsync("input[name=\"fieldHeight\"]", "Tab");

            // Fill input[name="fieldWeight"]
            await page.FillAsync("input[name=\"fieldWeight\"]", "70");

            // Click button:has-text("計算")
            await page.ClickAsync("button:has-text(\"計算\")");

            //取得"包含BMI : "的文字區塊;
            var ret = page.TextContentAsync("text=BMI : ").GetAwaiter().GetResult();
	    //回傳取得的區塊
            return ret;
        }
    }
}
