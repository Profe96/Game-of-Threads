Feature: Login
<feature description>

  Scenario: Login Google if unsigned
    Given Browse to web site "http://localhost:8080"
    When wait for classname "logo"
    When click in Google sign In