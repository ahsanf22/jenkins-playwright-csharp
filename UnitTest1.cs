using Microsoft.Playwright; // <--- This is the line that was missing!
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    // Test 1: Checks the title
    [Test]
    public async Task CheckGoogleTitle()
    {
        await Page.GotoAsync("https://google.com");
        await Expect(Page).ToHaveTitleAsync("Google");
    }

    // Test 2: Search for Jenkins
    [Test]
    public async Task SearchForJenkins()
    {
        await Page.GotoAsync("https://google.com");
        
        // 1. Handle "Reject Cookies" (Italian version)
        // We wait a bit to see if the popup appears
        try {
            var rejectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Rifiuta tutto" });
            if (await rejectButton.IsVisibleAsync(new() { Timeout = 2000 })) {
                await rejectButton.ClickAsync();
            }
        } catch { /* Ignore if no popup appears */ }

        // 2. Type "Jenkins" and search
        await Page.GetByRole(AriaRole.Combobox, new() { Name = "Cerca" }).FillAsync("Jenkins");
        await Page.Keyboard.PressAsync("Enter");

        // 3. ROBUST ASSERTION: Look for any Heading (h3) that contains the word "Jenkins"
        // This works even if the title is "Jenkins: The leading automation server"
        var firstResult = Page.Locator("h3").Filter(new() { HasText = "Jenkins" }).First;
        
        await Expect(firstResult).ToBeVisibleAsync();
    }
}
