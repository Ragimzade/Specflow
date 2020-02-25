Feature: Searching for cars
  As a generic user
  I want to be able to search for cars
  and get result on specified filters

  Scenario Outline: Search for cars
    Given User is on main page
    When User search for cars with brand <brand> and model <model>
    Then user gets result with correct car's brand <brand> and model <model>
    When User sorts cars by price
    Then User sees that cars are sorted by price
    When User sorts cars by year
    Then User sees that cars are sorted by year
    When User sorts cars by date
    Then User sees  that cars are sorted by date

    Examples:
      | brand | model |
      | Lexus | CT    |
      | Lexus | LX    |

    