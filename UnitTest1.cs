using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task CheckGoogleTitle()
    {
        await Page.GotoAsync("https://google.com");
        // Expect a title "Google"
        await Expect(Page).ToHaveTitleAsync("Google");
    }
}
