const puppeteer = require("puppeteer");

(async () => {

  const url = "http://localhost:5173/Build_CV";

  const browser = await puppeteer.launch({
    headless: true,
  });

  const page = await browser.newPage();
  await page.goto(url, { waitUntil: 'networkidle0' });

  await page.$$eval('.form-section', elements => {
    elements.forEach(el => el.remove());
  });

  await page.waitForSelector('.CV-container');

  await page.pdf({
    path: "CV-only.pdf",
    format: "A4",
    printBackground: true,
  });

  await browser.close();
})();
