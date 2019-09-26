Feature: Search
<feature description>

  Scenario: Search Televisions 1
    Given Browse to web site "http://localhost:8080"
    When input keyword "Televisions" in "search"
    Then wait for classname "btn btn-success"
    Then Click search button
    Then wait for classname "topnav"
    When input "22" in id "screenSize"
    When click by id "fullhd"
    When click by id "led"
    When input "3" in id "screenUsersAmount"
    When input "2" in id "screenDailyUsage"
    When input "2" in id "screenTotalAmount"
    When input "5" in id "screenHoursUsage"
    When input "Panasonic" in id "prefferedBrand"
    When click by id "negro"
    When set price to "300"
    When input "300" in id "desiredPricing"
    Then click submit button


Scenario: Search Televisions 2
    Given Browse to web site "http://localhost:8080"
    When input keyword "Televisions" in "search"
    Then wait for classname "btn btn-success"
    Then Click search button
    Then wait for classname "topnav"
    When input "22" in id "screenSize"
    When click by id "fullhd"
    When click by id "led"
    When input "3" in id "screenUsersAmount"
    When input "2" in id "screenDailyUsage"
    When input "2" in id "screenTotalAmount"
    When input "5" in id "screenHoursUsage"
    When input "Panasonic" in id "prefferedBrand"
    When click by id "negro"
    When click by id "deviceThickness"
    When input "10" in id "wantedThick"
    When set price to "300"
    When input "300" in id "desiredPricing"
    Then click submit button



