const { Given, When, Then } = require('cucumber');
const { Builder, By, Key, until } = require('selenium-webdriver');
const assert = require('assert');
const { driver } = require('../support/web_driver');
const util = require('util');



Given(/^Browse to web site "([^"]*)"$/, async function(url) {

    await driver.manage().window().maximize();
    await driver.get(url);
});


When(/^input keyword "([^"]*)" in "([^"]*)"$/, async function (Value, Element) {

    return await driver.findElement({name: Element}).sendKeys(Value);
     
});


When(/^wait for element "([^"]*)"$/, async function (element) {


    return driver.manage().timeouts().implicitlyWait(10000);
  //WebDriverWait wait = new WebDriverWait(driver, 15);
    //const val = await this.page.waitforselector("element");
});

Then(/^Click search button$/, async function () {

    const btn = await driver.findElement({ className:"btn btn-success"});
    return btn.click();
});


When(/^click in Google sign In$/, async function () {
    
    const elem = await driver.findElement({className: "abcRioButtonContents" })
    .findElement(By.tagName("span"));
    console.log("i got here");
    return await elem.click();
   
});

When(/^wait for classname "([^"]*)"$/, async function (element) {

    const ELEM_BY = await By["className"](element);
    await driver.wait(until.elementLocated(ELEM_BY));
    await driver.wait(until.elementIsVisible(driver.findElement(ELEM_BY)), 10000);
    await driver.sleep(1000);
    return true;
    
});

When(/^input "([^"]*)" in id "([^"]*)"$/, async function (value, id_name) {

    const element = await driver.findElement({id:id_name});
    await element.sendKeys(value);
    return true;
});

When(/^click by id "([^"]*)"$/, async function (id_Value) {

    const elem = await driver.findElement({id:id_Value});
    return elem.click();
});

When(/^set price to "([^"]*)"$/, async function (price) {

    const ele = await driver.findElement({ id: "myRange" });
    return ele.sendKeys(price);
});

Then(/^click submit button$/, async function () {

    const ele = await driver.findElement({ id:"submitButton"})
    return await ele.click();
});





