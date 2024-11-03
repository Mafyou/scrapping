using Microsoft.Playwright;

namespace ScrappingWebsite.Linxea.Tools;

public class SubscripitonTranversePath(string StartUrl)
{
    private const string questionSelector = "#app-container > section > main > div > div > div > form > div.full-width > div > article > div > div > div:nth-child({0}) > label";
    public async Task Run()
    {
        if (string.IsNullOrEmpty(StartUrl))
        {
            throw new ArgumentException("StartUrl is required");
        }
        using var playwright = await Playwright.CreateAsync();
        // initialize a Chromium instance
        await using var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = false, // set to "false" while developing
        });
        // open a new page within the current browser context
        var page = await browser.NewPageAsync();
        var nextPage = "#app-container > section > main > div > div > div > form > div.tunnel-btn-wrapper.wrapper__navigation-buttons > div > button";
        await page.GotoAsync(StartUrl);
        await page.ClickAsync(".linxea-redirectionlink");

        await page.ClickAsync("#app-container > section > main > section > div.tunnel-section__buttons > a.btn.btn--primary.btn--large.btn--wide");
        await page.ClickAsync("#app-container > section > main > div > div > div > form > div.full-width > div > article > div > div > div:nth-child(1) > label");
        await page.ClickAsync(nextPage);

        var actionScreen = page.FillAsync("input[name=initialInvestmentAmount]", "123456");
        var nextPageAction = page.ClickAsync(nextPage);
        await Task.WhenAll(actionScreen, nextPageAction);

        actionScreen = page.FillAsync("input[name=saveEachMonthAmount]", "123");
        nextPageAction = page.ClickAsync(nextPage);
        await Task.WhenAll(actionScreen, nextPageAction);

        await page.ClickAsync(string.Format(questionSelector, 2));
        await page.ClickAsync(string.Format(questionSelector, 3));

        actionScreen = page.FillAsync("input[name=birthDate]", "16/06/1987");
        nextPageAction = page.ClickAsync(nextPage);
        await Task.WhenAll(actionScreen, nextPageAction);

        await page.ClickAsync(string.Format(questionSelector, 1));
        await page.ClickAsync(string.Format(questionSelector, 2));
        await page.ClickAsync(string.Format(questionSelector, 1));
        await page.ClickAsync(string.Format(questionSelector, 1));

        await page.ClickAsync(string.Format(questionSelector, 1));
        nextPageAction = page.ClickAsync(nextPage);
        await Task.WhenAll(actionScreen, nextPageAction);

        actionScreen = page.ClickAsync(".tunnel-btn-wrapper__bottom");
        nextPageAction = page.ClickAsync(nextPage);
        await Task.WhenAll(actionScreen, nextPageAction);

        await Task.Delay(Timeout.Infinite);
    }
}