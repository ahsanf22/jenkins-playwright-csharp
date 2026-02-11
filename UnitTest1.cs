using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    // Test 1: Checks the title (Keep this one)
    [Test]
    public async Task CheckGoogleTitle()
    {
        await Page.GotoAsync("https://google.com");
        await Expect(Page).ToHaveTitleAsync("Google");
    }

    // Test 2: Actually searches for something (NEW!)
    [Test]
    public async Task SearchForJenkins()
    {
        await Page.GotoAsync("https://google.com");
        
        // 1. Handle the "Reject Cookies" popup if it appears (common in Europe/Italy)
        // We use .Or() to make it robust: either click 'Reject all' OR 'Accept all' if found
        var rejectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Rifiuta tutto" });
        var acceptButton = Page.GetByRole(AriaRole.Button, new() { Name = "Accetta tutto" });
        
        if (await rejectButton.IsVisibleAsync()) {
            await rejectButton.ClickAsync();
        } else if (await acceptButton.IsVisibleAsync()) {
            await acceptButton.ClickAsync();
        }

        // 2. Type "Jenkins" into the search box
        await Page.GetByRole(AriaRole.Combobox, new() { Name = "Cerca" }).FillAsync("Jenkins");
        
        // 3. Press Enter
        await Page.Keyboard.PressAsync("Enter");

        // 4. Check if the results contain the official site
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Jenkins" }).First).ToBeVisibleAsync();
    }
}
