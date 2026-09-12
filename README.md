# Pensionikalkulaator (Pension Calculator) — WPF + Class Library

## Overview

This is an educational project: a WPF application (`WpfApp1`) and a separate class library (`PensionCalculator.Core`).
All calculation logic and input validation are implemented in a public static class inside `PensionCalculator.Core` (file `PensionCalculator.Core/PensionLogic.cs`).
The architecture is split into two projects to keep the user interface completely independent of the business and validation logic.

---

## Project Structure

- `WpfApp1/` — WPF application (UI, XAML layout, input bindings, error display, and result presentation).
- `PensionCalculator.Core/` — class library containing all calculations and validation rules (`PensionLogic.cs`).
- `.gitignore` — standard ignore rules for Visual Studio and .NET.
- `README.md` — this documentation file.

---

## How to Run

1. Open the solution file in Visual Studio (supported versions: VS 2022/2026).
2. Ensure the target framework is available (`.NET 8` is set for the projects).
3. Go to the top menu and select **Build -> Rebuild Solution**.
4. Set the `WpfApp1` project as the Startup Project and click **Start (F5)**.

---

## Calculation Logic & Architecture

All validation and core calculations are encapsulated in the public static class `PensionLogic` (`PensionCalculator.Core/PensionLogic.cs`). This ensures clean separation of concerns and avoids code duplication (DRY principle).

### Step-by-step execution flow of the `Calculate` method:

1. **Empty Field Validation:** 
   The method uses `string.IsNullOrWhiteSpace` to check each of the input fields. If any field is empty, it throws an exception with a clear message in Estonian: *"Palun täitke kõik väljad."*

2. **Separator Normalization:** 
   Numeric strings automatically replace commas with dots (`.Replace(',', '.')`) to safely handle European keyboard layouts and localization differences.

3. **Type Parsing:** 
   Using `int.TryParse` and `decimal.TryParse` with invariant culture, the app checks that user inputs are valid numbers rather than letters. If parsing fails, it throws a format exception: *"Palun sisestage väljadesse ainult numbrilised väärtused."*

4. **Range & Business Validation:** 
   - Current age must be between 0 and 120 years.
   - Target age must be strictly greater than the current age and cannot exceed 120 years.
   - Current savings and monthly contributions must be non-negative.
   - Expected annual return rate must range between 0% and 100%.

5. **Compounding Model & Math:** 
   - **Time span:** Total years are calculated as `targetAge - currentAge`, converted into total months (`totalMonths = years * 12`).
   - **Rate conversion:** The annual rate $r$ is converted to a monthly rate: `monthlyRate = (annualRate / 100) / 12`.
   - **Compounding loop:** A loop runs for each month. On every iteration, accrued monthly interest is added to the capital, followed by the regular monthly contribution:
     $$\text{TotalCapital} = (\text{TotalCapital} \times (1 + \text{MonthlyRate})) + \text{MonthlyContribution}$$
   - **Rounding:** The final capital result is rounded to 2 decimal places using `Math.Round(..., 2)`.

---

## Calculation Model and Assumptions

- **Input data set:**
  - Current age (years)
  - Target age (years)
  - Current savings (currency)
  - Monthly contribution (currency)
  - Expected annual return (percentage, e.g., `5` = 5%)

- **Model assumptions:** Monthly compounding with regular end-of-month contributions. 
- **Exclusions:** Taxes, bank/fund fees, and inflation are excluded from the model.
- **Error Handling:** On error, the previous result is not shown — only the clear red error message is visible in the UI.

---

## Test Cases

### 1) Normal Example (Valid Input)
* **Input data:**
  - Current age: `30`
  - Target age: `65`
  - Current savings: `5,000`
  - Monthly contribution: `200`
  - Annual return: `7`

* **Expected result (approx.):** `334,521.42 €`  
*(Calculation details: monthly rate = 0.07/12, total periods $n = 35 \times 12 = 420$ months, compound growth loop applied).*

### 2) Invalid Input (Logical Range Error)
* **Input data:**
  - Current age: `65`
  - Target age: `60` (target age is less than or equal to current age)
  - Other fields: valid numeric data.

* **Expected behavior & error message:** Result is hidden. A clear error message is displayed in the UI: *"Sihtvanus peab olema suurem kui praegune vanus."* (Target age must be greater than current age).

---

## Where to Find Core Code

- `PensionCalculator.Core/PensionLogic.cs` — public static class implementing input validation and final capital calculations.
- `WpfApp1/*` — UI implementation, layout styling, input bindings, error handling, and result rendering.

---

## Notes

- This README contains all modeling assumptions and test cases. 
- Upload the repository to GitHub and provide the link in Moodle according to your instructor's instructions.

---

## View
<img width="461" height="467" alt="image" src="https://github.com/user-attachments/assets/a9ab7d89-7242-457e-856c-64ad724272a5" />
<img width="464" height="469" alt="image" src="https://github.com/user-attachments/assets/63f7b95f-a3d0-4474-a3d0-bb60d691cbeb" />

Mistakes:  
<img width="461" height="464" alt="image" src="https://github.com/user-attachments/assets/16655099-80d0-4930-9dda-8509cc2ce0ba" />
<img width="464" height="472" alt="image" src="https://github.com/user-attachments/assets/478798e4-d7eb-4e52-90d7-7b81504611b3" />
<img width="463" height="471" alt="image" src="https://github.com/user-attachments/assets/9f78a04d-d7ec-46ec-a6a5-ae9e85ee5f2f" />



