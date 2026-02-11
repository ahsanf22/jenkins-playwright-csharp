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
        
        // 1. Handle "Reject Cookies" if it appears
        var rejectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Rifiuta tutto" });
        var acceptButton = Page.GetByRole(AriaRole.Button, new() { Name = "Accetta tutto" });
        
        if (await rejectButton.IsVisibleAsync()) {
            await rejectButton.ClickAsync();
        } else if (await acceptButton.IsVisibleAsync()) {
            await acceptButton.ClickAsync();
        }

        // 2. Type "Jenkins"
        await Page.GetByRole(AriaRole.Combobox, new() { Name = "Cerca" }).FillAsync("Jenkins");
        
        // 3. Press Enter
        await Page.Keyboard.PressAsync("Enter");

        // 4. Check results
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Jenkins" }).First).ToBeVisibleAsync();
    }
}
