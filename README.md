# Adhan CSharp

[![badge-travis][]][travis] 

Adhan CSharp is a well tested and well documented library for calculating Islamic prayer times. Adhan CSharp is written to be compatible with .NET Core. It has a small method overhead, and has no external dependencies.

All astronomical calculations are high precision equations directly from the book [“Astronomical Algorithms” by Jean Meeus](http://www.willbell.com/math/mc1.htm). This book is recommended by the Astronomical Applications Department of the U.S. Naval Observatory and the Earth System Research Laboratory of the National Oceanic and Atmospheric Administration.

Implementations of Adhan in other languages can be found in the parent repo [Adhan](https://github.com/batoulapps/Adhan).

## Usage

### Visual Studio Code

Open Visual Studio Code.  Choose File Open Folder and select the folder where the cloned repo exists (e.g. c:\repos\adhan-csharp).
From a Terminal window (CTRL-SHIFT-`), run the following command:

```
dotnet build 
```

### Initialization parameters

#### Coordinates

Create a `Coordinates` object with the latitude and longitude for the location you want prayer times for.

```java
Coordinates coordinates = new Coordinates(35.78056, -78.6389);
```

#### Date

The date parameter passed in should be an instance of the `DateComponents` object. The year, month, and day values need to be populated. All other values will be ignored. The year, month and day values should be for the local date that you want prayer times for. These date values are expected to be for the Gregorian calendar. There's also a convenience method for converting a `System.DateTime` to `DateComponents`.

```csharp
DateComponents date = new DateComponents(2015, 11, 1);
DateComponents date = DateComponents.From(DateTime.Now);
```

#### Calculation parameters

The rest of the needed information is contained within the `CalculationParameters` class. Instead of manually initializing this class, it is recommended to use one of the pre-populated instances in the `CalculationMethod` class. You can then further customize the calculation parameters if needed.

```csharp
CalculationParameters calcParams =
     CalculationMethod.MUSLIM_WORLD_LEAGUE.GetParameters();
calcParams.Madhab = Madhab.HANAFI;
calcParams.Adjustments.Fajr = 2;
```

| Parameter | Description |
| --------- | ----------- |
| `Method`    | CalculationMethod name |
| `FajrAngle` | Angle of the sun used to calculate Fajr |
| `IshaAngle` | Angle of the sun used to calculate Isha |
| `IshaInterval` | Minutes after Maghrib (if set, the time for Isha will be Maghrib plus ishaInterval) |
| `Madhab` | Value from the Madhab object, used to calculate Asr |
| `HighLatitudeRule` | Value from the HighLatitudeRule object, used to set a minimum time for Fajr and a max time for Isha |
| `Adjustments` | Custom prayer time adjustments in minutes for each prayer time |

**CalculationMethod**

| Value | Description |
| ----- | ----------- |
| `MUSLIM_WORLD_LEAGUE` | Muslim World League. Fajr angle: 18, Isha angle: 17 |
| `EGYPTIAN` | Egyptian General Authority of Survey. Fajr angle: 19.5, Isha angle: 17.5 |
| `KARACHI` | University of Islamic Sciences, Karachi. Fajr angle: 18, Isha angle: 18 |
| `UMM_AL_QURA` | Umm al-Qura University, Makkah. Fajr angle: 18, Isha interval: 90. *Note: you should add a +30 minute custom adjustment for Isha during Ramadan.* |
| `DUBAI` | Method used in UAE. Fajr and Isha angles of 18.2 degrees. |
| `QATAR` | Modified version of Umm al-Qura used in Qatar. Fajr angle: 18, Isha interval: 90. |
| `KUWAIT` | Method used by the country of Kuwait. Fajr angle: 18, Isha angle: 17.5 |
| `MOONSIGHTING_COMMITTEE` | Moonsighting Committee. Fajr angle: 18, Isha angle: 18. Also uses seasonal adjustment values. |
| `SINGAPORE` | Method used by Singapore. Fajr angle: 20, Isha angle: 18. |
| `NORTH_AMERICA` | Referred to as the ISNA method. This method is included for completeness but is not recommended. Fajr angle: 15, Isha angle: 15 |
| `KUWAIT` | Kuwait. Fajr angle: 18, Isha angle: 17.5 |
| `OTHER` | Fajr angle: 0, Isha angle: 0. This is the default value for `method` when initializing a `CalculationParameters` object. |

**Madhab**

| Value | Description |
| ----- | ----------- |
| `SHAFI` | Earlier Asr time |
| `HANAFI` | Later Asr time |

**HighLatitudeRule**

| Value | Description |
| ----- | ----------- |
| `MIDDLE_OF_THE_NIGHT` | Fajr will never be earlier than the middle of the night and Isha will never be later than the middle of the night |
| `SEVENTH_OF_THE_NIGHT` | Fajr will never be earlier than the beginning of the last seventh of the night and Isha will never be later than the end of the first seventh of the night |
| `TWILIGHT_ANGLE` | Similar to `SEVENTH_OF_THE_NIGHT`, but instead of 1/7, the fraction of the night used is fajrAngle/60 and ishaAngle/60 |


### Prayer Times

Once the `PrayerTimes` object has been initialized it will contain values for all five prayer times and the time for sunrise. The prayer times will be  DateTime object instances initialized with UTC values. To display these times for the local timezone, a formatting and timezone conversion formatter should be used, for example `TimeZoneInfo`.

```csharp
TimeZoneInfo easternTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Fajr, easternTime)
```

### Qibla

As of version 1.1.0, this library provides a `Qibla` class for getting the qibla for a given location.

```java
Coordinates coordinates = new Coordinates(latitude, longitude);
Qibla qibla = new Qibla(coordinates);
// qibla.direction is the qibla direction
```

## Full Example

See an example in the `Adhan.Samples` module.

```csharp
static void Main(string[] args)
{
     Coordinates coordinates = new Coordinates(43.61, -79.70);
     DateComponents dateComponents = DateComponents.From(DateTime.Now);
     CalculationParameters parameters = CalculationMethod.NORTH_AMERICA.GetParameters();

     TimeZoneInfo easternTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

     PrayerTimes prayerTimes = new PrayerTimes(coordinates, dateComponents, parameters);
     Console.WriteLine("Fajr   : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Fajr, easternTime));
     Console.WriteLine("Sunrise: " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Sunrise, easternTime));
     Console.WriteLine("Dhuhr  : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Dhuhr, easternTime));
     Console.WriteLine("Asr    : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Asr, easternTime));
     Console.WriteLine("Maghrib: " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Maghrib, easternTime));
     Console.WriteLine("Isha   : " + TimeZoneInfo.ConvertTimeFromUtc(prayerTimes.Isha, easternTime));
}
```

[badge-travis]: https://travis-ci.org/davidpet86/Adhan-csharp.svg?branch=master
[travis]: https://travis-ci.org/davidpet86/Adhan-csharp
